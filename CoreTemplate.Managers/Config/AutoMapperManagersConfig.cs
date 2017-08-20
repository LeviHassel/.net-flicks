using AutoMapper;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Managers.ViewModels.Home;

namespace CoreTemplate.Managers.Config
{
  public class AutoMapperManagersConfig : Profile
  {
    public AutoMapperManagersConfig()
    {
      CreateMap<MyTableDTO, MyTableViewModel>().ReverseMap();
    }

    public override string ProfileName
    {
      get { return this.GetType().ToString(); }
    }
  }
}
