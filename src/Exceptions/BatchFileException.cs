namespace Exceptions
{
    public class BatchFileException : KoganMobileException
    {
        public BatchFileException(string? message) : base(message)
        {
        }

        public BatchFileException(string? message, Exception? innerException) 
            : base(message, innerException)
        {
        }
    }
}
