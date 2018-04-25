using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using aspnet_core_app.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace aspnet_core_app.Controllers
{
    public class StudentsController : Controller
    {
        StudentAPI _studentAPI = new StudentAPI();
        public async Task<IActionResult> Index()
        {
            List<StudentDTO> dto = new List<StudentDTO>();
            HttpClient client = _studentAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync("api/student");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<StudentDTO>>(result);
            }
            return View(dto);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentDTO student)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _studentAPI.InitializeClient();

                var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PostAsync("api/student", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(student);
        }
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null) return NotFound();

            List<StudentDTO> dto = new List<StudentDTO>();
            HttpClient client = _studentAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync("api/student");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<StudentDTO>>(result);
            }

            var student = dto.SingleOrDefault(m => m.StudentId == id);
            if (student == null) return NotFound();
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, StudentDTO student)
        {
            if (id != student.StudentId) return NotFound();

            if (ModelState.IsValid)
            {
                HttpClient client = _studentAPI.InitializeClient();

                var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PutAsync("api/student", content).Result;
                if (res.IsSuccessStatusCode) return RedirectToAction("Index");
            }
            return View(student);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();

            List<StudentDTO> dto = new List<StudentDTO>();
            HttpClient client = _studentAPI.InitializeClient();
            HttpResponseMessage res = await client.GetAsync("api/student");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<StudentDTO>>(result);
            }

            var student = dto.SingleOrDefault(m => m.StudentId == id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            HttpClient client = _studentAPI.InitializeClient();
            HttpResponseMessage res = client.DeleteAsync($"api/student/{id}").Result;
            if (res.IsSuccessStatusCode) return RedirectToAction("Index");
            return NotFound();
        }
    }
}