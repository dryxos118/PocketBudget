using System.ComponentModel;

namespace PocketApi.Models
{
    public enum PocketRole
    {
        [Description("PocketAdmin")] PocketAdmin = 0,
        [Description("PocketModerator")] PocketModerator = 1,
        [Description("PocketUser")] PocketUser = 2,
    }
}