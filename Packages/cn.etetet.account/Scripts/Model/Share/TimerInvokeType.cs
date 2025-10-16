namespace ET
{
    public static partial class TimerInvokeType
    {
        public const int SessionIdleChecker = PackageType.Account * 1000 + 1;
        public const int SessionAcceptTimeout = PackageType.Account * 1000 + 2;
        
        public const int AccountSessionCheckOutTime = PackageType.Account * 1000 + 3;

        public const int PlayerOfflineOutTime = PackageType.Account * 1000 + 4;
        
    }
}