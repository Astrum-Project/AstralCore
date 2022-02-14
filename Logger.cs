global using Logger = Astrum.AstralCore.Logger;

using System;

namespace Astrum.AstralCore
{
    public static class Logger
    {
        public static void Trace(object obj) => OnTrace?.Invoke(obj.ToString());
        public static void Debug(object obj) => OnDebug?.Invoke(obj.ToString());
        public static void Info(object obj) => OnInfo?.Invoke(obj.ToString());
        public static void Notif(object obj) => OnNotif?.Invoke(obj.ToString());
        public static void Warn(object obj) => OnWarn?.Invoke(obj.ToString());
        public static void Error(object obj) => OnError?.Invoke(obj.ToString());
        public static void Fatal(object obj) => OnFatal?.Invoke(obj.ToString());

        public static event Action<string> OnTrace;
        public static event Action<string> OnDebug;
        public static event Action<string> OnInfo;
        public static event Action<string> OnNotif;
        public static event Action<string> OnWarn;
        public static event Action<string> OnError;
        public static event Action<string> OnFatal;
    }
}
