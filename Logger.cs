namespace Astrum.AstralCore
{
    public static class Logger
    {
        public static void Trace(object obj) => Managers.LogManager.OnTrace(obj.ToString());
        public static void Debug(object obj) => Managers.LogManager.OnDebug(obj.ToString());
        public static void Info(object obj) => Managers.LogManager.OnInfo(obj.ToString());
        public static void Notif(object obj) => Managers.LogManager.OnNotif(obj.ToString());
        public static void Warn(object obj) => Managers.LogManager.OnWarn(obj.ToString());
        public static void Error(object obj) => Managers.LogManager.OnError(obj.ToString());
        public static void Fatal(object obj) => Managers.LogManager.OnFatal(obj.ToString());
    }
}
