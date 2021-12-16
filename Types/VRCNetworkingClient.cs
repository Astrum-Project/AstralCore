using System;
using System.Linq;
using System.Reflection;

namespace Astrum.AstralCore.Types
{
    public static class VRCNetworkingClient
    {
        public static Type Type;
        public static object Instance;

        public static MethodInfo m_OpRaiseEvent;
        public static PropertyInfo m_Instance;

        static VRCNetworkingClient()
        {
            Type = Hooks.Hooks.AssemblyCSharp.GetExportedTypes()
                .Where(x => !x.IsGenericType)
                .Where(x => x.DeclaringType == null)
                .Where(x => x.BaseType?.BaseType == typeof(Il2CppSystem.Object))
                .FirstOrDefault(x => x.Namespace == "VRC.Core");

            if (Type is null)
            {
                Logger.Warn($"Failed to find {nameof(VRCNetworkingClient)}");
                return;
            } else Logger.Debug($"{nameof(VRCNetworkingClient)}={Type}");

            m_OpRaiseEvent = Type.GetMethods()
                .Where(x => x.DeclaringType == Type.BaseType)
                .Where(x => x.ReturnType == typeof(bool))
                .FirstOrDefault(x =>
                {
                    ParameterInfo[] args = x.GetParameters();
                    return args.Length == 4
                    && args[0].ParameterType == typeof(byte)
                    && args[1].ParameterType == typeof(Il2CppSystem.Object)
                    && !args[2].ParameterType.IsValueType
                    && args[3].ParameterType.IsValueType;
                });

            if (m_OpRaiseEvent is null)
                Logger.Warn($"Failed to find {nameof(VRCNetworkingClient)}::{nameof(OpRaiseEvent)}");
            else Logger.Debug($"{nameof(VRCNetworkingClient)}::{nameof(OpRaiseEvent)}={m_OpRaiseEvent}");

            m_Instance = Type.GetProperties()
                .FirstOrDefault(x => x.PropertyType == Type);

            if (m_Instance is null)
                Logger.Warn($"Failed to find {nameof(VRCNetworkingClient)}::{nameof(Instance)}");
            else
            {
                Logger.Debug($"{nameof(VRCNetworkingClient)}::{nameof(Instance)}={m_Instance}");

                Instance = m_Instance.GetValue(null);
                if (Instance is null) 
                    MelonLoader.MelonCoroutines.Start(GetInstance());
            }
        }

        // instance is created right after OnApplicationStart
        private static System.Collections.IEnumerator GetInstance()
        {
            yield return null;
            Instance = m_Instance.GetValue(null);
        }

        // TODO: bake the MethodInfo to a delegate
        public static bool OpRaiseEvent(byte eventCode, object customEventContent, object raiseEventOptions, object sendOptions) =>
            (bool)m_OpRaiseEvent.Invoke(Instance, new object[] { eventCode, customEventContent, raiseEventOptions, sendOptions });
    }
}
