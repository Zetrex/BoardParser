using BoardParser.Common.Interfaces;
using BoardParser.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BoardParser.Common.Services
{
    public class FileService : IFileService
    {
        public FileService()
        {

        }

        public Task<List<BoardItem>> ReadFile(string path, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<string> WriteJson(string path, List<BoardItem> items)
        {
            throw new NotImplementedException();
        }

        public async Task<string> WriteXml(string path, List<BoardItem> items)
        {
            var xml = GetXml(items);
            var fileName = GetNewFileName();
            var fullPath = $"{path}\\{fileName}";

            using (StreamWriter file = new StreamWriter(fullPath))
            {
                await file.WriteAsync(xml);
            }

            return fullPath;
        }

        public string GetXml(List<BoardItem> items)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(List<BoardItem>));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, items);
                    xml = sww.ToString().Replace("ArrayOfListing", "listings");     // TODO: to fix
                }
            }

            return xml;
        }
        
        public string GetNewFileName()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;

            return $"items_{secondsSinceEpoch}.xml";
        }

    }
}
