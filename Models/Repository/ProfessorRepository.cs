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
    public class ProfessorRepository
    {
        private static string ConnectionString { get => ConfigurationManager.ConnectionStrings["UniversityDB"].ConnectionString; }

        public static IEnumerable<ProfessorModel> Get(int id)
        {
            return DbQuery($"SELECT * FROM ProfessorTable WHERE id={id}");
        }

        public static IEnumerable<ProfessorModel> Post(ProfessorModel professor)
        {
            var date = professor.StartDate.Date.ToString("yyyy-MM-dd");
            return DbQuery("INSERT INTO ProfessorTable " +
                $"VALUES ({professor.Id}, {professor.CathedraId}, {date}, '{professor.Surname}','{professor.Name}'" +
                $"'{professor.Patronymic}', '{professor.WorkPosition}', '{professor.AcademicDegree}', '{professor.Other}')");
        }

        public static IEnumerable<ProfessorModel> Put(ProfessorModel professor)
        {
            var date = professor.StartDate.Date.ToString("yyyy-MM-dd");
            return DbQuery("UPDATE ProfessorTable " +
                            $"SET СathedraId= {professor.CathedraId}, " +
                            $"StartDate={date}, " +
                            $"Surname='{professor.Surname}' " +
                            $"Name='{professor.Name}'"+
                            $"Patronymic='{professor.Patronymic}'"+
                            $"WorkPosition='{professor.WorkPosition}'" +
                            $"AcademicDegree='{professor.AcademicDegree}'" +
                            $"Other='{professor.Other}'" +
                            $"Where id={professor.Id}");
        }

        public static void Delete(int id)
        {
            DbQuery($"DELETE FROM ProfessorTable WHERE id={id}");
        }


        private static IEnumerable<ProfessorModel> DbQuery(string query)
        {            
            IEnumerable<ProfessorModel> professor = null;
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                db.Open();
                professor = db.Query<ProfessorModel>(query);
                db.Close();
            }

            return professor;
        }
    }
}
