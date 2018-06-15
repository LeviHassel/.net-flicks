using DotNetFlicks.Accessors.Models.DTO;
using DotNetFlicks.Common.Models;
using System.Collections.Generic;

namespace DotNetFlicks.Accessors.Interfaces
{
    public interface IPersonAccessor
    {
        PersonDTO Get(int id);

        List<PersonDTO> GetRequest(IndexRequest query);

        List<PersonDTO> GetAllByName(string query);

        int GetCount(string search);

        PersonDTO Save(PersonDTO dto);

        PersonDTO Delete(int id);
    }
}
