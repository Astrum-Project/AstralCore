using MelonLoader;
using System;

namespace Astrum.AstralCore.Managers
{
    public static class LogManager
    {
        public static Action<string> OnTrace = new Action<string>(s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[0mTrace] \x1b[K" + s));
        public static Action<string> OnDebug = new Action<string>(s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[34mDebug\x1b[0m] \x1b[K" + s));
        public static Action<string> OnInfo  = new Action<string>(s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[36mInfo \x1b[0m] \x1b[K" + s));
        public static Action<string> OnNotif = new Action<string>(s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[36mNotif\x1b[0m] \x1b[K" + s)); // todo: show this one ingame
        public static Action<string> OnWarn  = new Action<string>(s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[33mWarn \x1b[0m] \x1b[K" + s));
        public static Action<string> OnError = new Action<string>(s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[31mError\x1b[0m] \x1b[K" + s));
        public static Action<string> OnFatal = new Action<string>(s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[31mFatal\x1b[0m] \x1b[K" + s));
    }
}
