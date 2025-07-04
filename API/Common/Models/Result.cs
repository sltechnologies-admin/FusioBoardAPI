namespace API.Common.Models
{

    /*
      Reasoning for placing under Common.Models: 
        It’s not tied to Users → so avoid putting it under Features/Users.
        It’s not a DTO for data transfer, but rather a wrapper for method results.
        It's used in Services, Controllers, Repositories, and maybe later in Middleware, so Common is ideal.
     */
    public class Result<T>
    {
        public int? TotalCount { get; set; }
        public bool IsSuccess { get; set; }
        public string? UserErrorMessage { get; set; }         // User-facing
        public string? TechnicalErrorDetails { get; set; }     // Internal
        public T? Data { get; set; }

        public static Result<T> SuccessResult(T data, int? totalCount = 0) =>
            new Result<T> {
                IsSuccess = true,
                Data = data,
                TotalCount = totalCount
            };

        public static Result<T> Fail(string errorMessage, string? technicalDetails = null) =>
            new Result<T> {
                IsSuccess = false,
                UserErrorMessage = errorMessage,
                TechnicalErrorDetails = technicalDetails
            };
    }


}
