using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

            Logger.Debug($"{nameof(VRCNetworkingClient)}={Type}");

            m_OpRaiseEvent = Type.GetMethods()
                .Where(x => x.DeclaringType == Type.BaseType)
                .Where(x => x.ReturnType == typeof(bool))
                .FirstOrDefault(x =>
                {
                    var args = x.GetParameters();
                    return args.Length == 4
                    && args[0].ParameterType == typeof(byte)
                    && args[1].ParameterType == typeof(Il2CppSystem.Object)
                    && !args[2].ParameterType.IsValueType
                    && args[3].ParameterType.IsValueType;
                });

            Logger.Debug($"{nameof(VRCNetworkingClient)}::{nameof(OpRaiseEvent)}={m_OpRaiseEvent}");

            m_Instance = Type.GetProperties()
                .FirstOrDefault(x => x.PropertyType == m_Instance);

            Logger.Debug($"{nameof(VRCNetworkingClient)}::{nameof(Instance)}={m_Instance}");
        }

        // TODO: bake the MethodInfo to a delegate and cache the Instance
        public static bool OpRaiseEvent(byte eventCode, object customEventContent, object raiseEventOptions, object sendOptions) =>
            (bool)m_OpRaiseEvent.Invoke(m_Instance.GetValue(null), new object[] { eventCode, customEventContent, raiseEventOptions, sendOptions });
    }
}
