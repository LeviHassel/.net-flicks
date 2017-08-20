using AutoMapper;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Accessors.Models.EF;

namespace CoreTemplate.Accessors.Config
{
  public class AutoMapperAccessorsConfig : Profile
  {
    public AutoMapperAccessorsConfig()
    {
      CreateMap<MyTable, MyTableDTO>().ReverseMap();
    }

    public override string ProfileName
    {
      get { return this.GetType().ToString(); }
    }
  }
}
