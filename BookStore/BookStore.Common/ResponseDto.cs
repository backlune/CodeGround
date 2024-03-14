namespace BookStore.Common
{
    public class ResponseDto
    {
        public static ResponseDto<T> Success<T>(T result) => new() { Result = result, IsSuccess = true };

        public static ResponseDto<T> Error<T>(String message) => new() { DisplayMessage = message, IsSuccess = true };
    }

    public class ResponseDto<T>
    {
        

        public bool IsSuccess { get; set; }

        public T Result { get; set; }

        public string DisplayMessage { get; set; }


    }
}
