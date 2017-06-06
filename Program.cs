using System;
using System.Web;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace Cognitive
{
    class MainClass
    {
        private class UrlImage{
            public string url;
        }


        public static void Main(string[] args)
        {

			Console.WriteLine("Type image url....");
            string url = Console.ReadLine();

            MakeRequest(url);
			Console.WriteLine("Calcolo in corso....");
			Console.ReadLine();
        }

		static async void MakeRequest(string pUrl)
		{
			var client = new HttpClient();
			var queryString = HttpUtility.ParseQueryString(string.Empty);

			// Request headers
			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "dd14a6064e864fa7951cec3f7794c63b");

			// Request parameters
			queryString["returnFaceId"] = "true";
			queryString["returnFaceLandmarks"] = "false";
			queryString["returnFaceAttributes"] = "age,gender";
			var uri = "https://westeurope.api.cognitive.microsoft.com/face/v1.0/detect?" + queryString;

			HttpResponseMessage response;

			// Request body
			//byte[] byteData = Encoding.UTF8.GetBytes("{body}");

            JavaScriptSerializer js = new JavaScriptSerializer();


            using (var content = new StringContent(js.Serialize(new UrlImage() { url = pUrl })))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync (uri, content);
                var contents = await response.Content.ReadAsStringAsync();
                Console.Write(contents);
                //Console.ReadLine();
            }

		}
    }
}
