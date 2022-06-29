namespace NTEcommerce.WebAPI.Constant
{
    public static class MessageCode
    {
        public static class SuccessCode
        {

        }
        public static class ErrorCode
        {
            //User
            public static string USER_NOT_FOUND = "ERR_USER_NOT_FOUND";
            public static string USERNAME_OR_PASSWORD_NOT_CORRECT = "ERR_USERNAME_OR_PASSWORD_NOT_CORRECT";

            //Category
            public static string CATEGORY_NAME_ALREADY_EXISTED = "ERR_CATEGORY_NAME_ALREADY_EXISTED";
            public static string CREATE_CATEGORY_FAILED = "ERR_CREATE_CATEGORY_FAILED";
            public static string CATEGORY_NOT_EXIST = "ERR_CATEGORY_NOT_EXIST";
        }
    }
}
