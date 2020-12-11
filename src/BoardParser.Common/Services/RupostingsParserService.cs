using BoardParser.Common.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Linq;
using System.IO;
using BoardParser.Common.Interfaces;
using BoardParser.Common.Models.Enums;

namespace BoardParser.Common.Services
{
    public class RupostingsParserService : ISiteParserService
    {
        private readonly string _siteName = "https://www.rupostings.com";

        // TODO: get from appsettings
        private readonly bool PAUSES_ENABLED = true;

        // TODO: get from appsettings
        private readonly int PAUSE_DELAY = 2000;

        public RupostingsParserService()
        {

        }

        public string GetSiteName()
        {
            return _siteName;
        }

        public async Task<List<BoardItem>> ParseMainPageAsync()
        {
            var result = new List<BoardItem>();

            try
            {
                var mainPageHtml = await GetHtmlAsync(_siteName);
                var citiesLinks = GetCitiesLinks(mainPageHtml);

                foreach (var city in citiesLinks)
                {
                    result = await ParsePageWithCategoriesAsync(city);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<List<BoardItem>> ParsePageWithCategoriesAsync(string url)
        {
            var result = new List<BoardItem>();

            try
            {
                var cityHtml = await GetHtmlAsync(url);
                var categories = GetCityCategoriesLinks(cityHtml);

                if (PAUSES_ENABLED) await Task.Delay(PAUSE_DELAY);

                foreach (var category in categories)
                {
                    var items = await ParsePageWithCategoriesAsync(category + "?page=1&pageSize=100");
                    result.AddRange(items);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        public async Task<List<BoardItem>> ParseCategoryAsync(string url)
        {
            var result = new List<BoardItem>();

            try
            {
                var html = await GetHtmlAsync(url);
                if (string.IsNullOrEmpty(html)) return new List<BoardItem>();

                var links = GetCategoryLinks(html);

                if (PAUSES_ENABLED) await Task.Delay(PAUSE_DELAY);

                foreach (var link in links)
                {
                    var item = await ParsePageAsync(link);
                    if (item != null) result.Add(item);

                    if (PAUSES_ENABLED) await Task.Delay(PAUSE_DELAY);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<List<BoardItem>> ParsePageAsync(string url, PageTypes pageType)
        {
            var list = new List<BoardItem>();

            switch (pageType)
            {
                case PageTypes.MainPage:
                    list = await ParseMainPageAsync();
                    break;
                case PageTypes.PageWithCategories:
                    list = await ParsePageWithCategoriesAsync(url);
                    break;
                case PageTypes.CategoryPage:
                    list = await ParseCategoryAsync(url);
                    break;
                case PageTypes.SinglePage:
                    var item = await ParsePageAsync(url);
                    list.Add(item);
                    break;
                default:
                    break;
            }

            return list;
        }

        public async Task<BoardItem> ParsePageAsync(string url)
        {
            var item = new BoardItem();

            try
            {
                var html = await GetHtmlAsync(url);
                if (string.IsNullOrEmpty(html)) return null;

                //var regTitle = @"<h1[^>]+>(?<Title>[^<]+)";
                //var regImg = @"fotorama__stage[\D\d]*?<img[^""]*""(?<Image>[^""]*)";

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var parentNode = doc.DocumentNode.SelectNodes("//div").FirstOrDefault(x => x.HasClass("obyavl-wrapper"));

                // title
                item.Title = parentNode.SelectSingleNode("//h1").InnerText;

                // image
                // TODO: get all images
                string img = "";
                var imgBlock = parentNode.SelectNodes("//div").FirstOrDefault(x => x.HasClass("fotorama__wrap-link"));
                if (imgBlock != null)
                    item.Image.Add(_siteName + imgBlock.Attributes["data-img"].Value);

                // description
                item.Description = parentNode.SelectNodes("//div").FirstOrDefault(x => x.HasClass("obyvl-content")).SelectSingleNode("div/div").InnerText.Trim();

                // price
                // TODO: get double prices
                var regPrice = @"\$(?<Sum>\d+)";
                var matches = Regex.Matches(item.Description, regPrice);
                if (matches.Count > 0)
                    item.Price = matches[0].Groups["Sum"].Value;

                // phone
                var phoneNode = parentNode.SelectNodes("//a").FirstOrDefault(x => x.GetAttributeValue("id", "") == "phone");
                if (phoneNode != null)
                {
                    var pageId = GetPageId(url);
                    item.Phone = await GetPhoneAsync(pageId);
                }

                // username
                var contactNode = parentNode.SelectNodes("//img").FirstOrDefault(x => x.GetAttributeValue("src", "") == "../../Content/images/person-icon.png");
                if (contactNode != null)
                    item.ContactName = contactNode.ParentNode.InnerText.Trim();

                // city
                item.City = parentNode.SelectNodes("//img").FirstOrDefault(x => x.GetAttributeValue("src", "") == "/Content/images/location-icon.png").ParentNode.InnerText.Trim();

            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }

            return item;
        }

        // TODO: move to separate service
        private async Task<string> GetHtmlAsync(string url)
        {
            string response = null;

            try
            {
                using (WebClient client = new WebClient())
                {
                    response = client.DownloadString(url);
                }

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound || resp.StatusCode == HttpStatusCode.Moved)
                    {
                        // HTTP 404/301
                        return null;
                    }
                }

                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        private string GetPageId(string url)
        {
            try
            {
                var reg = @"\d+";

                var matches = Regex.Matches(url, reg);
                if (matches.Count > 0)
                    return matches.First().Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        private List<string> GetCitiesLinks(string html)
        {
            var list = new List<string>();

            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                var tags = doc.DocumentNode.SelectNodes("//a");
                var cityTags = tags.Where(x => x.HasClass("search-page-link-color")).ToList();
                list = cityTags.Select(x => _siteName + x.Attributes["href"].Value).Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        private List<string> GetCityCategoriesLinks(string html)
        {
            var list = new List<string>();

            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                list = doc.DocumentNode.SelectNodes("//a").Where(x => x.HasClass("category-group")).Select(x => _siteName + x.Attributes["href"].Value).Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        private List<string> GetCategoryLinks(string html)
        {
            var list = new List<string>();

            try
            {
                var reg = @"show[\?]id=\d+";
                var matches = Regex.Matches(html, reg);

                foreach (Match match in matches)
                {
                    list.Add($"{_siteName}/{match.Value}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list.Distinct().ToList();
        }

        private async Task<string> GetPhoneAsync(string pageId)
        {
            string responseBody = null;
            try
            {
                WebRequest request = WebRequest.Create($"https://www.rupostings.com/GetPhone");
                request.Method = "POST";

                string data = $"adId={pageId}";
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                WebResponse response = await request.GetResponseAsync();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        responseBody = reader.ReadToEnd();
                    }
                }
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseBody.Replace("\"", "");
        }

        public PageTypes GetPageType(string url)
        {
            // TODO: add tests

            var regMainPage = @"^http(s)?:\/\/(www.)?rupostings\.com(\/)?$";
            var matches = Regex.Matches(url, regMainPage);
            if (matches.Count > 0) return PageTypes.MainPage;

            var regPageWithCategories = @"^http(s)?:\/\/(www.)?rupostings\.com\/[\d\w-]+(\/)?$";
            matches = Regex.Matches(url, regPageWithCategories);
            if (matches.Count > 0) return PageTypes.PageWithCategories;

            var regCategoryPage = @"^http(s)?:\/\/(www.)?rupostings\.com(\/[\d\w-]+){2}(\/)?$";
            matches = Regex.Matches(url, regCategoryPage);
            if (matches.Count > 0) return PageTypes.CategoryPage;

            var regSinglePage = @"^http(s)?:\/\/(www.)?rupostings\.com\/show\?id=[\d]{1,10}(\/)?$";
            matches = Regex.Matches(url, regSinglePage);
            if (matches.Count > 0) return PageTypes.SinglePage;

            return PageTypes.Unknown;
        }
    }
}
