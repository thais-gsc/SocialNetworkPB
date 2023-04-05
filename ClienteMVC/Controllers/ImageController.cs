using ClienteMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ClienteMVC.Controllers
{
    public class ImageController : Controller
    {
        // GET: ImageController
        public async Task<ActionResult> Index()
        {
            List<BlobDto> imagens = new List<BlobDto>();

            MemoryStream stream = new();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Storage/Get"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    imagens = JsonConvert.DeserializeObject<List<BlobDto>>(apiResponse);

                    //foreach(var imagem in imagens) {
                    //    using (var responseAPI = await httpClient.GetAsync(imagem.Uri))
                    //    {
                    //        response.EnsureSuccessStatusCode();

                    //        stream = (MemoryStream)await response.Content.ReadAsStreamAsync();
                    //    }
                    //}
                }

            }
            return View(imagens);
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Create(BlobDto imagem)
        {
            BlobDto addImagem = new BlobDto();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                StringContent content = new StringContent(JsonConvert.SerializeObject(imagem), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:5001/api/Storage/Upload", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    addImagem = JsonConvert.DeserializeObject<BlobDto>(apiResponse);
                }
            }

            return View(addImagem);
        }
    }
}
