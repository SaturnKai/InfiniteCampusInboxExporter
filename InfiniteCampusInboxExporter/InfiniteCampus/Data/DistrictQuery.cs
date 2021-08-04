using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;

using InfiniteCampusInboxExporter.Utils;

namespace InfiniteCampusInboxExporter.InfiniteCampus.Data
{
    class DistrictQuery
    {
        public List<District> data { get; set; }

        public static DistrictQuery QueryDistricts(string stateCode, string districtName)
        {
            DistrictQuery districtQuery = new DistrictQuery();
            CookieContainer tempCookies = new CookieContainer();
            string districtResponse = HTTP.Get("https://mobile.infinitecampus.com/mobile/searchDistrict?query=" + districtName + "&state=" + stateCode, ref tempCookies);
            if (districtResponse.Contains("error")) return districtQuery;
            districtQuery = JsonConvert.DeserializeObject<DistrictQuery>(districtResponse);
            return districtQuery;
        }
    }
}