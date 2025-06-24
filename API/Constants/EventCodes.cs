namespace API.Constants
{
  
    public static class EventCodes
    {
        #region Naming Convention Strategy
        /*
         | Prefix                        | Meaning                                  |
        | ----------------------------- | ---------------------------------------- |
        | `REG-`, `PRJ-`, `TASK-`, etc. | Module name                              |
        | `ERR`                         | Error                                    |
        | `INFO`                        | Informational (e.g., duplicates)         |
        | `WARN` *(optional)*           | Warning-level                            |
        | `01`, `02`...                 | Method or use-case position (sequential) |

        */
        #endregion
        public static class Auth
        {
            public const string RegistrationError = "REG-ERR-01";
            public const string DuplicateUser = "REG-INFO-01";
            public const string LoginError = "LOGIN-ERR-01";
            public const string PasswordUpdateError = "PWD-ERR-01";
            public const string OldPasswordIncorrect = "PWD-INFO-01";
        }

        public static class Project
        {
            public const string CreateError = "PRJ-ERR-01";
            public const string FetchByIdError = "PRJ-ERR-02";
            public const string FetchAllError = "PRJ-ERR-03";
            public const string UpdateError = "PRJ-ERR-04";

            public const string AlreadyExistsInfo = "PRJ-INFO-01";
        }

        public static class Role
        {
            public const string AssignError = "ROLE-ERR-01";
            public const string FetchUserRolesError = "ROLE-ERR-02";

            public const string AlreadyExistsInfo = "ROLE-INFO-01";
        }

        public static class Task
        {
            public const string CreateError = "TASK-ERR-01";
            public const string UpdateError = "TASK-ERR-02";
            public const string StatusChangeError = "TASK-ERR-03";
        }

        public static class Sprint
        {
            public const string StartError = "SPRINT-ERR-01";
            public const string CloseError = "SPRINT-ERR-02";
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
    }
}
