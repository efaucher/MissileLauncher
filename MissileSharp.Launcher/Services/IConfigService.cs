﻿
namespace MissileSharp.Launcher.Services
{
    /// <summary>
    /// helper service, reads the config file and returns its content in a string[]
    /// </summary>
    public interface IConfigService
    {
        string ConfigFileName { get; }
        string[] GetConfig();
    }
}
