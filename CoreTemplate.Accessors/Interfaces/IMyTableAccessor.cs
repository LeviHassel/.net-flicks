using CoreTemplate.Accessors.Models.DTO;
using System.Collections.Generic;

namespace CoreTemplate.Accessors.Interfaces
{
  public interface IMyTableAccessor
  {
    MyTableDTO Save(MyTableDTO entity);

    List<MyTableDTO> GetAll();

    MyTableDTO Get(int id);
  }
}
