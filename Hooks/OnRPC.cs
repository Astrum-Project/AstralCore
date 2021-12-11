using MelonLoader;
using System.Linq;
using System.Reflection;
using VRC.SDKBase;

namespace Astrum.AstralCore.Hooks
{
    public static class OnRPC
    {
        private static MethodInfo m_OnRPC;

        public static void Initialize(HarmonyLib.Harmony harmony)
        {
            m_OnRPC = Hooks.AssemblyCSharp
                .GetTypes()
                .Where(f => f.BaseType == typeof(VRC_EventDispatcher))
                .SelectMany(f => f.GetMethods())
                .Where(f => f.GetCustomAttribute<UnhollowerBaseLib.Attributes.CallerCountAttribute>()?.Count > 0)
                .FirstOrDefault(f =>
                {
                    ParameterInfo[] p = f.GetParameters();

                    return p.Length == 5 &&
                    // 0 is VRC.Player, an obfuscated type
                    p[1].ParameterType == typeof(VRC_EventHandler.VrcEvent) &&
                    p[2].ParameterType == typeof(VRC_EventHandler.VrcBroadcastType) &&
                    p[3].ParameterType == typeof(int) &&
                    p[4].ParameterType == typeof(float);
                });

            if (m_OnRPC != null)
            {
                Logger.Debug("EventHandler=" + m_OnRPC.DeclaringType.Name);
                Logger.Debug("EventHandler::OnRPC=" + m_OnRPC.Name);

                harmony.Patch(m_OnRPC, typeof(OnRPC).GetMethod(nameof(HookOnRPC), Hooks.PrivateStatic).ToNewHarmonyMethod());
            }
            else Logger.Warn("Failed to find EventHandler::OnRPC");
        }

        private static void HookOnRPC(object __0, VRC_EventHandler.VrcEvent __1, VRC_EventHandler.VrcBroadcastType __2, int __3, float __4) => 
            Events.OnRPC(new RPCData { sender = __0, eventType = __1, broadcastType = __2, __unk3 = __3, __unk4 = __4 });

        public class RPCData
        {
            public object sender;
            public VRC_EventHandler.VrcEvent eventType;
            public VRC_EventHandler.VrcBroadcastType broadcastType;
            public int __unk3;
            public float __unk4;
        }
    }
}
