using System;
using System.Collections.Generic;
using System.Threading;
using MissileSharp.Properties;
using System.Threading.Tasks;

namespace MissileSharp
{
    /// <summary>
    /// Controls an USB missile launcher
    /// </summary>
    public class CommandCenter : ICommandCenter, IDisposable
    {
        IHidDevice device;
        ILauncherModel launcher;
        CommandSetList sets;

        /// <summary>
        /// Initializes a new instance of the CommandCenter class using the specified missile launcher model.
        /// </summary>
        /// <param name="launcher">missile launcher model you want to control</param>
        public CommandCenter(ILauncherModel launcher) : this(launcher, new HidLibraryDevice())
        {
        }

        /// <summary>
        /// Initializes a new instance of the CommandCenter class using the specified missile launcher model and HID library implementation.
        /// </summary>
        /// <param name="launcher">missile launcher model you want to control</param>
        /// <param name="device">HID library that will be used</param>
        /// <remarks>This is only for testing - HidLibrary is the default library for production use</remarks>
        internal CommandCenter(ILauncherModel launcher, IHidDevice device)
        {
            if (launcher == null)
            {
                throw new ArgumentException(Resources.LauncherIsNull);
            }

            if (device == null)
            {
                throw new ArgumentException(Resources.DeviceIsNull);
            }

            this.launcher = launcher;
            this.device = device;
            this.sets = new CommandSetList();

            device.Initialize(launcher.VendorId, launcher.DeviceId);
        }

        /// <summary>
        /// The name of the device
        /// </summary>
        public string LauncherModelName
        {
            get
            {
                return this.launcher.ModelName;
            }
        }

        /// <summary>
        /// The device is ready to receive commands
        /// </summary>
        public bool IsReady
        {
            get
            {
                return device.IsReady;
            }
        }

        /// <summary>
        /// Runs a LauncherCommand
        /// </summary>
        /// <param name="command">The command to run</param>
        public async Task RunCommand(LauncherCommand command)
        {
            if (!IsReady)
            {
                throw new InvalidOperationException(Resources.DeviceIsNotReady);
            }

            switch (command.Command)
            {
                case Command.Up:
                    await Up(command.Value);
                    break;
                case Command.Down:
                    await Down(command.Value);
                    break;
                case Command.Left:
                    await Left(command.Value);
                    break;
                case Command.Right:
                    await Right(command.Value);
                    break;
                case Command.Reset:
                    await Reset();
                    break;
                case Command.Fire:
                    await Fire(command.Value);
                    break;
            }
        }

        /// <summary>
        /// Runs a list of LauncherCommands
        /// </summary>
        /// <param name="commands">The list of commands to run</param>
        public async Task RunCommandSet(IEnumerable<LauncherCommand> commands)
        {
            foreach (var cmd in commands)
            {
                await RunCommand(cmd);
            }
        }

        /// <summary>
        /// Runs a list of LauncherCommands by name (commands must have been loaded before using LoadCommandSets)
        /// </summary>
        /// <param name="commandSetName">The name of the command set to run</param>
        public async Task RunCommandSet(string commandSetName)
        {
            if (sets.CountSets() == 0)
            {
                throw new InvalidOperationException(Resources.NoCommandSets);
            }

            if (sets.ContainsCommandSet(commandSetName))
            {
                await RunCommandSet(sets.GetCommandSet(commandSetName));
            }
        }

        /// <summary>
        /// Loads a list of command sets from a config file (execute command sets with RunCommandSet)
        /// </summary>
        /// <param name="pathToConfigFile">Complete path to the config file</param>
        /// <returns>True if at least one command set was loaded</returns>
        public bool LoadCommandSets(string pathToConfigFile)
        {
            sets = ConfigReader.Read(pathToConfigFile);
            return (sets.CountSets() > 0);
        }

        /// <summary>
        /// Loads a list of command sets from a config file (execute command sets with RunCommandSet)
        /// </summary>
        /// <param name="configFileLines">The lines of the config file (after loading the file with File.ReadAllLines)</param>
        /// <returns>True if at least one command set was loaded</returns>
        public bool LoadCommandSets(string[] configFileLines)
        {
            sets = ConfigReader.Read(configFileLines);
            return (sets.CountSets() > 0);
        }

        /// <summary>
        /// Gets a list with the names of all loaded command sets.
        /// </summary>
        /// <returns>A list of command set names</returns>
        public List<string> GetLoadedCommandSetNames()
        {
            if (sets.CountSets() > 0)
            {
                return sets.GetCommandSetNames();
            }

            return new List<string>();
        }

        /// <summary>
        /// Move up for X milliseconds
        /// </summary>
        /// <param name="milliseconds">Time to move</param>
        public async Task<ICommandCenter> Up(int milliseconds)
        {
            await SendMoveCommand(launcher.Up, milliseconds);
            return this;
        }

        /// <summary>
        /// Move down for X milliseconds
        /// </summary>
        /// <param name="milliseconds">Time to move</param>
        public async Task<ICommandCenter> Down(int milliseconds)
        {
            await SendMoveCommand(launcher.Down, milliseconds);
            return this;
        }

        /// <summary>
        /// Turn left for X milliseconds
        /// </summary>
        /// <param name="milliseconds">Time to move</param>
        public async Task<ICommandCenter> Left(int milliseconds)
        {
            await SendMoveCommand(launcher.Left, milliseconds);
            return this;
        }

        /// <summary>
        /// Turn right for X milliseconds
        /// </summary>
        /// <param name="milliseconds">Time to move</param>
        public async Task<ICommandCenter> Right(int milliseconds)
        {
            await SendMoveCommand(launcher.Right, milliseconds);
            return this;
        }

        /// <summary>
        /// Reset the launcher position (=move to bottom left)
        /// </summary>
        public async Task<ICommandCenter> Reset()
        {
            await Down(launcher.ResetTimeDown);
            await Left(launcher.ResetTimeLeft);
            return this;
        }

        /// <summary>
        /// Fire X missiles
        /// </summary>
        /// <param name="numberOfShots">Number of missiles to fire</param>
        public async Task<ICommandCenter> Fire(int numberOfShots)
        {
            if (numberOfShots < launcher.MinNumberOfShots)
            {
                numberOfShots = launcher.MinNumberOfShots;
            }

            if (numberOfShots > launcher.MaxNumberOfShots)
            {
                numberOfShots = launcher.MaxNumberOfShots; 
            }

            Thread.Sleep(launcher.WaitBeforeFire);

            for (int i = 1; i <= numberOfShots; i++)
            {
                Task run = new Task(async () =>
                {
                    await SendCommand(launcher.Fire);
                    Thread.Sleep(launcher.WaitAfterFire);
                });

                run.Start();
                await run;
            }

            return this;
        }

        /// <summary>
        /// dispose the device
        /// </summary>
        public void Dispose()
        {
            if (IsReady)
            {
                device.Dispose();
            }
        }

        /// <summary>
        /// Send a command to the device
        /// </summary>
        /// <param name="command">The command to send</param>
        internal async Task SendCommand(byte command)
        {
            Task run = new Task(() => {
                var data = launcher.CreateCommand(command);
                device.SendData(data);
            });
            run.Start();
            await run;
        }

        /// <summary>
        /// Send a move command to the device, wait X milliseconds, then stop
        /// </summary>
        /// <param name="command">The command to send</param>
        /// <param name="milliseconds">Time to wait</param>
        internal async Task SendMoveCommand(byte command, int milliseconds)
        {
            if (IsReady)
            {
                await SendCommand(command);
                Thread.Sleep(milliseconds);
                await SendCommand(launcher.Stop);
            }
        }

        internal async Task SendMoveCommand(byte command)
        {
            if (IsReady)
            {
                await SendCommand(command);
            }
        }

        public async Task<ICommandCenter> Up()
        {
            await SendMoveCommand(launcher.Up);
            return this;
        }

        public async Task<ICommandCenter> Down()
        {
            await SendMoveCommand(launcher.Down);
            return this;
        }

        public async Task<ICommandCenter> Left()
        {
            await SendMoveCommand(launcher.Left);
            return this;
        }

        public async Task<ICommandCenter> Right()
        {
            await SendMoveCommand(launcher.Right);
            return this;
        }

        public async Task<ICommandCenter> Stop()
        {
            await SendMoveCommand(launcher.Stop);
            return this;
        }
    }
}
