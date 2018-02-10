using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Job;
using System.Collections.Generic;

namespace CoreTemplate.Managers.Managers
{
    public class JobManager : IJobManager
    {
        private IJobAccessor _jobAccessor;

        public JobManager(IJobAccessor jobAccessor)
        {
            _jobAccessor = jobAccessor;
        }

        public JobViewModel Get(int? id)
        {
            var dto = id.HasValue ? _jobAccessor.Get(id.Value) : new JobDTO();
            var vm = Mapper.Map<JobViewModel>(dto);

            return vm;
        }

        public JobsViewModel GetAll()
        {
            var dtos = _jobAccessor.GetAll();
            var vms = Mapper.Map<List<JobViewModel>>(dtos);

            return new JobsViewModel { Jobs = vms };
        }

        public JobViewModel Save(JobViewModel vm)
        {
            var dto = Mapper.Map<JobDTO>(vm);
            dto = _jobAccessor.Save(dto);
            vm = Mapper.Map<JobViewModel>(dto);

            return vm;
        }

        public JobViewModel Delete(int id)
        {
            var dto = _jobAccessor.Delete(id);
            var vm = Mapper.Map<JobViewModel>(dto);

            return vm;
        }
    }
}
