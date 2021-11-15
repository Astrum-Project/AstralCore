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
                onExecute = new Func<string[], string>(args => "Reserved for future use")
            }.Register("list", "help");
        }

        public static Dictionary<string, Command> commands = new Dictionary<string, Command>(StringComparer.OrdinalIgnoreCase);

        public static string Execute(string raw)
        {
            raw.Trim();
            string[] tokens = raw.Split(' ');

            if (!commands.ContainsKey(tokens[0])) return "Unknown command";

            return commands[tokens[0]].onExecute(tokens.Skip(1).ToArray());
        }

        public static void Unregister(string name) => commands.Remove(name);

        public class Command
        {
            public string module = "";
            public Func<string[], string> onExecute;

            public void Register(string name) => commands.Add(name, this);
            public void Register(params string[] aliases) => aliases.ToList().ForEach(f => Register(f));
            public void Unregister() => commands.Where(f => f.Value == this).Select(f => f.Key).ToList().ForEach(f => CommandManager.Unregister(f));
        }

        public class ConVar
        {
            public Action<object> onChange;
            private Command command;

            public ConVar(Action<object> onChange, Func<string, object> tryParse) 
            {
                this.onChange = onChange;

                command = new Command
                {
                    onExecute = new Func<string[], string>(args =>
                    {
                        object res = tryParse(string.Join(" ", args));
                        if (res is null) return "Invalid input";
                        
                        onChange(res);
                        return "";
                    })
                };
            }

            public void Register(string name) => command.Register(name);
            public void Register(params string[] aliases) => command.Register(aliases);
            public void Unregister() => command.Unregister();

            public static Func<string, object> TryParseBool = new Func<string, object>(s =>
            {
                if (bool.TryParse(s, out bool result)) return result;
                return null;
            });
            public static Func<string, object> TryParseInt = new Func<string, object>(s =>
            {
                if (int.TryParse(s, out int result)) return result;
                return null;
            });
            public static Func<string, object> TryParseFloat = new Func<string, object>(s =>
            {
                if (float.TryParse(s, out float result)) return result;
                return null;
            });
            public static Func<string, object> TryParseString = new Func<string, object>(s => s);

        }
    }
}
