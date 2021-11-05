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
                onExecute = new Func<string[], string>(args => "Reserved for future use")
            }.Register("get", "set", "list", "help");
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
            public Func<string[], string> onExecute;

            public void Register(string name) => commands.Add(name, this);
            public void Register(params string[] aliases) => aliases.ToList().ForEach(f => Register(f));
            public void Unregister() => commands.Where(f => f.Value == this).Select(f => f.Key).ToList().ForEach(f => CommandManager.Unregister(f));
        }
    }
}
