namespace Task.Manager.Domain.Resource.Base
{
    public class ResponseDefault<T> where T : class
    {
        public ResponseDefault(T data)
        {
            Data = data;
            Success = true;
        }
        public ResponseDefault()
        {
            Success = true;
        }
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string MessageDetail { get; set; }
    }
}
