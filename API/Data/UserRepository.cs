using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using API.Helpers.Pagination;
using API.DTOs;
using AutoMapper.QueryableExtensions;
using System.Linq;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username.ToLower());
        }

        public void AddUser(AppUser user)
        {
            _context.Users.Add(user);
        }

        public void UpdateUser(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void DeleteUser(AppUser user)
        {
            _context.Users.Remove(user);
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .SingleOrDefaultAsync(x => x.Username == username.ToLower());
        }

        public async Task<PagedList<UserAdminDto>> GetUsersAsync(PaginationParams paginationParams)
        {
            var userAdminDtos = _context.Users
                .OrderBy(u => u.LastActive)
                .ProjectTo<UserAdminDto>(_mapper.ConfigurationProvider);

            return await PagedList<UserAdminDto>.CreateAsync(userAdminDtos, paginationParams, _mapper);
        }

        public async Task<bool> AnyUsersAsync()
        => await _context.Users.AnyAsync();
    }
}