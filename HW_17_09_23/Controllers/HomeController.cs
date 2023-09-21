using HW_17_09_23.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW_17_09_23.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        // Заповнюємо дані для головної сторінки
        var model = new HomeModel
        {
            PhotoPath = "/images/user-image.png",
            AboutMe = "Тут ваша інформація про себе."
        };
        return View(model);
    }
    private static readonly List<Skill> allSkills = new List<Skill>
    {
        new Skill { Name = "Навичка 1", Percentage = 80 },
        new Skill { Name = "Навичка 2", Percentage = 70 },
        // Додайте інші навички
    };
    private readonly SkillsModel model = new SkillsModel
    {
        FirstName = "Ваше прізвище, ім'я",
        Skills = allSkills,
    };
    //public IActionResult Skills()
    //{
    //    List<Skill> filteredSkills;
    //    string searchText = "";
    //    if (string.IsNullOrWhiteSpace(searchText))
    //    {
    //        // Повернути всі навички, якщо пошуковий текст пустий
    //        filteredSkills = model.Skills;
    //    }
    //    else
    //    {
    //        // Фільтрувати навички за введеним текстом
    //        filteredSkills = model.Skills
    //            .Where(skill => skill.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
    //            .ToList();
    //    }

    //    return View(filteredSkills);
    //}
    public IActionResult Skills(string searchText)
    {
        List<Skill> filteredSkills;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            // Повернути всі навички, якщо пошуковий текст пустий
            filteredSkills = allSkills;
        }
        else
        {
            // Фільтрувати навички за введеним текстом
            filteredSkills = allSkills
                .Where(skill => skill.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        model.Skills = filteredSkills;
        return View(model);
    }
}
