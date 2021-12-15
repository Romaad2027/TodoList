using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOListAPI.Models.Responses;

namespace TODOListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : Controller
    {
       private TODOListEFContext db = new TODOListEFContext();

        public TaskController()
        {
            //db = context;
        }

        [HttpGet]
        public ActionResult<TaskResponse> Menu()
        {
            TaskResponse model = new TaskResponse(HttpContext.User.Identity.Name);
            if (model == null)
            {
                return NotFound();
            }
            return new ObjectResult(model);
        }

        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody]string title)
        {
            TaskDB item = db.Tasks.FirstOrDefault(i => i.Id == id);
            item.Title = title;
            db.Tasks.Update(item);
            await db.SaveChangesAsync();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            TaskDB deleteItem = db.Tasks.FirstOrDefault(i => i.Id == id);
            if (deleteItem != null)
            {
                db.Tasks.Remove(deleteItem);
                await db.SaveChangesAsync();
                return Ok(deleteItem);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]string title)
        {
            TaskResponse model = new TaskResponse(HttpContext.User.Identity.Name);
            TaskDB item = new TaskDB();
            item.Title = title;
            item.AddDate = DateTime.Now;
            item.IdUser = model.user.Id;
            db.Tasks.Add(item);
            await db.SaveChangesAsync();

            return Ok(item);
        }

        /*[HttpPost]
        public async Task<IActionResult> CreateUpdate(TaskResponse model)
        {
            model.SetUser(HttpContext.User.Identity.Name);
            if (ModelState.IsValid)
            {
                if (model.EditableItem.Id <= 0)
                {
                    model.EditableItem.AddDate = DateTime.Now;
                    model.EditableItem.IdUser = model.user.Id;
                    db.Tasks.Add(model.EditableItem);
                    await db.SaveChangesAsync();
                }
                else
                {
                    TaskDB item = db.Tasks.FirstOrDefault(i => i.Id == model.EditableItem.Id);
                    item.Title = model.EditableItem.Title;
                    db.Tasks.Update(item);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Menu");
            }
            return RedirectToAction("Menu");
        }*/

        [HttpPut("{id}")]
        public async Task<IActionResult> FlagIsDone(int id)
        {
            TaskDB item = db.Tasks.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                item.IsDone = !item.IsDone;
                await db.SaveChangesAsync();
            }
            return Ok(item);
        }
    }
}
