namespace Exceptions
{
    public class KoganMobileException : Exception
    {
        public KoganMobileException()
        {
        }

        public KoganMobileException(string? message) : base(message)
        {
        }

        public KoganMobileException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
