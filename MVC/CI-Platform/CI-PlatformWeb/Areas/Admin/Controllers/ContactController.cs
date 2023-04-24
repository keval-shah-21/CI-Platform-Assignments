using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class ContactController : Controller
{
    private readonly IUnitOfService _unitOfService;

    public ContactController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }
    public IActionResult LoadContactPage()
    {
        try
        {
            List<ContactVM> contacts = _unitOfService.Contact.GetAll();
            contacts = contacts.OrderBy(c => c.Status).ToList();
            return PartialView("_Contact", contacts);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading contact: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    public IActionResult ViewMessage(long id) => PartialView("_ViewContact", _unitOfService.Contact.GetContactById(id));
    public IActionResult ReplyMessage(long id) => PartialView("_ReplyContact", _unitOfService.Contact.GetContactById(id));
    
    [HttpPut]
    public async Task<IActionResult> ReplyMessage(ContactVM contact)
    {
        try
        {
            await _unitOfService.Contact.SendReplyEmail(contact);
            await _unitOfService.Contact.ReplyContact(contact);
            _unitOfService.Save();
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loadin reply contact: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
    [HttpDelete]
    public IActionResult DeleteMessage(long id)
    {
        try
        {
            _unitOfService.Contact.RemoveContactById(id);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error deleting contact: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    public IActionResult SearchContact(string? query)
    {
        try
        {
            List<ContactVM> contacts = _unitOfService.Contact.SearchContact(query);
            contacts = contacts.OrderBy(c => c.Status).ToList();
            return PartialView("_Contact", contacts);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error searchin contact: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}