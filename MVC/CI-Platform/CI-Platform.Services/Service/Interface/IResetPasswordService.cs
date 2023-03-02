using CI_Platform.Entities.ViewModels;

namespace CI_Platform.Services.Service.Interface;

public interface IResetPasswordService
{
    void Add(ResetPasswordVM resetPasswordVM);

    bool IsValidRecord(string email);

    void Remove(ResetPasswordDataVM resetPasswordDataVM);

    void Update(ResetPasswordVM resetPasswordVM);

    ResetPasswordVM GetFirstOrDefaultByEmail(string email);
}