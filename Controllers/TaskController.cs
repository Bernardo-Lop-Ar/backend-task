using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Models;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private static List<ModelTask> modelTasks = new List<ModelTask>();

        [HttpGet]
        public ActionResult<List<ModelTask>> SearchTasks()
        {
            return Ok(modelTasks);
        }


        [HttpPost]
        public ActionResult<List<ModelTask>>
            AddTask(ModelTask novo)
        {
            if (string.IsNullOrEmpty(novo.Description) || novo.Description.Length < 10)
            {
                return BadRequest("A descrição deve ter pelo menos 10 caracteres.");
            }

            if (novo.Id == 0 && modelTasks.Count > 0)
                novo.Id = modelTasks[modelTasks.Count - 1].Id + 1;

            modelTasks.Add(novo);
            return Ok(modelTasks);
        }

        [HttpDelete("{id}")]
        public ActionResult<List<ModelTask>> DeleteTask(int id)
        {
            var taskDelete = modelTasks.FirstOrDefault(x => x.Id == id);

            if (taskDelete == null)
            {
                return NotFound("Tarefa não encontrada");
            }

            modelTasks.Remove(taskDelete);

            return Ok(modelTasks);
        }


    }
}
