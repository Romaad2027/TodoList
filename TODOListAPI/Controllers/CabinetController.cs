using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TODOListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinetController : ControllerBase
    {
        private TODOListEFContext db = new TODOListEFContext();

        [HttpGet]
        [Route("tasks")]
        public IActionResult GetAllTasks()
        {
            User user = db.Users.FirstOrDefault(u => u.Name == HttpContext.User.Identity.Name);
            var ToDoItems = db.Tasks.FromSqlRaw($"SELECT * FROM Task WHERE idUser = {user.Id}").ToList();

            var response = new
            {
                number = $"{ToDoItems.Count}"
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("week-tasks")]
        public IActionResult GetWeekTasks()
        {
            User user = db.Users.FirstOrDefault(u => u.Name == HttpContext.User.Identity.Name);
            var items = db.Tasks.Where(i => i.IdUser == user.Id).ToList();
            items = items.Where(i => (DateTime.Now - i.AddDate).TotalDays < 8).ToList();
            return Ok(items.Count());
        }

        [HttpGet]
        [Route("productivity")]
        public IActionResult GetProductivity()
        {
            User user = db.Users.FirstOrDefault(u => u.Name == HttpContext.User.Identity.Name);
            var items = db.Tasks.Where(i => i.IdUser == user.Id).ToList();
            var doneItems = items.Where(i => i.IsDone == true).ToList();
            float result = 100 * (float)doneItems.Count / (float)items.Count;
            return Ok(result);
        }
    }
}
