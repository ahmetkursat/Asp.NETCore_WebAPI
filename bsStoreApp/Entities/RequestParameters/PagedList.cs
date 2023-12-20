using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestParameters
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }

        public PagedList(List<T> items, int count, int pagenumber, int pageSize)
        {
            MetaData = new MetaData()
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pagenumber,
                TotalPage = (int)Math.Ceiling(count / (double)pageSize)
            };

            AddRange(items);



        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pagesize) {
        
            var count = source.Count();
            var items = source.Skip((pageNumber-1)*pagesize).Take(pagesize).ToList();
        
                return new PagedList<T>(items, count, pagesize,pageNumber);
        }
    }
}
