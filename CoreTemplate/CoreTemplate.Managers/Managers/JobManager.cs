using AutoMapper;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Common.Helpers;
using CoreTemplate.Managers.Interfaces;
using CoreTemplate.ViewModels.Job;
using System.Collections.Generic;
using System.Linq;

namespace CoreTemplate.Managers.Managers
{
    public class JobManager : IJobManager
    {
        private IJobAccessor _jobAccessor;
        private IMoviePersonAccessor _moviePersonAccessor;

        public JobManager(IJobAccessor jobAccessor,
            IMoviePersonAccessor moviePersonAccessor)
        {
            _jobAccessor = jobAccessor;
            _moviePersonAccessor = moviePersonAccessor;
        }

        public JobViewModel Get(int? id)
        {
            var jobDto = id.HasValue ? _jobAccessor.Get(id.Value) : new JobDTO();
            var moviePersonDtos = id.HasValue ? _moviePersonAccessor.GetAllByJob(jobDto.Id) : new List<MoviePersonDTO>();

            var vm = Mapper.Map<JobViewModel>(jobDto);

            if (moviePersonDtos != null && moviePersonDtos.Any())
            {
                vm.PeopleCount = moviePersonDtos.Count();
                vm.PeopleTooltip = ListHelper.GetBulletedList(moviePersonDtos.Select(x => string.Format("{0} - {1}", x.Person.FullName, x.Movie.Name)).ToList());
            }

            return vm;
        }

        public JobsViewModel GetAll()
        {
            var jobDtos = _jobAccessor.GetAll();
            var moviePersonDtos = _moviePersonAccessor.GetAll().OrderBy(x => x.Person.FirstName);

            var vms = Mapper.Map<List<JobViewModel>>(jobDtos);

            foreach (var vm in vms)
            {
                var people = moviePersonDtos.Where(x => x.JobId == vm.Id);

                if (people != null && people.Any())
                {
                    vm.PeopleCount = people.Count();
                    vm.PeopleTooltip = ListHelper.GetTooltipList(people.Select(x => string.Format("{0} - {1}", x.Person.FullName, x.Movie.Name)).ToList());
                }
            }

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
