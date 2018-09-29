
public static class EventManager {

    public delegate void Call();
    public delegate void CallArgs<T>(T arg0);

    // coin collect event
    public static event Call CrystalCollectEvent;

    public static void CrystalCollected() {
        if (CrystalCollectEvent != null)
            CrystalCollectEvent();
    }

    // platform climb event
    public static event CallArgs<int> PlatformClimbEvent;

    public static void PlatformClimbed(int platformNumber) {
        if (PlatformClimbEvent != null)
            PlatformClimbEvent(platformNumber);
    }

    // platform clear event
    public static event Call PlatformClearEvent;

    public static void PlatformClear() {
        if (PlatformClearEvent != null)
            PlatformClearEvent();
    }
}
