using HW_17_09_23.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW_17_09_23.Controllers;

public class SkillsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    //    private readonly SkillsModel model = new SkillsModel
    //        {
    //            FirstName = "Ваше прізвище, ім'я",
    //            Skills = new List<Skill>
    //            {
    //                new Skill { Name = "Навичка 1", Percentage = 75 },
    //                new Skill { Name = "Навичка 2", Percentage = 90 },
    //                // Додайте інші навички
    //            }
    //        };

    //public IActionResult Index(string searchText)
    //    {
    //        List<Skill> filteredSkills;

    //        if (string.IsNullOrWhiteSpace(searchText))
    //        {
    //            // Повернути всі навички, якщо пошуковий текст пустий
    //            filteredSkills = model.Skills;
    //        }
    //        else
    //        {
    //            // Фільтрувати навички за введеним текстом
    //            filteredSkills = model.Skills
    //                .Where(skill => skill.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
    //                .ToList();
    //        }

    //        return View(filteredSkills);
    //    }

    //    public ActionResult Skills()
    //    {
    //        // Заповнюємо дані для сторінки навичок
    //        var model = new SkillsModel
    //        {
    //            LastName = "Ваше прізвище",
    //            FirstName = "Ваше ім'я",
    //            Skills = new List<Skill>
    //        {
    //            new Skill { Name = "Навичка 1", Percentage = 80 },
    //            new Skill { Name = "Навичка 2", Percentage = 70 },
    //            // Додавайте інші навички тут
    //        }
    //        };

    //        return View(model);
    //    }



}
