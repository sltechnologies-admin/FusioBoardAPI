﻿namespace API.Common.Logging
{
    public interface IAppLogger<T>
    {
        void LogError(Exception ex, string message, params object[] args);
        void LogInformation(string message, params object[] args);
    }

}
