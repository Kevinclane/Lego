using System;
using System.Collections.Generic;
using System.Data;
using Legomaster.Models;
using Dapper;

namespace Legomaster.Repos
{
  public class LegoRepo
  {
    private readonly IDbConnection _db;
    public LegoRepo(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Lego> Get()
    {
      string sql = "SELECT * FROM legos";
      return _db.Query<Lego>(sql);
    }

    internal Lego GetById(object id)
    {
      string sql = "SELECT * FROM legos WHERE id = @id";
      return _db.QueryFirstOrDefault<Lego>(sql, new { id });
    }
    internal Lego Edit(Lego original)
    {
      {
        string sql = @"
        UPDATE legos
        SET
            name = @Name,
            kcal = @Kcal
        WHERE id = @Id;
        SELECT * FROM legos WHERE id = @id;";
        return _db.QueryFirstOrDefault<Lego>(sql, original);
      }
    }
    internal int Create(Lego newLego)
    {
      string sql = @"
        INSERT INTO legos
        (size, name)
        VALUES
        (@Size, @Name);
        SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newLego);
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM legos WHERE id = @id";
      _db.Execute(sql, new { id });
    }


  }
}