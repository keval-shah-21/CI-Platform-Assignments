using Microsoft.AspNetCore.Mvc;
using CI_Platform.Services.Service.Interface;
using CI_Platform.Entities.ViewModels;
using CI_PlatformWeb.Areas.Volunteer.Utilities;

namespace CI_PlatformWeb.Areas.Volunteer.Controllers;

[Area("Volunteer")]
[Route("/volunteer/user/")]
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
    public IActionResult Login()
    {
        return View();
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
            UserVM userVM = _unitOfService.User.Login(loginVM);
            if (userVM != null)
            {
                SetUserLoginSession(userVM.FirstName, userVM.LastName, userVM.Avatar, userVM.UserId, userVM.Email);
                return RedirectToAction("Index", "Home");
            }
            TempData["Error"] = "Invalid username or password.";
        }
        return View(loginVM);
    }

    public void SetUserLoginSession(string firstName, string lastName, string avatar, long userId, string email)
    {
        HttpContext.Session.SetString("FirstName", firstName);
        HttpContext.Session.SetString("LastName", lastName);
        HttpContext.Session.SetString("UserId", userId.ToString());
        HttpContext.Session.SetString("Email", email);
        HttpContext.Session.SetString("Avatar", avatar);
    }

    [Route("registration", Name = "Registration")]
    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("registration", Name = "RegistrationPost")]
    public IActionResult Registration(UserVM userVM)
    {
        if (ModelState.IsValid)
        {
            if (_unitOfService.User.GetFirstOrDefaultByEmail(userVM.Email) == null)
            {
                userVM.Avatar = @"\images\static\default-profile.webp";
                _unitOfService.User.Add(userVM);
                _unitOfService.Save();
                SetUserLoginSession(userVM.FirstName, userVM.LastName, userVM.Avatar, userVM.UserId, userVM.Email);
                return RedirectToAction("Index", "Home");
            }
            TempData["Error"] = "This email is already registered.";
        }
        return View(userVM);
    }

    [Route("forgot-password", Name = "ForgotPassword")]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("forgot-password", Name = "ForgotPasswordPost")]
    public IActionResult ForgotPassword(string? email)
    {
        if (email == null) { return RedirectToAction("Error", "Home"); }

        if (_unitOfService.User.GetFirstOrDefaultByEmail(email) != null)
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
            TempData["Success"] = "Mail is sent on your email address";
            return RedirectToRoute("Login");
        }
        else
            TempData["Error"] = "You don't have an account with this email address.";
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
                Token = token
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

            TempData["Success"] = "Password updated successfully.";
            return RedirectToRoute("Login");
        }
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
        if (userId == 0) return NotFound();
        ProfileVM user = _unitOfService.User.GetUserProfileById(userId);
        if (user == null) return NotFound();
        return View(user);
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
                string oldPath = Path.Combine(wwwRootPath, preloadedImage.TrimStart('\\'));
                bool isDefault = preloadedImage.Split("\\").Last().Split(".")[0].Equals("default-profile");
                if (System.IO.File.Exists(oldPath) && !isDefault)
                {
                    System.IO.File.Delete(oldPath);
                }

                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\user");
                string extension = Path.GetExtension(profileInput.FileName);
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    profileInput.CopyTo(fileStreams);
                }
                profileVM.Avatar = @"\images\user\" + fileName + extension;
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
            SetUserLoginSession(profileVM.FirstName, profileVM.LastName, profileVM.Avatar, profileVM.UserId, profileVM.Email);
            return RedirectToAction("Index", "Home", new { profileSuccess = "true" });
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
        return Ok(200);
    }

    [Route("contact-partial")]
    public IActionResult GetContactPartial()
    {
        return PartialView("_Contact");
    }

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
}
