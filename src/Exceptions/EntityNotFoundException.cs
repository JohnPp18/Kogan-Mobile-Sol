namespace Exceptions
{
    public class EntityNotFoundException : KoganMobileException
    {
        #region Properties
        public int Id { get; private init; }
        #endregion

        public EntityNotFoundException(string entityName, int id)
            : base($"{entityName} not found (Id: {id}).")
        {
            this.Id = id;
        }
    }
}
