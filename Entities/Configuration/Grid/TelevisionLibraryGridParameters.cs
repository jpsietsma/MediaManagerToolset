using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration.Grid
{
    public class TelevisionLibraryGridParameters : QueryStringURLParameters
    {
		private int _pageSize;

		/// <summary>
		/// Number of records to return per page
		/// </summary>
		public override int PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				_pageSize = (value > MaxPageSize) ? MaxPageSize : value;
			}
		}



		public TelevisionLibraryGridParameters()
		{
			MaxPageSize = 20;
			PageNumber = 1;
			PageSize = 15;
		}
	}
}
