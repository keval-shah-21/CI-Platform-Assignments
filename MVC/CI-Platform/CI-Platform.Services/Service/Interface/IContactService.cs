using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IContactService
{
    void SaveContact(ContactVM contact);
    List<ContactVM> GetAll();
    ContactVM GetContactById(long id);
    void RemoveContactById(long id);
    Task ReplyContact(ContactVM contactVM);
    Task SendReplyEmail(ContactVM contactVM);
    List<ContactVM> SearchContact(string? query);
}
