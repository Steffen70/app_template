using System.Threading.Tasks;
using API.Helpers.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class GenericRepository<TEntity, TDto> where TEntity : BaseEntity
    {
        protected readonly DataContext _context;
        protected readonly UnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public GenericRepository(DataContext context, UnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            return _mapper.Map<TDto>(entity);
        }

        public async Task<PagedList<TDto>> ListAllAsync(PaginationParams paginationParams)
        {
            var dtos = _context.Set<TEntity>()
                .ProjectTo<TDto>(_mapper.ConfigurationProvider);

            return await PagedList<TDto>.CreateAsync(dtos, paginationParams, _mapper);
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}