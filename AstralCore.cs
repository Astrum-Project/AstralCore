using MelonLoader;
using System;

[assembly: MelonInfo(typeof(Astrum.AstralCore.Loader), "AstralCore", "0.1.0", downloadLink: "github.com/Astrum-Project/AstralCore")]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonColor(ConsoleColor.DarkMagenta)]

namespace Astrum.AstralCore
{
    public class Loader : MelonMod { }
}
