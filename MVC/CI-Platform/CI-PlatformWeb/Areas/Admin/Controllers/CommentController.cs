using CI_Platform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CI_PlatformWeb.Areas.Admin.Controllers;
public class CommentController : Controller
{
    private readonly IUnitOfService _unitOfService;
    public CommentController(IUnitOfService unitOfService)
    {
        _unitOfService = unitOfService;
    }
}
