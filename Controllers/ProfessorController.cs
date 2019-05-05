using Microsoft.AspNetCore.Mvc;
using System.Linq;
using University.Models;
using University.Models.Repository;

namespace University.Controllers
{
    public class ProfessorController : Controller
    {
        public IActionResult GetProfessors()
        {
            return View();
        }

        public IActionResult GetProfessors(int Id)
        {
            try
            {
                var professor = ProfessorRepository.Get(Id);
                ViewBag.IdProfessorIsTrue = professor.ToList().Count() > 0 ? true : false;
                ViewBag.Professor = professor;
                return View();
            }
            catch
            {
                return Content("Ошибка получения данных преподавателя "+Id);
            }
        }

        public IActionResult AddProfessor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProfessor(ProfessorModel professor)
        {
            if (ModelState.IsValid)
            {
              //  try
            //    {
                    ProfessorRepository.Post(professor);
                    return Content($"Преподаватель {professor.Id} {professor.Surname} {professor.Name} {professor.Patronymic} добавлен");
            //    }
            //    catch
           //     {
           //         return Content($"Ошибка добавления преподавателя {professor.Surname} {professor.Name} {professor.Patronymic}");
           //     }
            }
            else return View();
        }

        [HttpPut]
        public IActionResult ChangeProfessor(ProfessorModel professor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProfessorRepository.Put(professor);
                    return Content($"Данные о преподавателе {professor.Surname} {professor.Name} {professor.Patronymic} изменены.");
                }
                catch
                {
                    return Content($"Ошибка изменения данных преподавателя {professor.Name} {professor.Patronymic}");
                }
            }
            else return View();
        }

        public IActionResult DeleteProfessor()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult DeleteProfessor(int Id)
        {
            try
            {
                ProfessorRepository.Delete(Id);
                return View();
            }
            catch
            {
                return Content($"Ошибка удаления преподавателя {Id}");
            }
        }
    }
}
