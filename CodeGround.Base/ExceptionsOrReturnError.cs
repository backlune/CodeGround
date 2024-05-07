using System;
using System.Threading.Tasks;

namespace GodeGround.Base
{
    internal class ServiceConsumer
    {
        private readonly ServiceReturningError _serviceReturningError;
        private readonly ServiceThrowingException _serviceThrowingException;

        public ServiceConsumer(ServiceReturningError serviceReturningError,
            ServiceThrowingException serviceThrowingException)
        {
            _serviceReturningError = serviceReturningError;
            _serviceThrowingException = serviceThrowingException;
        }

        public int GetInt()
        {
            var result = _serviceReturningError.GetInt();
            result.ExtractResult(out var val); // Still not required to check for error
            return val;
        }

        public Result<int> GetIntError()
        {
            return _serviceReturningError.GetIntError();
        }

        public int GetIntException()
        {
            try
            {
                return _serviceThrowingException.GetIntException();
            }
            catch (Exception e)
            {
                // Do something with the exception that consumer can handle! Below is stupid example just to compile
                return -1;
            }
        }
    }

    internal class ServiceThrowingException
    {
        public int GetInt()
        {
            return 42;
        }

        public int GetIntException()
        {
            throw new Exception("Exception");
        }
    }

    internal class ServiceReturningError
    {
        public Result<int> GetInt()
        {
            return Result.Success(42);
        }

        public Task<Result<int>> GetIntAsync()
        {
            return Task.FromResult(Result.Success(42));
        }

        public Result<int> GetIntError()
        {
            return Result.Error<int>("Error");
        }
    }

    internal class Result
    {
        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Error<T>(string errorMessage)
        {
            return new Result<T>(errorMessage);
        }
    }

    internal class Result<T>
    {
        public Result(T value)
        {
            Value = value;
            IsSuccess = true;
        }

        public Result(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsSuccess = false;
        }

        private T Value { get; set; }
        private bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }


        public bool ExtractResult(out T val)
        {
            val = Value;
            return IsSuccess;
        }

        


    }


}
