using Client.APIUrl;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class ImageController : Controller
    {
        ImageAPI _imageAPI = new ImageAPI();
        List<Image> _images = new List<Image>();
        // GET: ImageController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            HttpClient httpClient = _imageAPI.Initial();
            _images = new List<Image>();
            HttpResponseMessage response = await httpClient.GetAsync("/image");
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = response.Content.ReadAsStringAsync().Result;
                _images = JsonConvert.DeserializeObject<List<Image>>(apiResponse);
            }
            return View(_images);
        }

        // GET: ImageController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ImageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormFile file)
        {
            HttpClient client = _imageAPI.Initial();
            using (var httpClient = new HttpClient())
            {
                var form = new MultipartFormDataContent();
                using (var fileStream = file.OpenReadStream())
                {
                    form.Add(new StreamContent(fileStream), "file", file.FileName);
                    await client.PostAsync("/Image", form);
                    ViewBag.Message = "File uploaded succesfully";
                    ViewBag.AlertClass = "alert alert-success alert-dismissible show";
                    ViewBag.AlertVisibility = "";
                    
                    return RedirectToAction("Index");   
                      
                }
            }

        }

        // GET: ImageController/Edit/5
        public ActionResult New()
        {
            return View("Create");
        }

        // POST: ImageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ImageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            HttpClient client = _imageAPI.Initial();
            var image = new Image();
            HttpResponseMessage responseMessage = await client.DeleteAsync($"/image/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var apiResponse = responseMessage.Content.ReadAsStringAsync().Result;
                return RedirectToAction("Index");
            }

            return View();
        }

        // POST: ImageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
