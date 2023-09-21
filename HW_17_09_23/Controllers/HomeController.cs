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
            PhotoPath = "/images/user-image.jpg",
            AboutMe = "Олексій Сердюк, Full-stack developer"
        };
        return View(model);
    }
    private static readonly List<Skill> allSkills = new List<Skill>
    {
        new Skill { Name = "C#", Percentage = 90 },
        new Skill { Name = "ASP.NET Core", Percentage = 60 },
        new Skill { Name = "HTML", Percentage = 70 },
        new Skill { Name = "CSS", Percentage = 60 },
        new Skill { Name = "JavaScript", Percentage = 50 },
        new Skill { Name = "C++", Percentage = 50 },
        new Skill { Name = "Java", Percentage = 60 },
        new Skill { Name = "Android App developing(Java)", Percentage = 50 },
        new Skill { Name = "ADO.NET", Percentage = 80 },
        new Skill { Name = "Windows Forms", Percentage = 90 },
        new Skill { Name = "WPF", Percentage = 80 },
        new Skill { Name = "SQL", Percentage = 80 },
        new Skill { Name = "PHP", Percentage = 50 },
        new Skill { Name = "Laravel", Percentage = 50 },
        new Skill { Name = "Azure Cloud Services", Percentage = 50 },
        // Додайте інші навички
    };
    private readonly SkillsModel model = new SkillsModel
    {
        FirstName = "Олексій",
        LastName = "Сердюк",
        Skills = allSkills,
    };

    public async Task<IActionResult> Skills(string searchText)
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
