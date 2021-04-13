using System.Threading.Tasks;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public IUserRepository UserRepository => new UserRepository(_context, _mapper);
        public IMemberRepository MemberRepository => new MemberRepository(_context, this, _mapper);

        public async Task<bool> Complete()
        => await _context.SaveChangesAsync() > 0;

        public bool HasChanges()
        => _context.ChangeTracker.HasChanges();

        public async Task MigrateAsync()
        => await _context.Database.MigrateAsync();
    }
}