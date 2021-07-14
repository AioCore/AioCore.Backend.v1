namespace AioCore.Shared.Constants
{
    public static class Messages
    {
        public const string SignInFail = "Đăng nhập thất bại";
        public const string SignInSuccess = "Đăng nhập thành công";

        public const string SignUpSuccess = "Đăng ký thành công";
        public const string SignUpFail = "Đăng ký thất bại: {0}";
        public const string SignUpPasswordNotMatch = "Mật khẩu không khớp";
        public const string SignUpAccountExisted = "Tài khoản {0} đã tồn tại";
        public const string SignUpEmailExisted = "Email {0} đã tồn tại";
        public const string SignUpTenantNotExists = "Thuê bao không tồn tại";

        public const string SystemUserUpdateSuccess = "Cập nhật thông tin người dùng thành công";
        public const string SystemUserUpdateFail = "Cập nhật thông tin người dùng thất bại";
        public const string SystemUserDeleteSuccess = "Xóa người dùng thành công";
        public const string SystemUserDeleteFail = "Xóa người dùng thất bại";
    }
}