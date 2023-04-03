using Microsoft.AspNetCore.Mvc;
using ClienteMVC.Services;
using Azure.Storage.Blobs;

namespace ClienteMVC.Controllers
{
    public class StorageController : Controller
    {
        private readonly IAzureStorage _storage;

        public StorageController(IAzureStorage storage)
        {
            _storage = storage;
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile photo)
        {
            var imageUrl = await _storage.UploadAsync(photo);
            TempData["LatestImage"] = imageUrl.ToString();
            return RedirectToAction("LatestImage");
        }

        public ActionResult LatestImage()
        {
            var latestImage = string.Empty;
            if (TempData["LatestImage"] != null)
            {
                ViewBag.LatestImage = Convert.ToString(TempData["LatestImage"]);
            }

            return View();
        }

        //public IActionResult UploadFile()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public IActionResult UploadFile(IFormFileCollection flInput)
        //{

        //    foreach (var item in this.Request.Form.Files)
        //    {

        //        MemoryStream ms = new MemoryStream();

        //        item.CopyTo(ms);

        //        ms.Position = 0;

        //        var fileName = "file_" + Guid.NewGuid() + ".png";

        //        UploadToAzure(fileName, ms);

        //    }

        //    return View();
        //}

        //private static void UploadToAzure(string fileName, MemoryStream ms)
        //{
        //    String azureBlobStorageConnection =
        //        "DefaultEndpointsProtocol=https;AccountName=pbinfnet5;AccountKey=ge+96z23tmEU8//RQAV8QFhUp4zb9jhckK5nxfyrPKyspcgISqyfBwVP4CHVm7/Khbg/hVa/cZfQ+AStU6pQVQ==;EndpointSuffix=core.windows.net";

        //    BlobServiceClient blobServiceClient = new BlobServiceClient(azureBlobStorageConnection);

        //    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("socialnetwork");

        //    BlobClient blobClient = containerClient.GetBlobClient(fileName);

        //    blobClient.Upload(ms);

        //}
    }
}
