using BoardParser.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardParser.Common.Interfaces
{
    public interface ISiteParser
    {
        string GetSiteName();
        Task<List<BoardItem>> ParseMainPageAsync();
        Task<List<BoardItem>> ParseCategoryAsync(string url);
        Task<BoardItem> ParsePageAsync(string url);
    }
}
