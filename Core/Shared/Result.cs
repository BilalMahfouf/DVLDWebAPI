using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Shared
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public string? ErrorMessage { get; protected set; }
        public Enums.ErrorType ErrorType { get; protected set; }

        protected Result(bool isSuccess, string? errorMessage = null, Enums.ErrorType errorType = Enums.ErrorType.Success)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            ErrorType = errorType;
        }
        public static Result Success => new Result(true);
        public static Result Failure(string errorMessage,
            Enums.ErrorType errorType = Enums.ErrorType.InternalServerError)
        {
            return new Result(false,errorMessage, errorType);
        }
    }
}
