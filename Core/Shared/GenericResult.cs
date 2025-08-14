using Core.Common;

namespace Core.Shared
{
    public  class GenericResult<T>:Result
    {
        
        public T Data { get; protected set; }

        protected GenericResult(bool isSuccess, T data, string? errorMessage = null
            , Enums.ErrorType errorType = Enums.ErrorType.Success)
            : base(isSuccess, errorMessage, errorType)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            ErrorType = errorType;
            Data = data;
        }
        public static new GenericResult<T> Success(T data) =>new GenericResult<T>(true,data);
        public static new GenericResult<T> Failure(string errorMessage, Enums.ErrorType errorType = Enums.ErrorType.InternalServerError)
        {
            return new GenericResult<T>(false, default!, errorMessage, errorType);
        }
    }
}
