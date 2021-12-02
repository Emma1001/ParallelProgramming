using System;
using System.Threading;
using System.Threading.Tasks;

namespace Base51
{
    class Agent
    {
        public string Name { get; set; }
        public SecurityLevelEnum SecurityLevel { get; set; }
        public Elevator Elevator { get; set; }
        public FloorEnum CurrentFloor { get; set; }

        private bool goHome = false;
        private Random rand = new Random();
        private readonly object @lock = new object();

        private enum Action { WalkAround, CallElevator, GoHome };

        public Agent(string name, SecurityLevelEnum securityLevel, Elevator elevator)
        {
            Name = name;
            SecurityLevel = securityLevel;
            Elevator = elevator;

            CurrentFloor = FloorEnum.G;
        }

        public void MakeAction()
        {
            Console.WriteLine($"{Name} walks into the building.");
            Thread.Sleep(2000);

            while (!goHome)
            {
                var action = GetRandomAction();
                switch (action)
                {
                    case Action.WalkAround:
                        Console.WriteLine($"{Name} is walking around.");
                        Thread.Sleep(2000);
                        break;

                    case Action.CallElevator:
                        Console.WriteLine($"{Name} calls the elevator.");
                        Thread.Sleep(2000);
                        UseElevator();
                        break;

                    case Action.GoHome:
                        GoHome();
                        break;

                    default:
                        throw new NotSupportedException("This action is not supported.");
                }
            }
        }

        private void UseElevator()
        {
            lock (@lock)
            {
                Task callElevator = new Task(() =>
                {
                    Console.WriteLine($"{Name} called the elevator.");
                    Elevator.CallElevator(CurrentFloor);
                    Console.WriteLine($"{Name} is in the elevator.");
                });

                callElevator.Start();

                if (goHome)
                {
                    Task GoHome = callElevator.ContinueWith((prevTask) =>
                    {
                        Elevator.MoveToFloor(Name, SecurityLevel, 0);
                    });
                    GoHome.Wait();
                }
                else
                {
                    Task selectRandomFloor = callElevator.ContinueWith((prevTask) =>
                    {
                        bool validFloor = false;
                        while (!validFloor)
                        {
                            CurrentFloor = (FloorEnum)rand.Next(4);
                            validFloor = Elevator.MoveToFloor(Name, SecurityLevel, CurrentFloor);
                        }
                    });
                    selectRandomFloor.Wait();
                }

                Console.WriteLine($"{Name} left the elevator.");
                Thread.Sleep(3000);
            }
        }

        private void GoHome()
        {
            goHome = true;
            if (CurrentFloor != 0)
            {
                Console.WriteLine($"{Name} wants to go home and goes to the elevator.");
                UseElevator();
            }
            Console.WriteLine($"{Name} is going home.");
        }

        private Action GetRandomAction()
        {
            return (Action)rand.Next(3);
        }
    }
}
