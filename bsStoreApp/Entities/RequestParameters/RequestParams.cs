using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeature
{
    public abstract class RequestParams
    {
		const int maxPageSize = 50;

        public int PageNumber { get; set; }

		private int _pagesize;

		public int PageSize
		{
			get { return _pagesize; }
			set { _pagesize = value > maxPageSize ? maxPageSize : value; }
		}

	}
}
