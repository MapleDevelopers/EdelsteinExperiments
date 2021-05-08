namespace Edelstein.Service.Login.Types
{
    public enum LoginResultCode : byte
    {
        ProcFail = byte.MaxValue,
        Success = 0x0,
        TempBlocked = 0x1,
        Blocked = 0x2,
        Abandoned = 0x3, // CheckPassResult: This is an ID that has been deleted or blocked from connection
        IncorrectPassword = 0x4, // CheckPassResult: This is an inccorrect password
        NotRegistered = 0x5, // CheckPassResult: This is not a registred ID
        DBFail = 0x6, // CheckPassResult: Trouble logging in? Try loggin in via nexon website blabla...
        AlreadyConnected = 0x7, // CheckPassResult: This is an ID that is already logged in, or the server in under inspection
        NotConnectableWorld = 0x8, // CheckPassResult: Trouble logging in? Try loggin in via nexon website blabla...
        Unknown = 0x9, // CheckPassResult: Trouble logging in? Try loggin in via nexon website blabla...
        Timeout = 0xA, // CheckPassResult: Could not be processed due to too many connection requests to the server
        NotAdult = 0xB, // CheckPassResult: Only those who are 20 years old or older can use this.
        AuthFail = 0xC, // ?? crash on check pass
        ImpossibleIP = 0xD, // CheckPassResult: Unable to log-on as a master at IP
        NotAuthorizedNexonID = 0xE, // CheckPassResult: You have either selected wrong gateway, or you have yet to change your personal information
        NoNexonID = 0xF, // CheckPassResult: Were still processing your request at this time, so you dont have access to this game fow now.
        NotAuthorized = 0x10, // CheckPassResult: Please verify your account via email in order to play the game.
        InvalidRegionInfo = 0x11, // CheckPassResult: You have either selected wrong gateway, or you have yet to change your personal information
        InvalidBirthDate = 0x12, // CheckPassResult: does nothing, DeleteCharResult: ??
        PassportSuspended = 0x13, // CheckPassResult: does nothing, DeleteCharResult: ??
        IncorrectSSN2 = 0x14, // CheckPassResult: does nothing, DeleteCharResult: ??
        WebAuthNeeded = 0x15, // CheckPassResult: Please verify your account via email in order to play the game.
        DeleteCharacterFailedOnGuildMaster = 0x16, // CheckPassResult: does nothing, DeleteCharResult: ??
        NotagreedEULA = 0x17, // CheckPassResult: shows eula agreement --> Unhandled packet operation ConfirmEULA
        DeleteCharacterFailedEngaged = 0x18, // CheckPassResult: does nothing, DeleteCharResult: ??
        RegisterLimitedIP = 0x19, // CheckPassResult: Youre logging in from outside of the service region
        RequestedCharacterTransfer = 0x1A, // CheckPassResult: does nothing, DeleteCharResult: ??
        CashUserCannotUseSimpleClient = 0x1B, // CheckPassResult: Please download the full client to experience the world of MapleStory. Would you like to download the full client from our website?
        //0x1C CheckPassResult: does nothing, DeleteCharResult: ??
        DeleteCharacterFailedOnFamily = 0x1D, // CheckPassResult: does nothing, DeleteCharResult: ??
        InvalidCharacterName = 0x1E,  // CheckPassResult: does nothing, DeleteCharResult: ??
        IncorrectSSN = 0x1F, // CheckPassResult: does nothing, DeleteCharResult: ??
        SSNConfirmFailed = 0x20, // CheckPassResult: does nothing, DeleteCharResult: ??
        SSNNotConfirmed = 0x21, // CheckPassResult: does nothing, DeleteCharResult: ??
        WorldTooBusy = 0x22, // CheckPassResult: does nothing, DeleteCharResult: ??
        OTPReissuing = 0x23, // CheckPassResult: does nothing, DeleteCharResult: ??
        OTPInfoNotExist = 0x24, // CheckPassResult: does nothing, DeleteCharResult: ??

        // Todo: more research
        IncorrectSPW = 0x14,
        SamePasswordAndSPW = 0x16,
        SamePincodeAndSPW = 0x17,
    }
}