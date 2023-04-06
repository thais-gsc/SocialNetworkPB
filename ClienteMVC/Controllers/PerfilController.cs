using ClienteMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ClienteMVC.Controllers
{
    public class PerfilController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Contato> listaContatos = new List<Contato>();

            var accessToken = HttpContext.Session.GetString("JWToken");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Contatos"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    listaContatos = JsonConvert.DeserializeObject<List<Contato>>(apiResponse);
                }
            }
            return View(listaContatos);
        }

        public IActionResult Page1()
        {
            
            return View();
        }
    }
}
