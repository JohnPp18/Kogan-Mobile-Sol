namespace Kogan.Domain.Common.Interfaces
{
    public interface ICompletable
    {
        DateTime? CompletedAtUtc { get; set; }
    }
}
