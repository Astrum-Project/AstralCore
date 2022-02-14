using System;

namespace Astrum.AstralCore.Utils
{
    public static class LogType
    {
        public static void Self(string name, Type type)
        {
            if (type is null)
                Logger.Warn($"Failed to find {name}");
            else Logger.Debug($"{name}={type.Name}");
        }
    }
}
