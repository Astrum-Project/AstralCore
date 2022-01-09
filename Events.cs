using System;

namespace Astrum.AstralCore
{
    public static class Events
    {
        public static Action OnUpdate = new(() => { });
        public static Action OnUIInit = new(() => { });
        public static Action<Types.Player> OnPlayerJoined = new(_ => { });
        public static Action<Types.Player> OnPlayerLeft = new(_ => { });
    }
}
