using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models.Repository
{
    public static class FacultyRepository
    {
        private static string ConnectionString { get => ConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString; }

        public static IEnumerable<dynamic> Get()
        {
            return DbQuery("SELECT * FROM FacultyTable ORDER BY Id");
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
