using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace University.Models.Repository
{
    public static class CathedraRepository
    {
        private static string ConnectionString { get=> ConfigurationManager.ConnectionStrings ["UniversityDB"].ConnectionString; }

        public static IEnumerable<dynamic> Get()
        {                        
            return DbQuery("SELECT CathedraTable.Id, CathedraTable.CathedraName, CathedraTable.FoundationYear, FacultyTable.FacultyName FROM CathedraTable INNER JOIN FacultyTable ON CathedraTable.FacultyId=FacultyTable.Id");
        }

        public static IEnumerable<dynamic> Post(CathedraModel cathedra)
        {
                return DbQuery("INSERT INTO CathedraTable " +
                    $"VALUES ({cathedra.Id}," +
                            $"N'{cathedra.CathedraName}'," +
                            $" {cathedra.FoundationYear}," +
                            $"{cathedra.FacultyId})");            
        }

        public static IEnumerable<dynamic> Put(CathedraModel cathedra)
        {
            return DbQuery("UPDATE CathedraTable " +
                            $"SET CathedraName= '{cathedra.CathedraName}', " +
                            $"FoundationYear={cathedra.FoundationYear}, " +
                            $"FacultyId={cathedra.FacultyId} " +
                            $"Where id={cathedra.Id}");
        }

        public static void Delete(int id)
        {
            DbQuery($"DELETE FROM CathedraTable WHERE id={id}");
        }

        public static IEnumerable<dynamic> GetProfessors(int id)
        {
           return DbQuery("SELECT*FROM CathedraTable " +
                "INNER JOIN ProfessorTable ON " +
                "CathedraTable.Id=ProfessorTable.CathedraId " +
                "INNER JOIN FacultyTable ON " +
                "FacultyTable.Id=CathedraTable.FacultyId " +
                $"WHERE CathedraTable.Id = {id}");
        }

        private static IEnumerable<dynamic> DbQuery(string query)
        {
            
            IEnumerable<dynamic> cathedras = null;
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
               cathedras = db.Query(query);
            }


            return cathedras;
        }


    }
}
