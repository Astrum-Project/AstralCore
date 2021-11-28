using System;
using System.Linq;
using System.Reflection;

namespace Astrum.AstralCore.Managers
{
    public static class SelectionManager
    {
        public static Type UserSelectionManager_T;
        public static PropertyInfo m_Instance;
        public static PropertyInfo m_APIUser0;
        public static PropertyInfo m_APIUser1;

        public static object Instance;

        static SelectionManager()
        {
            UserSelectionManager_T = Hooks.Hooks.AssemblyCSharp.GetTypes()
                .Where(t => t.BaseType == typeof(UnityEngine.MonoBehaviour))
                .FirstOrDefault(t => t.Namespace == "VRC.DataModel");

            m_Instance = UserSelectionManager_T.GetProperties().FirstOrDefault(x => x.PropertyType == UserSelectionManager_T);
            Instance = m_Instance.GetValue(null);

            PropertyInfo[] m_APIUsers = UserSelectionManager_T.GetProperties().Where(x => x.PropertyType == typeof(VRC.Core.APIUser)).ToArray();
            m_APIUser0 = m_APIUsers[0]; // im not entirely sure is caching these is any more optimized
            m_APIUser1 = m_APIUsers[1];
        }

        public static Types.Player TargetPlayer;
        public static VRC.Core.APIUser SelectedPlayer
        {
            get => TargetPlayer?.APIUser ?? InGameSelectedPlayer;
        }

        public static VRC.Core.APIUser InGameSelectedPlayer
        {
            // if you know a reliable way to get which of these isn't dead,
            // please make an issue or a pull request
            get => (VRC.Core.APIUser)(m_APIUser0.GetValue(Instance) ?? m_APIUser1.GetValue(Instance));
        }
    }
}
