namespace API.Constants
{
    public static class EventCodes
    {
        public static class Auth
        {
            public const string RegistrationError = "REG-ERR-01";
            public const string DuplicateUser = "REG-INFO-01";
            public const string LoginError = "LOGIN-ERR-01";
            public const string PasswordUpdateError = "PWD-ERR-01";
            public const string OldPasswordIncorrect = "PWD-INFO-01";
        }

        public static class Database
        {
            public const string GenericError = "DB-ERR-01";
            public const string Timeout = "DB-TIMEOUT-01";
        }

        public static class System
        {
            public const string Unexpected = "SYS-UNEXPECTED-01";
            public const string ValidationFailed = "SYS-VALIDATION-01";
        }

        // Add more categories like Project, Task, etc. as needed
    }
}

    
 
