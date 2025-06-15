using API.Models;

namespace API.Constants
{
    public static class Messages
    {
        public static class Auth
        {
            // Success
            public const string s_UserRegSuccess = "User registered successfully.";
            public const string s_PwdUpdateSuccess = "Password changed successfully.";

            // Info
            public const string i_UserAlreadyExists = "Email or Username already exists.";
            public const string i_InvalidUserNameOrPwd = "Invalid username or password.";
            public const string i_UserNotFound = "User not found.";
            public const string i_OldPwdIncorrect = "Old password is incorrect.";

            // Error
            public const string e_UnexpectedRegistrationError = "Unexpected error during registration.";
            public const string e_PwdUpdateFailed = "Password update failed.";
        }

        // Example structure for Project module
        public static class Project
        {
            // Success
            public const string s_ProjectCreated = "Project created successfully.";

            // Info
            public const string i_ProjectAlreadyExists = "A project with the same name already exists.";

            // Error
            public const string e_ProjectCreationFailed = "Failed to create project.";
        }

        // Add more modules as needed: Task, Dashboard, Notification, etc.
    }
}
