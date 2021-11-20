using MelonLoader.TinyJSON;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Astrum.AstralCore.Managers
{
    public static class CommandManager
    {
        static CommandManager()
        {
            new Command
            {
                onExecute = new Func<string[], string>(args => args.ToString())
            }.Register("echo");

            new Command
            {
                onExecute = new Func<string[], string>(_ => {
                    Environment.Exit(0);
                    return "";
                })
            }.Register("exit", "quit", "abort");

            new Command
            {
                onExecute = new Func<string[], string>(_ => string.Join("\0", ModuleManager.modules.Select(f => f.Key)))
            }.Register("Get-Modules", "help");

            new Command
            {
                onExecute = new Func<string[], string>(args =>
                {
                    if (!ModuleManager.modules.TryGetValue(String.Join(" ", args), out ModuleManager.Module module))
                        return "";
                    return string.Join("\0", module.commands.Select(f => f.Key));
                })
            }.Register("Get-Commands", "list");
        }

        public static Dictionary<string, Command> commands
        {
            get => coreModule.commands;
        }

        private static ModuleManager.Module coreModule = new ModuleManager.Module("Core");

        public static string Execute(string raw)
        {
            string[] tokens = raw.Trim().Split(' ');

            if (!ModuleManager.modules.TryGetValue(tokens[0], out ModuleManager.Module module))
                return "Unknown Module";

            if (!module.commands.TryGetValue(tokens[1], out Command command))
                return "Unknown Command";

            return command.onExecute(tokens.Skip(2).ToArray());
        }

        public static void Unregister(string name) => commands.Remove(name);

        public class Command
        {
            public Command() { }
            public Command(Func<string[], string> onExecute) => this.onExecute = onExecute;

            public Func<string[], string> onExecute;

            public void Register(string name) => commands.Add(name, this);
            public void Register(params string[] aliases) => aliases.ToList().ForEach(f => Register(f));
            public void Register(ModuleManager.Module module, string name) => module.Register(this, name);
            public void Register(ModuleManager.Module module, params string[] aliases) => aliases.ToList().ForEach(f => Register(module, f));
            public void Unregister() => commands.Where(f => f.Value == this).Select(f => f.Key).ToList().ForEach(f => CommandManager.Unregister(f));
        }

        public class ConVar<T> : Command
        {
            public Action<T> onChange;

            public ConVar(Action<T> onChange)
            {
                this.onChange = onChange;

                onExecute = new Func<string[], string>(args =>
                {
                    onChange(Decoder.Decode(string.Join(" ", args)).Make<T>());

                    return "";
                });
            }
        }
    }
}
