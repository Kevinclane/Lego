using System;
using System.Collections.Generic;
using Legomaster.Models;
using Legomaster.Repos;

namespace Legomaster.Services
{
  public class LegoKitService
  {
    private readonly LegoKitRepo _repo;
    public LegoKitService(LegoKitRepo repo)
    {
      _repo = repo;
    }

    public DTOLegoKit Get(int Id)
    {
      DTOLegoKit exists = _repo.GetById(Id);
      if (exists == null) { throw new Exception("Invalid Lego Kit"); }
      return exists;
    }
    public DTOLegoKit Create(DTOLegoKit newLegoKit)
    {
      int id = _repo.Create(newLegoKit);
      newLegoKit.Id = id;
      return newLegoKit;
    }

    public DTOLegoKit Delete(int id)
    {
      DTOLegoKit exists = Get(id);
      _repo.Delete(id);
      return exists;
    }

    public IEnumerable<LegoKit> GetLegosByKitId(int id)
    {
      return _repo.GetLegosByKitId(id);
    }
  }
}