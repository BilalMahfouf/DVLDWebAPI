using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared
{
    public  class Result<T>
    {
        public bool IsSuccess { get; protected set; }
        public string? ErrorMessage { get; protected set; }
        public Enums.ErrorType ErrorType { get; protected set; }
        public T Data { get; protected set; }

        protected Result(bool isSuccess,T data ,string? errorMessage = null, Enums.ErrorType errorType = Enums.ErrorType.InternalServerError)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            ErrorType = errorType;
            Data = data;
        }
        public static Result<T> Success(T data) =>new Result<T>(true,data);
        public static Result<T> Failure(string errorMessage, Enums.ErrorType errorType = Enums.ErrorType.InternalServerError)
        {
            return new Result<T>(false, default!, errorMessage, errorType);
        }
    }
}
