using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using University.Models;
using University.Models.Repository;

namespace University.Controllers
{
    public class CathedraController : Controller
    {
        [HttpGet]
        public IActionResult GetCathedras()
        {
            try
            {
                var cathedras = CathedraRepository.Get();

                ViewBag.CountCathedras = CountCathedras(cathedras);
                ViewBag.Cathedras = cathedras;
                return View();
            }
            catch
            {
                return Content("Ошибка запроса к базе данных");
            }

        }

        [HttpGet]
        public IActionResult AddCathedra()
        {
            return GetFaculties();
        }
        

        [HttpPost]
        public IActionResult AddCathedra(int Id, string CathedraName, int FoundationYear,string FacultyName)
        {
            string[] Faculty = FacultyName.Split(new char[] { '.' });
            CathedraModel cathedra = new CathedraModel() {Id = Id, CathedraName=CathedraName, FoundationYear=FoundationYear, FacultyId= Convert.ToInt32(Faculty[0]) };
            if (ModelState.IsValid)
            {
                CathedraRepository.Post(cathedra);
                return Content($"Добавлена кафедра \n Название кафедры: {cathedra.CathedraName} \n " +
                                                 $"Год основания кафедры: {cathedra.FoundationYear} \n " +
                                                 $"Факультет: {cathedra.FacultyId}");
            }
            else
            {
                return GetFaculties();
            }
        }

        public IActionResult DeleteCathedra(int Id)
        {
            try
            {
                CathedraRepository.Delete(Id);
                return View();
            }
            catch
            {
                return Content("Ошибка удаления кафедры " + Id);
            }
        }
                
        [HttpGet]
        public IActionResult СhangeCathedra()
        {
            return GetFaculties();
        }

        [HttpPut]
        public IActionResult СhangeCathedra(int Id, string CathedraName, int FoundationYear, string FacultyName)
        {
            string[] Faculty = FacultyName.Split(new char[] { '.' });
            CathedraModel cathedra = new CathedraModel() { Id = Id, CathedraName = CathedraName, FoundationYear = FoundationYear, FacultyId = Convert.ToInt32(Faculty[0]) };
            if (ModelState.IsValid)
            {
                try
                {
                    CathedraRepository.Put(cathedra);
                    return Content($"Кафедра изменена \n Название кафедры: {cathedra.CathedraName} \n " +
                                                     $"Год основания кафедры: {cathedra.FoundationYear} \n " +
                                                     $"Факультет: {cathedra.FacultyId}");
                }
                catch
                {
                    return Content ("Ошибка изменения кафедры " + Id);
                }
            }
            else
            {
                ViewBag.Faculty = GetFaculties();            
                return View();
            }
        }

        public IActionResult GetProfessors()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetProfessors(int Id)
        {
            try
            {
                var professors = CathedraRepository.GetProfessors(Id);
                ViewBag.Professors = professors;
                ViewBag.Count = professors.Count();
                return View();
            }
            catch
            {
                return Content("Ошибка обращения к базе данных");
            }
        }

        private IActionResult GetFaculties()
        {
            try
            {

                ViewBag.Faculty = FacultyRepository.Get();
                return View();
            }
            catch
            {
                return Content("Ошибка запроса к таблице \"Факультеты\"");
            }
        }

        private string CountCathedras(IEnumerable<dynamic> cathedras)
        {
            string countCathedras;
            if (cathedras.Count() == 0)
            {
                countCathedras = "Кафедры не найдены";
            }
            else if (cathedras.Count() == 1) { countCathedras = $"Всего {cathedras.Count()} кафедра"; }
            else if (cathedras.Count() > 1 && cathedras.Count() < 5)
            {
                countCathedras = $"Всего {cathedras.Count()} кафедры";
            }
            else
            {
                countCathedras = $"Всего {cathedras.Count()} кафедр";
            }
            return countCathedras;
        }
    }
}