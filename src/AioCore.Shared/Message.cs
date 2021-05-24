namespace AioCore.Shared
{
    public static class Message
    {
        public const string SignInMessageSuccess = "SignIn.Message.Success";
        public const string SignInMessageFail = "SignIn.Message.Fail";

        public const string SignUpMessageSuccess = "SignUp.Message.Success";
        public const string SignUpMessageFail = "SignUp.Message.Fail";
        public const string SignUpMessagePasswordNotMatch = "SignUp.Message.PasswordNotMatch";

        public const string SystemUserUpdateMessageSuccess = "SystemUser.Update.Message.Success";
        public const string SystemUserUpdateMessageFail = "SystemUser.Update.Message.Fail";
        public const string SystemUserDeleteMessageSuccess = "SystemUser.Delete.Message.Success";
        public const string SystemUserDeleteMessageFail = "SystemUser.Delete.Message.Fail";
    }
}