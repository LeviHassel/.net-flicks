using CoreTemplate.ViewModels.Job;

namespace CoreTemplate.Managers.Interfaces
{
    public interface IJobManager
    {
        JobViewModel Get(int? id);

        JobsViewModel GetAll();

        JobViewModel Save(JobViewModel vm);

        JobViewModel Delete(int id);
    }
}
