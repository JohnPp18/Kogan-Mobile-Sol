namespace Exceptions
{
    public class CannotDeleteBatchException : KoganMobileException
    {
        public int Id { get; set; }

        public CannotDeleteBatchException(int id, string innerMsg)
            : base($"Cannot delete batch with id '{id}': {innerMsg}")
        {
            Id = id;
        }
    }
}
