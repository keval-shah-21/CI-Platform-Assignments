using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.Constants;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class ContactService:IContactService
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public List<ContactVM> GetAll()
    {
        IEnumerable<Contact> obj = _unitOfWork.Contact.GetAll();
        if (obj == null) return new();
        return obj.Select(c => ConvertContactToVM(c)).ToList();
    }

    public static ContactVM ConvertContactToVM(Contact contact)
    {
        return new ContactVM()
        {
            ContactId = contact.ContactId,
            Message = contact.Message,
            Status = (ApprovalStatus)contact.Status!,
            Subject = contact.Subject,
            UserId = contact.UserId,
            User = contact.User != null ? UserService.ConvertUserToVM(contact.User) : null!, 
        };
    }

    public void SaveContact(ContactVM contact)
    {
        _unitOfWork.Contact.Add(new Contact()
        {
            UserId = contact.UserId,
            Message = contact.Message,
            Subject = contact.Subject,
            CreatedAt = DateTimeOffset.Now,
            Status = 0
        });
    }
}
