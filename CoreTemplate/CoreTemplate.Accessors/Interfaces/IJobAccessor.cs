using CoreTemplate.Accessors.Models.DTO;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Interfaces
{
    public interface IJobAccessor
    {
        JobDTO Get(int id);

        List<JobDTO> GetAll();

        JobDTO Save(JobDTO dto);

        JobDTO Delete(int id);
    }
}
