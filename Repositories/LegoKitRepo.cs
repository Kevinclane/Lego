using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Legomaster.Models;

namespace Legomaster.Repos
{
  public class LegoKitRepo
  {
    private readonly IDbConnection _db;
    public LegoKitRepo(IDbConnection db)
    {
      _db = db;
    }


    internal DTOLegoKit GetById(int Id)
    {
      string sql = "SELECT * FROM legokits WHERE id = @Id";
      return _db.QueryFirstOrDefault<DTOLegoKit>(sql, new { Id });
    }

    internal int Create(DTOLegoKit newDTOLegoKit)
    {
      string sql = @"
        INSERT INTO legokits
        (kitId, legoId)
        VALUES
        (@KitId, @LegoId);
        SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newDTOLegoKit);
    }

    internal void Delete(int Id)
    {
      string sql = "DELETE FROM legokits WHERE id = @Id";
      _db.Execute(sql, new { Id });
    }

    internal IEnumerable<LegoKit> GetLegosByKitId(int id)
    {
      string sql = @"
        SELECT
            l.*,
            lk.id as legoKitId
        FROM legokits lk
        INNER JOIN legos l ON l.id = lk.legoId
        WHERE(lk.kitId = @id)
        ";
      return _db.Query<LegoKit>(sql, new { id });
    }
  }
}