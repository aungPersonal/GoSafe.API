using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSafe.Dto.Common
{
    public class OurTableState
    {
        /// <summary>
        /// The requested index of the page to display.
        /// </summary>
        /// <remarks>
        /// The index of the first page is <c>0</c>.  
        /// </remarks>
        public int Page { get; set; }

        /// <summary>
        /// The number of items requested.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The optional column to sort by.
        /// </summary>
        public string? SortLabel { get; set; }

        /// <summary>
        /// The direction to sort results.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="SortDirection.None"/>.
        /// </remarks>
        public OurSortDirection SortDirection { get; set; }
    }

    public enum OurSortDirection
    {
        /// <summary>
        /// No sort direction.
        /// </summary>
        [Description("none")]
        None,

        /// <summary>
        /// Results are sorted in ascending order (A-Z).
        /// </summary>
        [Description("ascending")]
        Ascending,

        /// <summary>
        /// Results are sorted in descending order (Z-A).
        /// </summary>
        [Description("descending")]
        Descending,
    }
}
