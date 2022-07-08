using AutoMapper;
using System.Text;
using System.Reflection;
using System.Linq.Dynamic.Core;


namespace NTEcommerce.SharedDataModel
{
    //T1 is model, T2 is output model
    public class PagedList<T1, T2> : List<T2> where T1 : class
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public PagedList(List<T2> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }
        public static PagedList<T1, T2> ToPageList(IQueryable<T1> source, int pageNumber, int pageSize, IMapper mapper)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var itemT2 = mapper.Map<List<T2>>(items);

            return new PagedList<T1, T2>(itemT2, count, pageNumber, pageSize);
        }
        public static PagedList<T1, T2> ToPageList(IEnumerable<T1> source, int pageNumber, int pageSize, IMapper mapper)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var itemT2 = mapper.Map<List<T2>>(items);
            return new PagedList<T1, T2>(itemT2, count, pageNumber, pageSize);
        }

        private static List<T2> ApplySort(IQueryable<T2> entities, string orderByQueryString)
        {
            if (!entities.Any())
                return entities.ToList();
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return entities.ToList();
            }
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(T2).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            return entities.OrderBy(orderQuery).ToList();
        }
    }
}