using Clover.DbLayer;
using Clover.Shared;

namespace Clover.Gestion
{
    public static class AppEnvironment
    {
        public static Settings CurrentSettings { get; set; }
        public static User CurrentUser { get; set; }
    }
}
