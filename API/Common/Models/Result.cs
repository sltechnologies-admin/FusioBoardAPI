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
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }         // User-facing
        public string? TechnicalDetails { get; set; }     // Internal
        public T? Data { get; set; }

        public static Result<T> SuccessResult(T data) =>
            new Result<T> { Success = true, Data = data };

        public static Result<T> Fail(string errorMessage, string? technicalDetails = null) =>
            new Result<T> { Success = false, ErrorMessage = errorMessage, TechnicalDetails = technicalDetails };
    }

}
