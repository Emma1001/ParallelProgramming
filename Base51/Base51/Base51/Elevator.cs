using System;
using System.Threading;

namespace Base51
{
    class Elevator
    {
        public FloorEnum CurrentFloor { get; set; }
        private bool isDoorOpen;

        public Elevator()
        {
            CurrentFloor = FloorEnum.G;
        }

        public void CallElevator(FloorEnum requestedFloor)
        {
            if (requestedFloor == CurrentFloor)
            {
                Console.WriteLine("The elevator is already on this floor.");
                OpenDoors();
            }
            else
            {
                Console.WriteLine($"The elevator is moving to floor: {requestedFloor}.");
                int moving = Math.Abs(CurrentFloor - requestedFloor) * 1000;
                Thread.Sleep(moving);
                CurrentFloor = requestedFloor;
                OpenDoors();
            }
        }

        public bool MoveToFloor(string name, SecurityLevelEnum securityLevel, FloorEnum floor)
        {
            if (CurrentFloor == floor)
            {
                if (isDoorOpen)
                {
                    CloseDoors();
                }
                Console.WriteLine("The elevator is already on this floor.");

                if (CanAccessFloor(CurrentFloor, securityLevel))
                {
                    OpenDoors();
                    return true;
                }
                Console.WriteLine($"{name} cannot access this floor.");
                return false;
            }
            else
            {
                if (isDoorOpen)
                {
                    CloseDoors();
                }
                Console.WriteLine($"The elevator is moving to floor: {floor}.");

                int moving = Math.Abs(CurrentFloor - floor) * 1000;
                Thread.Sleep(moving);
                CurrentFloor = floor;

                if (CanAccessFloor(CurrentFloor, securityLevel))
                {
                    OpenDoors();
                    return true;
                }
                Console.WriteLine($"{name} cannot access this floor.");
                return false;
            }
        }

        private void OpenDoors()
        {
            Console.WriteLine("The doors are opening...");
            Thread.Sleep(3000);
            isDoorOpen = true;
        }

        private void CloseDoors()
        {
            Console.WriteLine("The doors are closing...");
            Thread.Sleep(3000);
            isDoorOpen = false;
        }

        private bool CanAccessFloor(FloorEnum floor, SecurityLevelEnum agent)
        {
            bool canAccess = false;
            switch(agent)
            {
                case SecurityLevelEnum.Confidential:
                    if (FloorEnum.G == floor)
                        canAccess = true;
                    break;
                case SecurityLevelEnum.Secret:
                    if (FloorEnum.G == floor || FloorEnum.S == floor)
                        canAccess = true;
                    break;
                case SecurityLevelEnum.TopSecret:
                        canAccess = true;
                    break;
                default:
                    canAccess = false;
                    break;
            }

            return canAccess;
        }
    }
}
