using System;
using System.Collections.Generic;
using System.Text;

namespace BoardParser.Common.Models
{
    public class BoardItem
    {
        public BoardItem()
        {
            Image = new List<string>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string CountryId { get; set; }
        public string Country { get; set; }
        public List<string> Image { get; set; }
        public string DateTime { get; set; }
    }
}
