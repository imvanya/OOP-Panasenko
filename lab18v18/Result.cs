using System;

namespace Lab18v18
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T Value { get; private set; }
        public string Error { get; private set; }

        private Result(T value, bool isSuccess, string error)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value, true, null);
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(default(T), false, error);
        }

        public static Result<T> Failure(Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException("ex");
            }
            return new Result<T>(default(T), false, ex.Message);
        }
    }
}