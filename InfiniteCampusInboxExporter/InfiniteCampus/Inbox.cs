using InfiniteCampusInboxExporter.Utils;
using InfiniteCampusInboxExporter.InfiniteCampus.Data;

namespace InfiniteCampusInboxExporter.InfiniteCampus
{
    class Inbox
    {
        public static string GetInbox(District district, User user)
        {
            return HTTP.Get(district.district_baseurl + "api/portal/process-message", ref user.sessionCookies);
        }
    }
}