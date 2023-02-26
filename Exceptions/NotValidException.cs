namespace ToDoApi.Exceptions
{
    public class NotValidException : Exception
    {
        public NotValidException(string? message)
            : base(message)
        { }
    }
}