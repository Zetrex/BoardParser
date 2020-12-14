using BoardParser.Common.Models;
using BoardParser.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardParser.Common.Interfaces
{
    public interface ISiteParserService
    {
        delegate void ParserHandler(int value, int maxValue);
        event ParserHandler ProcessEvent;

        string GetSiteName();
        PageTypes GetPageType(string url);
        Task<List<BoardItem>> ParsePageAsync(string url, PageTypes pageType, int max = 100);
    }
}
