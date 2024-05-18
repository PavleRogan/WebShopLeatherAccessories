using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Application.Common;

public class PagedResult<T>
{

    public PagedResult(IEnumerable<T> items, int totalCount, int pageSize, int pageNumber)
    {
        Items = items;
        TotalItemsCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public IEnumerable<T> Items { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalItemsCount { get; set; }
}
