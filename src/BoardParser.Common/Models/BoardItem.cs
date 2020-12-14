using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BoardParser.Common.Models
{
    [Serializable]
    public class StringWithLang
    {
        [XmlText]
        public string Text { get; set; }

        [XmlAttribute("lang")]
        public string Lang { get; set; }

        //public StringWithLang(string lang)
        //{
        //    Lang = lang;
        //}
    }

    [XmlType("listing")]
    public class BoardItem
    {
        public BoardItem()
        {
            Image = new List<string>();

            var lang = "ru_RU";
            Title = new StringWithLang();
            Description = new StringWithLang();
            Category = new StringWithLang();
        }

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("title")]
        public StringWithLang Title { get; set; }

        [XmlElement("content")]
        public StringWithLang Description { get; set; }

        [XmlElement("category")]
        public StringWithLang Category { get; set; }

        [XmlElement("contactemail")]
        public string ContactEmail { get; set; }

        [XmlElement("contactname")]
        public string ContactName { get; set; }

        [XmlElement("sPhone")]
        public string Phone { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("currency")]
        public string Currency { get; set; }

        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("region")]
        public string Region { get; set; }

        [XmlElement("countryId")]
        public string CountryId { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlElement("image")]
        public List<string> Image { get; set; }

        [XmlElement("datetime")]
        public string DateTime { get; set; }
    }
}
