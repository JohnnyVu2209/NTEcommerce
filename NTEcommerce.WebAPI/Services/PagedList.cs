using AutoMapper;
using NTEcommerce.SharedDataModel.Product;
using NTEcommerce.WebAPI.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTEcommerce.WebAPI.Services
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

            /*if (typeof(T1) == typeof(Product))
            {
                IList list = items;
                IList listModel = itemT2;
                for (int i = 0; i < itemT2.Count; i++)
                {
                    ProductModel itemModel = (ProductModel)itemT2[i];
                    for (int j = 0; j < items.Count; j++)
                    {
                        Product item = (Product)items[j];
                        if (itemModel.Id == item.Id)
                            item.Images = GetImages(item.Images);

                    }
                }
            }*/

            return new PagedList<T1, T2>(itemT2, count, pageNumber, pageSize);
        }
        public static PagedList<T1, T2> ToPageList(IEnumerable<T1> source, int pageNumber, int pageSize, IMapper mapper)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var itemT2 = mapper.Map<List<T2>>(items);

            return new PagedList<T1, T2>(itemT2, count, pageNumber, pageSize);
        }
    }
}
