using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPSDemo.Api.Contracts.Task;
using VPSDemo.Api.Validations;
using VPSDemo.Application.Services;

namespace VPSDemo.Api.Controllers
{
    [Route("[controller]")]
    public class TasksController : BaseController
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;
        public TasksController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create(TaskCreateRequest req)
        {
            var result = _taskService.Create(
                req.Title,
                req.Description,
                req.EffortEstimation);
            var taskResponse = _mapper.Map<TaskResponse>(result.Value);
            return Ok(taskResponse);
        }

        [HttpPut("{identifier}")]
        public IActionResult Edit(int identifier, TaskEditRequest req)
        {
            var identifierValidation = BaseValidation.ValidateIdentifier(identifier);
            if (identifierValidation.IsFailed)
                return ProblemHandler(identifierValidation.Errors);

            var result = _taskService.Edit(
                identifier,
                req.Title,
                req.Description,
                req.EffortEstimation,
                req.Status);

            var taskResponse = _mapper.Map<TaskResponse>(result.Value);
            return Ok(taskResponse);

        }

        [HttpGet("{identifier}")]
        public IActionResult Get(int identifier)
        {
            var paramValidation = BaseValidation.ValidateIdentifier(identifier);
            if (paramValidation.IsFailed)
                return ProblemHandler(paramValidation.Errors);

            var result = _taskService.Get(identifier);
            if (result.IsFailed)
                return ProblemHandler(result.Errors);

            var taskResponse = _mapper.Map<TaskResponse>(result.Value);
            return Ok(taskResponse);
        }

        [HttpDelete("{identifier}")]
        public IActionResult Delete(int identifier)
        {
            var paramValidation = BaseValidation.ValidateIdentifier(identifier);
            if (paramValidation.IsFailed)
                return ProblemHandler(paramValidation.Errors);

            var result = _taskService.Delete(identifier);
            if (result.IsFailed)
                return ProblemHandler(result.Errors);

            return NoContent();

        }
        [HttpPut("[action]/{identifier}")]
        public IActionResult AssignSubTask(int identifier, int parentIdentifier)
        {
            var firstParamValidation = BaseValidation.ValidateIdentifier(identifier);
            if (firstParamValidation.IsFailed)
                return ProblemHandler(firstParamValidation.Errors);

            var secondParamValidation = BaseValidation.ValidateIdentifier(parentIdentifier);
            if (secondParamValidation.IsFailed)
                return ProblemHandler(secondParamValidation.Errors);

            var result = _taskService.AssignSubTask(identifier, parentIdentifier);
            if (result.IsFailed)
                return ProblemHandler(result.Errors);

            var taskResponse = _mapper.Map<TaskResponse>(result.Value);
            return Ok(taskResponse);
        }
    }
}
