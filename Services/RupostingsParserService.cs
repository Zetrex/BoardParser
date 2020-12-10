using BoardParser.Common.Models;
using BoardParser.Common.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BoardParser.Services
{
    class RupostingsParserService : ISiteParser
    {
        // TODO: 
        // - сортировать по просмотрам
        // - где нет телефона не сохранять
        // - парсить телефон по регялрки из текста
        // - категории парсить из хлебных крошек
        // - counting errors


        private readonly string _siteName = "rupostings.com";

        public RupostingsParserService()
        {

        }

        public string GetSiteName()
        {
            return _siteName;
        }

        public async Task<BoardItem> ParcePage(string url)
        {
            var result = new BoardItem();

            try
            {
                var html = await GetHtml(url);



            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        // TODO: move to separate service
        private async Task<string> GetHtml(string url)
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
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        // HTTP 404
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
    }
}
