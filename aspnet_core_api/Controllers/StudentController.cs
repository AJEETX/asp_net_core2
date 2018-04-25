using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnet_core_api.Model;
using aspnet_core_api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_core_api.Controllers
{
    [Produces("application/json")]
    [Route("api/Student")]
    public class StudentController : Controller
    {
        IRepository<Student> _repo;
        public StudentController(IRepository<Student> repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            var students= _repo.GetAll();
            return students;
        }
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return _repo.Get(id);
        }

        [HttpPost]
        public void Post([FromBody]Student student)
        {
            _repo.Add(student);
        }

        [HttpPut]
        public void Put([FromBody]Student student)
        {
            _repo.Update(student);
        }

        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}