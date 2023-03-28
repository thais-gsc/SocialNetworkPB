using Azure.Storage.Blobs;
using ClienteMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ClienteMVC.Controllers
{
    public class JogosController : Controller
    {
        private static string LastFileUpload;
        public async Task<IActionResult> Index()
        {
            List<Jogo> listaJogos = new List<Jogo>();

            var accessToken = HttpContext.Session.GetString("JWToken"); 

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Jogos"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    listaJogos = JsonConvert.DeserializeObject<List<Jogo>>(apiResponse);
                }
            }
            return View(listaJogos);
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Create(Jogo jogo)
        {
            Jogo addJogo = new Jogo();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                StringContent content = new StringContent(JsonConvert.SerializeObject(jogo), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:5001/api/Jogos", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    addJogo = JsonConvert.DeserializeObject<Jogo>(apiResponse);
                }
            }

            return View(addJogo);
        }

        public async Task<ActionResult> Update(int id)
        {
            Jogo jogo = new Jogo();
            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Jogos/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    jogo = JsonConvert.DeserializeObject<Jogo>(apiResponse);
                }
            }
            return View(jogo);
        }

        [HttpPost]
        public async Task<ActionResult> Update(Jogo jogo)
        {
            Jogo jogoAtualizado = new Jogo();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                StringContent content = new StringContent(JsonConvert.SerializeObject(jogo), Encoding.UTF8, "application/json");



                using (var response = await httpClient.PutAsync("https://localhost:5001/api/Jogos/" + jogo.Id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    jogoAtualizado = JsonConvert.DeserializeObject<Jogo>(apiResponse);
                }
            }
            return View(jogoAtualizado);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                using (var response = await httpClient.DeleteAsync("https://localhost:5001/api/Jogos/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
