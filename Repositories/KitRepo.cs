using System;
using System.Collections.Generic;
using System.Data;
using Legomaster.Models;
using Dapper;

namespace Legomaster.Repos
{
  public class KitRepo
  {
    private readonly IDbConnection _db;
    public KitRepo(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Kit> Get()
    {
      string sql = "SELECT * FROM kits";
      return _db.Query<Kit>(sql);
    }

    internal Kit GetById(object id)
    {
      string sql = "SELECT * FROM kits WHERE id = @id";
      return _db.QueryFirstOrDefault<Kit>(sql, new { id });
    }
    internal Kit Edit(Kit original)
    {
      {
        string sql = @"
        UPDATE kits
        SET
            name = @Name,
            piececount = @Piececount,
            price = @Price,
            owner = @Owner
        WHERE id = @Id;
        SELECT * FROM kits WHERE id = @id;";
        return _db.QueryFirstOrDefault<Kit>(sql, original);
      }
    }
    internal int Create(Kit newKit)
    {
      string sql = @"
        INSERT INTO kits
        (piececount, name, price, owner)
        VALUES
        (@Piececount, @Name, @Price, @Owner);
        SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newKit);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM kits WHERE id = @id";
      _db.Execute(sql, new { id });
    }


  }
}