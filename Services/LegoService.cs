using System;
using System.Collections.Generic;
using Legomaster.Models;
using Legomaster.Repos;

namespace Legomaster.Services
{
  public class LegoService
  {
    private readonly LegoRepo _repo;
    public LegoService(LegoRepo repo)
    {
      _repo = repo;
    }

    public IEnumerable<Lego> Get()
    {
      return _repo.Get();
    }

    public Lego Get(int id)
    {
      Lego exists = _repo.GetById(id);
      if (exists == null) { throw new Exception("Invalid lego bruh"); }
      return exists;
    }
    public Lego Edit(Lego editLego, string UserEmail)
    {
      Lego original = Get(editLego.Id);
      if (original.Owner != UserEmail)
      {
        throw new UnauthorizedAccessException("You do not own this kit!");
      }
      original.Name = editLego.Name.Length > 0 ? editLego.Name : original.Name;
      original.Size = editLego.Size.Length > 0 ? editLego.Size : original.Size;
      return _repo.Edit(original);
    }
    public Lego Create(Lego newLego)
    {
      int id = _repo.Create(newLego);
      newLego.Id = id;
      return newLego;
    }

    public Lego Delete(int id, string UserEmail)
    {
      Lego exists = Get(id);
      if (exists.Owner != UserEmail)
      {
        throw new UnauthorizedAccessException("You do not own this kit!");
      }
      _repo.Delete(id);
      return exists;
    }
  }
}