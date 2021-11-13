using System;

namespace Astrum.AstralCore
{
    public static class Events
    {
        public static Action OnUpdate = new Action(() => { });
        public static Action<Hooks.OnRPC.RPCData> OnRPC = new Action<Hooks.OnRPC.RPCData>(_ => { });
        public static Action<Types.Player> OnPlayerJoined = new Action<Types.Player>(_ => { });
        public static Action<Types.Player> OnPlayerLeft = new Action<Types.Player>(_ => { });
    }
}
