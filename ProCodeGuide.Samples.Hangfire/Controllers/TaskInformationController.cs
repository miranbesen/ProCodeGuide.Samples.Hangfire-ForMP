using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProCodeGuide.Samples.Hangfire.Model;
using ProCodeGuide.Samples.Hangfire.Model.Context;

namespace ProCodeGuide.Samples.Hangfire.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskInformationController : Controller
    {
        private readonly ProCodeGuideSamplesHangfireContext _dbContext;

        public TaskInformationController(ProCodeGuideSamplesHangfireContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTaskInformation()
        {
            return Ok(await _dbContext.TaskInformations.ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addTaskInformation"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddTaskInformation(TaskInformation addTaskInformation)
        {
            var taskInformation = new TaskInformation()
            {
                Id = addTaskInformation.Id,
                ScheduleTime = addTaskInformation.ScheduleTime,
                ServiceUrl = addTaskInformation.ServiceUrl,
                ToMail = addTaskInformation.ToMail
            };
            await _dbContext.TaskInformations.AddAsync(taskInformation);
            await _dbContext.SaveChangesAsync();
            return Ok(taskInformation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateTaskInformation"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateTaskInformation(TaskInformation updateTaskInformation)
        {
            var taskInformation = _dbContext.TaskInformations.Find(updateTaskInformation.Id);


            taskInformation.ScheduleTime = updateTaskInformation.ScheduleTime;
            taskInformation.ToMail = updateTaskInformation.ToMail;
            taskInformation.ServiceUrl = updateTaskInformation.ServiceUrl;

            await _dbContext.SaveChangesAsync();
            return Ok(taskInformation);

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteTaskInformation(int id)
        {

            var taskInformation = _dbContext.TaskInformations.Find(id);
            _dbContext.TaskInformations.Remove(taskInformation);
            await _dbContext.SaveChangesAsync();
            return Ok(taskInformation);

        }


    }
}
