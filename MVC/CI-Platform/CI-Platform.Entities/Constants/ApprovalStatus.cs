using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Entities.Constants
{
    public enum ApprovalStatus
    {
        [Display(Name = "Pending")]
        PENDING,
        [Display(Name = "Approved")]
        APPROVED,
        [Display(Name = "Declined")]
        DECLINED,
        DRAFT
    }
}