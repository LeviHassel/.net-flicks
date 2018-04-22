using DotNetFlicks.Accessors.Models.DTO;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Interfaces
{
    public interface IPersonAccessor
    {
        PersonDTO Get(int id);

        List<PersonDTO> GetAll();

        int GetCount();

        PersonDTO Save(PersonDTO dto);

        PersonDTO Delete(int id);
    }
}
