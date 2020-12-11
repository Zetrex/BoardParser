﻿using BoardParser.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardParser.Common.Interfaces
{
    public interface IFileService
    {
        Task<string> WriteJson(string path, List<BoardItem> items);
        Task<string> WriteXml(string path, List<BoardItem> items);
        Task<List<BoardItem>> ReadFile(string path, string fileName);
    }
}
