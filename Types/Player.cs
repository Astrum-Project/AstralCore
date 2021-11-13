using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VRC.Core;

namespace Astrum.AstralCore.Types
{
    public class Player
    {
        public static readonly Type Type;

        private static readonly PropertyInfo m_APIUser;

        static Player()
        {
            Type = Hooks.Hooks.AssemblyCSharp.GetExportedTypes()
                .Where(f => f.Namespace == "VRC")
                .OrderByDescending(f => f.GetProperties().Count(f1 => f1.PropertyType == typeof(bool)))
                .FirstOrDefault();

            m_APIUser = Type.GetProperties()
                .FirstOrDefault(f => f.PropertyType == typeof(APIUser));
        }

        public readonly MonoBehaviour Inner;
        public readonly APIUser APIUser;

        public Player(MonoBehaviour inner)
        {
            Inner = inner;
            APIUser = m_APIUser?.GetValue(inner) as APIUser;
        }
    }
}
