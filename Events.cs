using System;
using UnityEngine.SceneManagement;

namespace Astrum.AstralCore
{
    public static class Events
    {
        public static Action OnAppStart;
        public static Action OnUIInit;
        public static Action<Scene> OnWorldLoad;

        public static Action<Types.Player> OnPlayerJoined;
        public static Action<Types.Player> OnPlayerLeft;

        public static Action OnUpdate;
    }
}
