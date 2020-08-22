using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AmazonLoginMvc.Models;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using System.Text.Json;

namespace MvcMovie.Controllers
{
    public class AlexaTokenRequest
    {
        public String grant_type {get; set;}
        public String code {get; set;}
        public String client_id {get; set;}
        public String client_secret {get; set;}
    }

    public class AmazonTokenResponse
    {
        public String access_token {get; set;}
        public int expires_in {get; set;}
        public String refresh_token {get; set;}
        public String token_type {get; set;}
    }

    public class Data
    {
        public string value {get; set;}
    }

    public class AccessTokenController : Controller
    {
        private readonly string AmazonTokenUrl = "https://api.amazon.com/auth/o2/token";

        [HttpPost]
        public ActionResult Index(AlexaTokenRequest alexaRequest)
        {
            //var values = Request.Form;
            
            if(!(alexaRequest is null || String.IsNullOrEmpty(alexaRequest.code)))
            {
                var responseAccessToken = JsonSerializer.Deserialize<AmazonTokenResponse>(getAccessToken(alexaRequest.code).GetAwaiter().GetResult());

                return Json(new { 
                    access_token = responseAccessToken.access_token,
                    token_type = responseAccessToken.token_type,
                    expires_in = responseAccessToken.expires_in,
                    refresh_token = responseAccessToken.refresh_token
                });
            }

            return new BadRequestResult();
        }

         private async Task<string> getAccessToken(string authCode)
        {
            string data = "grant_type=authorization_code&code=" + authCode + "&client_id=amzn1.application-oa2-client.b16223dc11ee4cd3bd4b01ada15d11e1";
            data += "&client_secret=b21a5d9c158b89eea3854c919e697be1b8fe6a9a3eea2a0c5f379fb74f57964d";
            var client = new HttpClient();
            var queryString = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            var response = await client.PostAsync(AmazonTokenUrl, queryString);

            return  await response.Content.ReadAsStringAsync();
        }

        [HttpPost]
        public ActionResult SetData(Data value)
        {
            return Json(new { result = "Data has been set"});
        }
    }
}