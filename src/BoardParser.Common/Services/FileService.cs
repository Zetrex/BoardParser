using BoardParser.Common.Interfaces;
using BoardParser.Common.Models;
using Newtonsoft.Json;
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
        private readonly string _pathFileWithIds;

        public FileService()
        {
            // TODO: getfrom settings
            _pathFileWithIds = Environment.CurrentDirectory + "\\ids.json";
        }

        public Task<List<BoardItem>> ReadFile(string path, string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<int>> GetIds()
        {
            var list = new List<int>();

            try
            {
                if (!File.Exists(_pathFileWithIds))
                    return list;

                using (StreamReader file = new StreamReader(_pathFileWithIds))
                {
                    var data = await file.ReadToEndAsync();
                    list = JsonConvert.DeserializeObject<List<int>>(data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        public async Task SaveIds(List<int> ids)
        {
            var json = JsonConvert.SerializeObject(ids.ToArray());

            using (StreamWriter file = new StreamWriter(_pathFileWithIds))
            {
                await file.WriteAsync(json);
            }
        }

        public Task<string> WriteJson(string path, List<BoardItem> items)
        {
            throw new NotImplementedException();
        }

        public async Task<string> WriteXml(string path, List<BoardItem> items)
        {
            var xml = GetXml(items);
            var fileName = GetNewFileName();
            var fullPath = $"{ path}\\{fileName}.xml";

            using (StreamWriter file = new StreamWriter(fullPath))
            {
                await file.WriteAsync(xml);
            }

            return fullPath;
        }

        public async Task WriteXmlSeparated(string path, List<BoardItem> items, int amountToSplit)
        {
            var fileName = GetNewFileName();
            for (int i = 0; i < items.Count; i += amountToSplit)
            {
                var partOfItems = new List<BoardItem>();
                int index = 0;
                for (int j = 0; j < amountToSplit; j++)
                {
                    if (i + j >= items.Count) break;
                    partOfItems.Add(items[i + j]);
                    index = j;
                }

                var fullPath = $"{path}\\{fileName}_{i + 1}-{i + index + 1}.xml";
                var xml = GetXml(partOfItems);
                using (StreamWriter file = new StreamWriter(fullPath))
                {
                    await file.WriteAsync(xml);
                }
            }
        }

        public string GetXml(List<BoardItem> items)
        {
            try
            {
                XmlSerializer xsSubmit = new XmlSerializer(items.GetType());
                var xml = "";

                using (var sww = new StringWriterWithEncoding())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww, new XmlWriterSettings() { Encoding = Encoding.UTF8 }))
                    {
                        xsSubmit.Serialize(writer, items);
                        xml = sww.ToString().Replace("ArrayOfListing", "listings");     // TODO: to fix
                    }
                }

                return xml;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string GetNewFileName()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;

            return $"items_{secondsSinceEpoch}";
        }
    }

    public sealed class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding encoding;

        public StringWriterWithEncoding() : this(Encoding.UTF8) { }

        public StringWriterWithEncoding(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return encoding; }
        }
    }
}
