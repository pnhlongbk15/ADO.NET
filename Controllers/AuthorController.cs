using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController
    {
        private readonly DbContext _dbContext;
        public AuthorController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("overview")]
        public JsonResult GetAll()
        {
            _dbContext.Author.GET();
            return new JsonResult(_dbContext.AuthorTable);
        }

        [HttpPost("AddOne")]
        public JsonResult AddOne(string id, string name, int age)
        {
            var data = new AuthorModel(id, name, age);
            _dbContext.Author.AddOne(data);
            _dbContext.Author.INSERT();
            return new JsonResult(_dbContext.AuthorTable);
        }

    }
}
