using BoardParser.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardParser.Common.Interfaces
{
    public interface IFileService
    {
        Task WriteJson(List<BoardItem> items);
        Task WriteXml(List<BoardItem> items);
        Task<List<BoardItem>> ReadFile(string path);
    }
}
