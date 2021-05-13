namespace AioCore.Shared
{
    public static class Message
    {
        public const string SignInMessageSuccess = "SignIn.Message.Success";
        public const string SignInMessageFail = "SignIn.Message.Fail";

        public const string SignUpMessageSuccess = "SignUp.Message.Success";
        public const string SignUpMessageFail = "SignUp.Message.Fail";
        public const string SignUpMessagePasswordNotMatch = "SignUp.Message.PasswordNotMatch";

        public const string SecurityUserUpdateMessageSuccess = "SecurityUser.Update.Message.Success";
        public const string SecurityUserUpdateMessageFail = "SecurityUser.Update.Message.Fail";
        public const string SecurityUserDeleteMessageSuccess = "SecurityUser.Delete.Message.Success";
        public const string SecurityUserDeleteMessageFail = "SecurityUser.Delete.Message.Fail";
    }
}