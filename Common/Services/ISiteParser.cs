using BoardParser.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardParser.Common.Services
{
    public interface ISiteParser
    {
        string GetSiteName();
        Task<List<BoardItem>> ParceMainPageAsync();
        Task<List<BoardItem>> ParceCategoryAsync(string url);
        Task<BoardItem> ParcePageAsync(string url);
    }
}
