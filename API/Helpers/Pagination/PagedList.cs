using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers.Pagination
{
    public class PagedList<TList, THeader> : List<TList> where THeader : PaginationHeader
    {
        public THeader Header { get; set; }

        public PagedList() { }

        public PagedList(IEnumerable<TList> items, THeader header)
        {
            Header = header;

            AddRange(items);
        }

        public static async Task<PagedList<TList, PaginationHeader>> CreateAsync(
            IQueryable<TList> source, PaginationParams paginationParams, IMapper mapper)
            => await CreateAsync<PaginationParams, PaginationHeader>(source, paginationParams, mapper);

        public static async Task<PagedList<TList, TPaginationHeader>> CreateAsync<TPaginationParams, TPaginationHeader>(
            IQueryable<TList> source, TPaginationParams paginationParams, IMapper mapper)
            where TPaginationParams : PaginationParams
            where TPaginationHeader : PaginationHeader
        {
            var (items, header) = await FetchDataAsync<TPaginationParams, TPaginationHeader>(source, paginationParams, mapper);

            return new PagedList<TList, TPaginationHeader>(items, header);
        }

        public static async Task<PagedList<TList, PaginationHeader>> CreateAndMapInMemoryAsync<TEntity>(
            IQueryable<TEntity> source, PaginationParams paginationParams, IMapper mapper)
            => await CreateAndMapInMemoryAsync<PaginationParams, PaginationHeader, TEntity>(source, paginationParams, mapper);

        public static async Task<PagedList<TList, TPaginationHeader>> CreateAndMapInMemoryAsync<TPaginationParams, TPaginationHeader, TEntity>(
            IQueryable<TEntity> source, TPaginationParams paginationParams, IMapper mapper)
            where TPaginationParams : PaginationParams
            where TPaginationHeader : PaginationHeader
        {
            var (items, header) = await FetchDataAsync<TEntity, TPaginationParams, TPaginationHeader>(source, paginationParams, mapper);

            var dtos = mapper.Map<IEnumerable<TList>>(items);

            return new PagedList<TList, TPaginationHeader>(dtos, header);
        }

        protected static async Task<Tuple<IEnumerable<TList>, TPaginationHeader>> FetchDataAsync<TPaginationParams, TPaginationHeader>(
            IQueryable<TList> source, TPaginationParams paginationParams, IMapper mapper)
            where TPaginationParams : PaginationParams
            where TPaginationHeader : PaginationHeader
            => await FetchDataAsync<TList, TPaginationParams, TPaginationHeader>(source, paginationParams, mapper);

        protected static async Task<Tuple<IEnumerable<T>, TPaginationHeader>> FetchDataAsync<T, TPaginationParams, TPaginationHeader>(
            IQueryable<T> source, TPaginationParams paginationParams, IMapper mapper)
            where TPaginationParams : PaginationParams
            where TPaginationHeader : PaginationHeader
        {
            var header = mapper.Map<TPaginationHeader>(paginationParams);

            header.TotalItems = await source.CountAsync();

            var items = await source
                .Skip((header.CurrentPage - 1) * header.PageSize)
                .Take(header.PageSize)
                .ToListAsync();

            header.TotalPages = (int)Math.Ceiling(header.TotalItems / (double)header.PageSize);

            return new Tuple<IEnumerable<T>, TPaginationHeader>(items, header);
        }
    }
}