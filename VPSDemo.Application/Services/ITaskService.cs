using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPSDemo.Application.Services
{
    public interface ITaskService
    {
        public Result<Domain.Entities.Task> Create(string title, string? description, int effortEstimation);
        public Result<Domain.Entities.Task> Edit(int id, string? title, string? description, int? effortEstimation, string? status);
        public Result<Domain.Entities.Task> Get(int id);
        public Result Delete(int id);
        public Result<Domain.Entities.Task> AssignSubTask(int id, int parentId);
    }
}
