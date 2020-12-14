using System;
using System.Collections.Generic;
using System.Text;

namespace BoardParser.WindowsApp
{
    public class ParsingSettings
    {
        public string SiteName { get; set; }
        public string Page { get; set; }
        public string ExportFilePath { get; set; }
        public bool Split { get; set; }
        public int AmountToSplit { get; set; }
        public int MaxItemsInCategory { get; set; }
        public bool CheckDuplicates { get; set; }
    }
}
