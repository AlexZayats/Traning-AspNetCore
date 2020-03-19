using System.Collections.Generic;

namespace Traning.AspNetCore.EntityFramework.Logic.Models
{
    public class PagedResult<TItem>
    {
        public IEnumerable<TItem> Items { get; set; }
        public long Total { get; set; }
    }
}
