public static class Messages
{
    public static class User
    {
        // ---------- Error ----------
        public const string e_UnexpectedRegistrationError = "Unexpected error during registration.";
        public const string e_PwdUpdateFailed = "Password update failed.";
        public const string e_UnexpectedErrorFetchingUserRoles = "Unexpected error fetching user roles.";

        // Keep only what's shown to the user
        public const string i_UserNotFound = "User not found.";
        public const string i_InvalidUserNameOrPwd = "Invalid username or password.";
        public const string i_OldPwdIncorrect = "Old password is incorrect.";
        public const string i_UserAlreadyExists = "A user with the same email or username already exists.";


        // Success
        public const string s_UserRegSuccess = "User registered successfully.";
        public const string s_PasswordChanged = "Password changed successfully.";
        public const string s_UserUpdated = "User updated successfully.";
        public const string s_UserDeactivated = "User deactivated successfully.";

    }

    public static class Project
    {
        // Success
        public const string s_ProjectCreated = "Project created successfully.";
        public const string s_ProjectUpdated = "Project updated successfully.";
        public const string s_ProjectArchived = "Project archived successfully.";

        // ---------- Info ----------
        public const string i_ProjectAlreadyExists = "A project with the same name already exists.";

        // ---------- Error ----------
        public const string e_ProjectCreationFailed = "Failed to create project.";

        public const string s_ProjectUpdatedSuccessfully = "Project updated successfully.";
        public const string e_UnexpectedErrorUpdatingProject = "An unexpected error occurred while updating the project.";
        public const string e_UnexpectedErrorFetchingProjects = "An error occurred while retrieving project list.";
        public const string e_UnexpectedErrorFetchingProjectById = "An error occurred while retrieving the project.";
        public const string e_ProjectUpdateFailed = "An error occurred while updating the project.";
        

    }

    public static class Role
    {
        // Success
        public const string s_UserRoleAssigned = "Role assigned to user successfully.";
        public const string s_RoleCreated = "Role created successfully.";
    }

    public static class Task
    {
        // Success
        public const string s_TaskCreated = "Task created successfully.";
        public const string s_TaskUpdated = "Task updated successfully.";
        public const string s_TaskStatusChanged = "Task status updated successfully.";
    }

    public static class Sprint
    {
        public const string s_SprintCreated = "Sprint created successfully.";
        public const string s_SprintUpdatedSuccessfully = "Sprint updated successfully.";
        public const string s_SprintDeleted = "Sprint deleted successfully.";

        public const string e_SprintCreateFailed = "Unable to create sprint.";
        public const string e_SprintUpdateFailed = "Unable to update sprint.";
        public const string e_SprintFetchFailed = "Failed to fetch sprint(s).";
        public const string e_SprintDeleteFailed = "Failed to delete sprint.";
    }
}

 