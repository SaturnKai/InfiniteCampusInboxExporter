using InfiniteCampusInboxExporter.Utils;
using InfiniteCampusInboxExporter.InfiniteCampus.Data;

namespace InfiniteCampusInboxExporter.InfiniteCampus
{
    class Auth
    {
        public static User Login(District district, string username, string password)
        {
            User user = new User();
            string response = HTTP.Post(district.district_baseurl + "verify.jsp", "appName=" + district.district_app_name + "&screen=&username=" + username + "&password=" + password + "&useCSRFProtection=true", ref user.sessionCookies);
            if (response.Contains("password") || response.Contains("error")) return user;
            user.username = username;
            return user;
        }
    }
}