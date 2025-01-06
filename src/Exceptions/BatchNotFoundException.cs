namespace Exceptions
{
    public sealed class BatchNotFoundException : EntityNotFoundException
    {
        public BatchNotFoundException(int id) 
            : base("Batch", id)
        {
        }
    }
}
