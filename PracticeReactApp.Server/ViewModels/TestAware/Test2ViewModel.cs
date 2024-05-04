using System.ComponentModel.DataAnnotations;

namespace PracticeReactApp.Server.ViewModels.TestAware
{
    public class Test2ViewModel
    {
        [StringLength(197, ErrorMessage = $"The length of {nameof(P1)} cannot exceed 99 items.")]//max length check on value split from ','. (99 + 98)
        public string? P1 { get; set; }
    }
}
