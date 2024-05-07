using Newtonsoft.Json;
using PracticeReactApp.Core.Constants;
using PracticeReactApp.Core.Data.Entities;

namespace PracticeReactApp.Server.ViewModels.RoleManagement
{
    public class MaintenanceRoleViewModel : Role
    {
        [JsonProperty("id")]
        public override string Id { get => base.Id; set => base.Id = value; }

        [JsonProperty("name")]
        public override string? Name { get => base.Name; set => base.Name = value; }

        [JsonProperty("normalizedName")]
        public override string? NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; }

        [JsonProperty("mode")]
        public ActionMode Mode { get; set; }
    }
}