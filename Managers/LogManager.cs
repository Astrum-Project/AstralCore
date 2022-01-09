#pragma warning disable CS0618 // Type or member is obsolete

using MelonLoader;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

namespace Astrum.AstralCore.Managers
{
    public static class LogManager
    {
        static LogManager()
        {
            if (MelonDebug.IsEnabled() || Environment.CommandLine.ToLower().Contains("--astral.debug"))
            {
                OnTrace += s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[0mTrace] \x1b[K" + s);
                OnDebug += s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[34mDebug\x1b[0m] \x1b[K" + s);

                Logger.Trace("Logger loaded");
            }
        }

        private static Text log;
        private static readonly Queue<string> lines = new Queue<string>();

        internal static void Initialize()
        {
            GameObject gameObject = new("AstralLog");
            log = gameObject.AddComponent<Text>();

            gameObject.transform.SetParent(GameObject.Find("UserInterface/UnscaledUI/HudContent/Hud").transform, false);
            gameObject.transform.localPosition = new Vector3(15, 300);

            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 30);

            // raleway might look better here
            log.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            log.horizontalOverflow = HorizontalWrapMode.Wrap;
            log.verticalOverflow = VerticalWrapMode.Overflow;
            log.alignment = TextAnchor.UpperLeft;
            log.fontStyle = FontStyle.Bold;
            log.supportRichText = true;
            log.fontSize = 30;

            OnNotif += s => MelonCoroutines.Start(DisplayOnScreen(s, 3));
        }

        public static System.Collections.IEnumerator DisplayOnScreen(string message, float duration)
        {
            // this should sync us with the main thread
            // that way we won't have to lock lines whilst using it
            // unfortunately messages will be a frame late due to it
            yield return null;

            foreach (VRCPlayerApi player in VRCPlayerApi.AllPlayers)
                message = message.Replace(player.displayName, $"<color=#5ab2a8>{player.displayName}</color>");

            lines.Enqueue(message);
            log.text = string.Join("\n", lines);
            yield return new WaitForSecondsRealtime(duration);
            lines.Dequeue();
            log.text = string.Join("\n", lines);
        }

        public static Action<string> OnTrace = new(_ => {});
        public static Action<string> OnDebug = new(_ => {});
        public static Action<string> OnInfo  = new(s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[36mInfo \x1b[0m] \x1b[K" + s));
        public static Action<string> OnNotif = new(s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[36mNotif\x1b[0m] \x1b[K" + s));
        public static Action<string> OnWarn  = new(s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[33mWarn \x1b[0m] \x1b[K" + s));
        public static Action<string> OnError = new(s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[31mError\x1b[0m] \x1b[K" + s));
        public static Action<string> OnFatal = new(s => MelonLogger.Msg("\r[\x1b[35mAstral \x1b[31mFatal\x1b[0m] \x1b[K" + s));
    }
}
