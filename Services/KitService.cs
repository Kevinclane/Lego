using System;
using System.Collections.Generic;
using Legomaster.Models;
using Legomaster.Repos;

namespace Legomaster.Services
{
  public class KitService
  {
    private readonly KitRepo _repo;
    public KitService(KitRepo repo)
    {
      _repo = repo;
    }

    public IEnumerable<Kit> Get()
    {
      return _repo.Get();
    }

    public Kit Get(int id)
    {
      Kit exists = _repo.GetById(id);
      if (exists == null) { throw new Exception("Invalid lego bruh"); }
      return exists;
    }
    public Kit Edit(Kit editKit, string UserEmail)
    {
      Kit original = Get(editKit.Id);
      if (original.Owner != UserEmail)
      {
        throw new UnauthorizedAccessException("You do not own this kit!");
      }
      else
      {

        original.Name = editKit.Name.Length > 0 ? editKit.Name : original.Name;
        original.PieceCount = editKit.PieceCount > 0 ? editKit.PieceCount : original.PieceCount;
        original.Price = editKit.Price > 0 ? editKit.Price : original.Price;
        return _repo.Edit(original);
      }
    }
    public Kit Create(Kit newKit)
    {
      int id = _repo.Create(newKit);
      newKit.Id = id;
      return newKit;
    }

    public Kit Delete(int id, string UserEmail)
    {
      Kit exists = Get(id);
      if (exists.Owner != UserEmail)
      {
        throw new UnauthorizedAccessException("You do not own this kit!");
      }
      else
      {
        _repo.Delete(id);
        return exists;
      }
    }


  }
}