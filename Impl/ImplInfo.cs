namespace Astrum.AstralCore
{
    public static class ImplInfo
    {
        public const string Version = "8.1.0";
        public const string GUID = "com.github.astrum-project.astralcore";

        public static Implementation ImplType;

        public enum Implementation
        {
            Unknown = 0,
            MelonLoader = 1,
            BepInEx = 2,
        }
    }
}
