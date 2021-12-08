using System;
using System.Collections.Generic;

namespace Astrum.AstralCore.Managers
{
    public static class ModuleManager
    {
        public static Dictionary<string, Module> modules = new Dictionary<string, Module>(StringComparer.OrdinalIgnoreCase);
        public static Action<string, Module> OnModuleRegistered = new Action<string, Module>((_, __) => { });

        public class Module
        {
            public Module(string name)
            {
                modules.Add(name, this);
                OnModuleRegistered(name, this);
            }

            public Dictionary<string, CommandManager.Command> commands = new Dictionary<string, CommandManager.Command>(StringComparer.OrdinalIgnoreCase);
            public Action<string, CommandManager.Command> OnCommandRegistered = new Action<string, CommandManager.Command>((_, __) => { });

            public void Register(string name, CommandManager.Command command)
            {
                commands.Add(name, command);
                OnCommandRegistered(name, command);
            }

            public void Register(CommandManager.Command command, params string[] names)
            {
                foreach (string name in names) 
                    Register(name, command);
            }
        }
    }
}
