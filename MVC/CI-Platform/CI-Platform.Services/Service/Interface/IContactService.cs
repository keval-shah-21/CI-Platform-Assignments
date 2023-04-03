using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IContactService
{
    void SaveContact(ContactVM contact);
}
