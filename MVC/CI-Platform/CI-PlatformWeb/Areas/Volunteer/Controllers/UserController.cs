using Microsoft.AspNetCore.Mvc;
using CI_Platform.Services.Service.Interface;
using CI_Platform.Entities.ViewModels;
using CI_PlatformWeb.Areas.Volunteer.Utilities;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
[Route("/volunteer/user/")]
[AuthenticateAdmin]
public class UserController : Controller
{
    private readonly IUnitOfService _unitOfService;
    private readonly IEmailService _emailService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UserController(IUnitOfService unitOfService, IEmailService emailService, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfService = unitOfService;
        _emailService = emailService;
        _webHostEnvironment = webHostEnvironment;
    }

    [Route("login", Name = "Login")]
    public async Task<IActionResult> Login(string? email, string? token, string? returnURL)
    {
        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(token))
        {
            if (_unitOfService.User.VerifyEmail(email, token))
            {
                _unitOfService.User.RemoveVerifyEmail(email);
                await _unitOfService.User.UpdateStatusAsync(email, 1);
                _unitOfService.Save();
            }
        }
        LoginVM login = new LoginVM()
        {
            bannerVMs = _unitOfService.Banner.GetAll().OrderBy(b => b.SortOrder),
            ReturnURL = returnURL,
        };
        return View(login);
    }

    [Route("logout", Name = "Logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("login", Name = "LoginPost")]
    public IActionResult Login(LoginVM loginVM)
    {
        if (ModelState.IsValid)
        {
            AdminVM admin = _unitOfService.User.AdminLogin(loginVM);
            if (admin != null)
            {
                SetUserLoginSession(admin.FirstName, admin.LastName, admin.Avatar, admin.AdminId, admin.Email, true);
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            UserVM userVM = _unitOfService.User.Login(loginVM);

            if (userVM != null)
            {
                if (userVM.Status == false)
                    TempData["Error"] = "Account is not verified yet.";
                else if (userVM.IsBlocked)
                    TempData["Error"] = "Account is blocked by admin.";
                else
                {
                    SetUserLoginSession(userVM.FirstName, userVM.LastName, userVM.Avatar, userVM.UserId, userVM.Email, false);
                    if (loginVM.ReturnURL != null)
                        return LocalRedirect(loginVM.ReturnURL);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["Error"] = "Invalid username or password.";
            }
        }
        loginVM.bannerVMs = _unitOfService.Banner.GetAll().OrderBy(b => b.SortOrder);
        return View(loginVM);
    }

    public void SetUserLoginSession(string firstName, string lastName, string avatar, long userId, string email, bool isAdmin)
    {
        HttpContext.Session.SetString("FirstName", firstName);
        HttpContext.Session.SetString("LastName", lastName);
        HttpContext.Session.SetString("UserId", userId.ToString());
        HttpContext.Session.SetString("Email", email);
        HttpContext.Session.SetString("Avatar", avatar);
        HttpContext.Session.SetString("IsAdmin", isAdmin.ToString());
    }

    [Route("registration", Name = "Registration")]
    public IActionResult Registration()
    {
        UserVM user = new UserVM()
        {
            bannerVMs = _unitOfService.Banner.GetAll().OrderBy(b => b.SortOrder)
        };
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("registration", Name = "RegistrationPost")]
    public async Task<IActionResult> Registration(UserVM userVM)
    {
        if (!ModelState.IsValid)
        {
            userVM.bannerVMs = _unitOfService.Banner.GetAll().OrderBy(b => b.SortOrder);
            return View(userVM);
        }

        var isAdminEmail = _unitOfService.User.IsAdminEmail(userVM.Email);
        if (isAdminEmail)
        {
            TempData["Error"] = "This email is already registered.";
        }
        else if (_unitOfService.User.GetFirstOrDefaultByEmail(userVM.Email) != null)
        {
            TempData["Error"] = "This email is already registered.";
        }
        else
        {
            userVM.Avatar = @"\images\static\default-profile.webp";
            try
            {
                long userId = _unitOfService.User.Add(userVM);

                string token = Guid.NewGuid().ToString();
                var url = Url.Action("Login", "User", new { email = userVM.Email, token = token }, "https");
                _unitOfService.User.SaveVerifyAccountDetails(userVM.Email, token);
                _unitOfService.User.SendVerifyAccountEmail(userVM.Email, url);

                await _unitOfService.NotificationSetting.Add(userId);
                await _unitOfService.UserNotification.AddNotificationCheck(userId);
                await _unitOfService.SaveAsync();

                TempData["Registered"] = "true";
            }
            catch (Exception)
            {
                TempData["Exception"] = "An error occured while registering.";
            }
        }
        userVM.bannerVMs = _unitOfService.Banner.GetAll().OrderBy(b => b.SortOrder);
        return View(userVM);
    }

    [Route("forgot-password", Name = "ForgotPassword")]
    public IActionResult ForgotPassword() => View(_unitOfService.Banner.GetAll().OrderBy(b => b.SortOrder));

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("forgot-password", Name = "ForgotPasswordPost")]
    public IActionResult ForgotPassword(string? email)
    {
        if (email == null) { return RedirectToAction("Error", "Home"); }
        UserVM user = _unitOfService.User.GetFirstOrDefaultByEmail(email);

        if (user == null)
        {
            TempData["Error"] = "You don't have an account with this email address.";
            return RedirectToRoute("ForgotPassword");
        }

        if (user.Status == false)
        {
            TempData["Error"] = "Account is not verified yet.";
        }
        else if (user.IsBlocked)
        {
            TempData["Error"] = "Account is blocked by admin.";
        }
        else
        {
            string token = Guid.NewGuid().ToString();
            var url = Url.Action("ResetPassword", "User", new { email = email, token = token }, "https");

            ResetPasswordVM obj = new ResetPasswordVM() { Email = email, Token = token };
            byte result = _unitOfService.ResetPassword.IsValidRecord(email);
            if (result == 0)
            {
                TempData["Error"] = "Try again after sometime.";
                return RedirectToRoute("ForgotPassword");
            }
            else if (result == 1)
                _unitOfService.ResetPassword.RemoveByEmail(email);

            _unitOfService.ResetPassword.Add(obj);
            _unitOfService.Save();

            _unitOfService.User.SendResetPasswordEmail(email, url!);

            TempData["ForgotPassword"] = "true";
        }
        return RedirectToRoute("ForgotPassword");
    }

    [Route("reset-password", Name = "ResetPassword")]
    public IActionResult ResetPassword(string email, string token)
    {
        if (!_unitOfService.ResetPassword.IsValidRequest(email, token))
        {
            TempData["Error"] = "Invalid Token or Token Expired.";
            return RedirectToAction("ForgotPassword");
        }
        return View(
            new ResetPasswordDataVM()
            {
                Email = email,
                Token = token,
                bannerVMs = _unitOfService.Banner.GetAll().OrderBy(b => b.SortOrder)
            }
        );
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("reset-password", Name = "ResetPasswordPost")]
    public IActionResult ResetPassword(ResetPasswordDataVM resetPasswordDataVM)
    {
        if (ModelState.IsValid)
        {
            _unitOfService.ResetPassword.Remove(resetPasswordDataVM);
            _unitOfService.User.UpdatePassword(resetPasswordDataVM.Email, resetPasswordDataVM.Password);

            _unitOfService.ResetPassword.RemoveByEmail(resetPasswordDataVM.Email);
            _unitOfService.Save();

            TempData["ResetPassword"] = "true";
        }
        resetPasswordDataVM.bannerVMs = _unitOfService.Banner.GetAll().OrderBy(b => b.SortOrder);
        return View(resetPasswordDataVM);
    }

    [Route("get-users-to-recommend")]
    public IActionResult GetAllUsersToRecommend(long? missionId, long? storyId, long userId)
    {
        List<UserVM> users = new();
        if (missionId != null)
        {
            users = _unitOfService.User.GetAllUsersToRecommendMission();
        }
        else
        {
            users = _unitOfService.User.GetAllUsersToRecommendStory();
        }
        ViewBag.UserId = userId;
        ViewBag.MissionId = missionId;
        ViewBag.StoryId = storyId;
        return PartialView("_RecommendToCoWorker", users?.Where(u => u.UserId != userId).ToList());
    }

    [Route("user-profile")]
    [Authentication]
    public IActionResult UserProfile(long userId)
    {
        if (userId != long.Parse(HttpContext.Session.GetString("UserId")))
        {
            return RedirectToAction("Error", "Home", new { area = "Volunteer" });
        }
        try
        {
            ProfileVM user = _unitOfService.User.GetUserProfileById(userId);
            return View(user);
        }
        catch (Exception)
        {
            return RedirectToAction("Error", "Home", new { area = "Volunteer" });
        }
    }

    [Route("user-profile")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UserProfile(ProfileVM profileVM, string preloadedImage, IFormFile profileInput, List<short> preloadedSkills, List<short> skillIds)
    {
        ModelState.Remove("profileInput");
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (profileInput != null)
            {
                _unitOfService.User.RemoveProfileImage(wwwRootPath, preloadedImage);
                profileVM.Avatar = _unitOfService.User.SaveProfileImage(wwwRootPath, profileInput);
            }
            _unitOfService.User.UpdateUserProfile(profileVM);
            if ((preloadedSkills.Count() > 0 && skillIds.Count() == 0) || skillIds.Count() > 0)
            {
                _unitOfService.UserSkill.RemoveAllUserSkills(profileVM.UserId);
            }
            if (skillIds.Count() > 0)
            {
                _unitOfService.UserSkill.SaveAllUserSkills(skillIds, profileVM.UserId);
            }
            _unitOfService.Save();
            SetUserLoginSession(profileVM.FirstName, profileVM.LastName, profileVM.Avatar, profileVM.UserId, profileVM.Email, false);
            TempData["ProfileSuccess"] = "true";
            return RedirectToAction("Index", "Home");
        }

        return View(profileVM);
    }

    [HttpPut]
    [Route("update-password")]
    public IActionResult UpdateProfilePassword(string email, string oldPassword, string newPassword)
    {
        bool isValid = _unitOfService.User.IsPasswordValid(email, oldPassword);
        if (!isValid) return NoContent();
        _unitOfService.User.UpdatePassword(email, newPassword);
        return StatusCode(200);
    }

    [Route("contact-partial")]
    public IActionResult GetContactPartial() => PartialView("_Contact");

    [HttpPost]
    [Route("contact-admin")]
    public IActionResult ContactAdmin(long UserId, string Subject, string Message)
    {
        _unitOfService.Contact.SaveContact(new ContactVM()
        {
            UserId = UserId,
            Subject = Subject,
            Message = Message
        });
        _unitOfService.Save();
        return NoContent();
    }

    [Route("is-profile-filled")]
    public bool IsProfileFilled(long userId) => _unitOfService.User.IsProfileFilled(userId);
}
