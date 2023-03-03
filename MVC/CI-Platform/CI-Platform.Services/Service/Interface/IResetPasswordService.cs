using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IResetPasswordService
{
    void Add(ResetPasswordVM resetPasswordVM);

    bool IsValidRequest(string email, string token);

    byte IsValidRecord(string email);

    void Remove(ResetPasswordVM resetPasswordVM);

    void RemoveByEmail(string email);

    void Update(ResetPasswordVM resetPasswordVM);

    ResetPasswordVM GetFirstOrDefaultByEmail(string email);
}