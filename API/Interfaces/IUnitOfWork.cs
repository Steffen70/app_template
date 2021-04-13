using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IMemberRepository MemberRepository { get; }

        Task<bool> Complete();
        bool HasChanges();

        Task MigrateAsync();
    }
}