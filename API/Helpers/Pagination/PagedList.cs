using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers.Pagination
{
    public class PagedList<TList> : List<TList>
    {
        public PaginationHeader Header { get; set; }

        public PagedList() { }

        public PagedList(IEnumerable<TList> items, PaginationHeader header)
        {
            Header = header;

            header.TotalPages = (int)Math.Ceiling(header.TotalItems / (double)header.PageSize);

            AddRange(items);
        }

        public static async Task<PagedList<TList>> CreateAsync<TPaginationParams>(
            IQueryable<TList> source, TPaginationParams paginationParams, IMapper mapper) where TPaginationParams : PaginationParams
        {
            var header = mapper.Map<PaginationHeader>(paginationParams);

            header.TotalItems = await source.CountAsync();

            var items = await source
                .Skip((header.CurrentPage - 1) * header.PageSize)
                .Take(header.PageSize)
                .ToListAsync();

            return new PagedList<TList>(items, header);
        }

        public static PagedList<TList> Create<TPaginationParams>(
            IEnumerable<TList> source, TPaginationParams paginationParams, IMapper mapper) where TPaginationParams : PaginationParams
        {
            var header = mapper.Map<PaginationHeader>(paginationParams);

            header.TotalItems = source.Count();

            var items = source
                .Skip((header.CurrentPage - 1) * header.PageSize)
                .Take(header.PageSize).AsEnumerable();

            return new PagedList<TList>(items, header);
        }
    }
}