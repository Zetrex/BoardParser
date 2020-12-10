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
        Task<BoardItem> ParcePage(string url);
    }
}
