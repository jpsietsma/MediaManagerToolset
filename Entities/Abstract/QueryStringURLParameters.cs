using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Abstract
{
    public abstract class QueryStringURLParameters
	{
		#region Section: Pagination Query Parameters

			/// <summary>
			/// Maximum number of pages to show
			/// </summary>
			public int MaxPageSize { get; set; }

			/// <summary>
			/// Page Number of records to show
			/// </summary>
			public int PageNumber { get; set; }

			/// <summary>
			/// Number of records to return per page
			/// </summary>
			public abstract int PageSize { get; set;}
        #endregion
    }
}
