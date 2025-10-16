namespace ET.Server
{
    public static partial class LocationType
    {
        public const int Unit = ET.PackageType.Account * 1000 + 1;
        public const int Player = ET.PackageType.Account * 1000 + 2;
        public const int GateSession = ET.PackageType.Account * 1000 + 3;
        public const int Mail = ET.PackageType.Account * 1000 + 4;
    }
}