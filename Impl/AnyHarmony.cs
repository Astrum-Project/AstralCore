extern alias ml;

using HarmonyLib;
using System.Reflection;

namespace Astrum.AstralCore.Impl
{
    public abstract class AnyHarmony
    {
        public abstract void Patch(MethodBase original, MethodInfo prefix = null, MethodInfo postfix = null, MethodInfo transpiler = null, MethodInfo finalizer = null, MethodInfo ilmanipulator = null);
    }
}
