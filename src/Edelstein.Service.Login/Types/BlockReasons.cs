namespace Edelstein.Service.Login.Types
{
    public enum BlockReasons
    {
        NoReason = 0x0,
        Hacking = 0x1,
        Macroing = 0x2,
        Advertising = 0x3,
        Harassment = 0x4,
        Cursing = 0x5,
        Scamming = 0x6,
        Misconduct = 0x7,
        IllegalCash = 0x8,
        IllegalCharging = 0x9,
        TemporaryRequest = 0x10,
        ImpersonatingGM = 0x11,
        IllegalPrograms = 0x12,
        MegaphonesAbuse = 0x13
    }
}