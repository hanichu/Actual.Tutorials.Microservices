using System;

namespace Common
{
    public class ApiResponse
    {
        public ApiResponse(bool isFaulted)
        {
            this.IsFaulted = isFaulted;
        }

        public bool IsFaulted { get; }
    }

    public class Success : ApiResponse
    {
        public Success()
            : base(false)
        { }
    }

    public class SuccessWithResult<T> : ApiResponse
    {
        public SuccessWithResult(T result)
            : base(false)
        {
            this.Result = result;
        }

        public T Result { get; }
    }

    public class Fault : ApiResponse
    {
        public Fault(string message)
            : base(true)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
