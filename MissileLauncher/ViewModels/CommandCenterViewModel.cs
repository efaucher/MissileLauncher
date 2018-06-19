using System;
using MissileSharp;

namespace MissileLauncher
{
    public class CommandCenterViewModel
    {
        private readonly ICommandCenter _launcher;
        private int _positionX = 0;
        private int _positionY = 0;

        public CommandCenterViewModel()
        {
            var launcherModel = LauncherModelFactory.GetLauncher("MissileSharp.ThunderMissileLauncher");
            _launcher = new CommandCenter(launcherModel);
        }

        public async void Reset()
        {
            await _launcher.Reset();
            _positionX = 0;
            _positionY = 0;
        }

        public async void Fire()
        {
            await _launcher.Fire(1);
        }

        public void MoveLauncher(int positionX, int positionY)
        {
            int movementX = positionX - _positionX;
            int movementY = positionY - _positionY;

            _positionX += movementX;
            _positionY += movementY;

            if (movementX > 0) // Move right
            {
                MoveRight(movementX);
            }
            else if (movementX < 0)// Move Left
            {
                movementX = Math.Abs(movementX);
                MoveLeft(movementX);
            }

            if (movementY > 0)
            {
                MoveUp(movementY);
            }
            else if (movementY < 0)
            {
                movementY = Math.Abs(movementY);
                MoveDown(movementY);
            }
        }

        private async void MoveRight(int movement)
        {
            await _launcher.Right(movement);
        }

        private async void MoveLeft(int movement)
        {
            await _launcher.Left(movement);
        }

        private async void MoveUp(int movement)
        {
            await _launcher.Up(movement);
        }

        private async void MoveDown(int movement)
        {
            await _launcher.Down(movement);
        }

        public async void MoveRight()
        {
            await _launcher.Right();
        }

        public async void MoveUp()
        {
            await _launcher.Up();
        }

        public async void MoveLeft()
        {
            await _launcher.Left();
        }

        public async void MoveDown()
        {
            await _launcher.Down();
        }

        public async void Stop()
        {
            await _launcher.Stop();
        }
    }
}
