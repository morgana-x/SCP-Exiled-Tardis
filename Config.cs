﻿using Exiled.API.Interfaces;

namespace TardisPlugin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;
    }
}