using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Common.Configuration;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Interfaces
{
    public interface IPersonAccessor
    {
        PersonDTO Get(int id);

        List<PersonDTO> GetAll();

        List<PersonDTO> GetQuery(IndexQuery query);

        List<PersonDTO> GetAllByName(string query);

        int GetCount();

        PersonDTO Save(PersonDTO dto);

        PersonDTO Delete(int id);
    }
}
