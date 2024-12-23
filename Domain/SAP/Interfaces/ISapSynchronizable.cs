namespace Kogan.Domain.SAP.Interfaces
{
    public interface ISapSynchronizable
    {
        string ObjectType { get; set; }

        string ObjectKey { get; set; }
    }
}
