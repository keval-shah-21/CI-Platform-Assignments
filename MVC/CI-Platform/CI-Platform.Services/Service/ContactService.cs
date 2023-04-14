using CI_Platform.DataAccess.Repository.Interface;
using CI_Platform.Entities.DataModels;
using CI_Platform.Entities.ViewModels;
using CI_Platform.Services.Service.Interface;

namespace CI_Platform.Services.Service;

public class ContactService : IContactService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;

    public ContactService(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }
    public List<ContactVM> GetAll()
    {
        IEnumerable<Contact> obj = _unitOfWork.Contact.GetAllWithInclude();
        if (obj == null) return new();
        return obj.Select(c => ConvertContactToVM(c)).ToList();
    }
    public ContactVM GetContactById(long id)
    {
        return ConvertContactToVM(_unitOfWork.Contact.GetFirstOrDefaultWithInclude(c => c.ContactId == id));
    }
    public static ContactVM ConvertContactToVM(Contact contact)
    {
        return new ContactVM()
        {
            ContactId = contact.ContactId,
            Message = contact.Message,
            Status = contact.Status,
            Subject = contact.Subject,
            UserId = contact.UserId,
            Reply = contact.Reply,
            CreatedAt = contact.CreatedAt,
            UpdatedAt = contact.UpdatedAt,
            User = contact.User != null ? ConvertUserToVM(contact.User) : null!,
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
            Status = false,
        });
    }

    public void RemoveContactById(long id)
    {
        _unitOfWork.Contact.RemoveById(id);
    }

    public void ReplyContact(ContactVM contactVM)
    {
        Contact contact = _unitOfWork.Contact.GetFirstOrDefault(c => c.ContactId == contactVM.ContactId);
        contact.Reply = contactVM.Reply;
        contact.Status = true;
        contact.UpdatedAt = DateTimeOffset.Now;
    }
    public void SendReplyEmail(ContactVM contactVM)
    {
        string subject = "CI Platform - Contact US";
        string body = $"<div style='font-size:1rem'><p>Thank you for reaching us out through Contact Us. We appreciate your feedback/inquiry and apologize for any inconvenience caused.</p><p>{contactVM.Reply}</p><p style='margin-top:0.5rem;'>Best Regards,</p><p>CSR Team</p></div>";
        _emailService.SendEmail(contactVM.User.Email, subject, body);
    }
    public List<ContactVM> SearchContact(string? query)
    {
        IEnumerable<Contact> contact= _unitOfWork.Contact.GetAllWithInclude();
        return string.IsNullOrEmpty(query) ? contact.Select(u => ConvertContactToVM(u)).ToList()
            : contact
                .Where(u => u.User.FirstName.ToLower().Contains(query.ToLower()) || u.User.LastName.ToLower().Contains(query.ToLower()) || u.Subject.ToLower().Contains(query.ToLower()))
                .Select(u => ConvertContactToVM(u))
                .ToList();
    }
    private static UserVM ConvertUserToVM(User user)
    {
        return new UserVM()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }
}
