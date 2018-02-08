using CoreTemplate.Accessors.Models.DTO;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Interfaces
{
    public interface IJobTitleAccessor
    {
        JobTitleDTO Get(int id);

        List<JobTitleDTO> GetAll();

        JobTitleDTO Save(JobTitleDTO dto);

        JobTitleDTO Delete(int id);
    }
}
