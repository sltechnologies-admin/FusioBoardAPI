using System.Runtime.CompilerServices;

namespace API.Common.Extensions
{
    public static class ExceptionHelper
    {
        public static string GetDetailedError(Exception ex,
            [CallerMemberName] string methodName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            var className = System.IO.Path.GetFileNameWithoutExtension(filePath);

            var message = $"Exception in {className}.{methodName} (Line {lineNumber}): {ex.Message}";

            if (!string.IsNullOrWhiteSpace(ex.StackTrace))
            {
                message += $"\nStackTrace: {ex.StackTrace}";
            }

            return message;
        }
    }
}
 