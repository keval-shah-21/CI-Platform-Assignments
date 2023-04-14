using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class SkillController : Controller
{
    private readonly IUnitOfService _unitOfService;

    public SkillController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }
    public IActionResult LoadSkillPage()
    {
        try
        {
            List<SkillVM> skills = _unitOfService.Skill.GetAll();
            return PartialView("_Skill", skills);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading skill: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult AddSkill()
    {
        try
        {
            return PartialView("_AddSkill");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading addSkill: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    [HttpPost]
    public IActionResult AddSkill(SkillVM s)
    {
        try
        {
            _unitOfService.Skill.SaveSkill(s);
            _unitOfService.Save();
            List<SkillVM> skills = _unitOfService.Skill.GetAll();
            return PartialView("_Skill", skills);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading addSkill: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    public IActionResult EditSkill(long id)
    {
        try
        {
            return PartialView("_EditSkill", _unitOfService.Skill.GetSkillById(id));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading editSkill: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    [HttpPut]
    public IActionResult EditSkill(SkillVM s)
    {
        try
        {
            _unitOfService.Skill.UpdateSkill(s);
            _unitOfService.Save();
            List<SkillVM> skills = _unitOfService.Skill.GetAll();
            return PartialView("_Skill", skills);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error updating skill: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpDelete]
    public IActionResult DeleteSkill(long id)
    {
        try
        {
            if (_unitOfService.Skill.IsAlreadyUsed(id))
            {
                return NoContent();
            }
            _unitOfService.Skill.DeleteSkill(id);
            List<SkillVM> skills = _unitOfService.Skill.GetAll();
            return PartialView("_Skill", skills);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error deleting skill: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPut]
    public IActionResult DeactivateSkill(long id, int value)
    {
        try
        {
            _unitOfService.Skill.UpdateStatusByid(id, value);
            _unitOfService.Save();
            List<SkillVM> skills = _unitOfService.Skill.GetAll();
            return PartialView("_Skill", skills);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error updating Skill: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult SearchSkill(string? query)
    {
        try
        {
            List<SkillVM> skills = _unitOfService.Skill.Search(query);
            return PartialView("_Skill", skills);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error searching skill: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    public bool IsSkillUnique(string skillName, long? id)
    {
        if (string.IsNullOrEmpty(skillName)) return false;
        return _unitOfService.Skill.IsSkillUnique(skillName, id);
    }
}
