using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoreTemplate.Accessors.Accessors.Base;
using CoreTemplate.Accessors.Interfaces;
using CoreTemplate.Accessors.Models.DTO;
using CoreTemplate.Accessors.Models.EF;
using CoreTemplate.Accessors.Models.EF.Base;
using Microsoft.EntityFrameworkCore;

namespace CoreTemplate.Accessors.Accessors
{
  public class MyRepository : EntityAccessor<Entity>, IMyTableAccessor
  {
    public MyRepository(CoreTemplateContext db) : base(db)
    {
    }

    public MyTableDTO Get(int id)
    {
      var entity = _db.MyTables
        .Single(x => x.Id == id);

      var dto = Mapper.Map<MyTableDTO>(entity);

      return dto;
    }

    public List<MyTableDTO> GetAll()
    {
      var entities = _db.MyTables
        .AsNoTracking()
        .ToList();

      var dtos = Mapper.Map<List<MyTableDTO>>(entities);

      return dtos;
    }

    public MyTableDTO Save(MyTableDTO dto)
    {
      try
      {
        var entity = Mapper.Map<MyTable>(dto);

        if (string.IsNullOrEmpty(dto.Foo))
        {
          throw new ArgumentException("MyTable requires Foo");
        }

        if (dto.Id == 0)
        {
          //added
          _db.MyTables.Add(entity);
        }
        else
        {
          //modified
          _db.Entry(entity).State = EntityState.Modified;
        }

        _db.SaveChanges();

        var returnDto = Mapper.Map<MyTableDTO>(entity);

        return returnDto;
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
