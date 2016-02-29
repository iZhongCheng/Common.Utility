namespace Common.Utility
{
    /// <summary>
    /// Author：Kt
    /// Date Created：2011-04-01
    /// Description：坐标地址Api-工具类
    /// </summary>
    public class LocationHelper
    {
        /// <summary>
        /// 根据经纬度得到地址
        /// </summary>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <returns></returns>
        public static string GetLocation(string longitude, string latitude)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(longitude) || string.IsNullOrWhiteSpace(latitude))
                    return string.Empty;
                return HttpHelper.HttpGet(string.Format("http://api.map.baidu.com/geocoder?location={0},{1}&coord_type=gcj02&output=json", latitude, longitude)).Replace("\n", string.Empty).Replace("\r", string.Empty);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 根据ip地址得到地址信息
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>Json 字符串</returns>
        public static string GetLocation(string ip)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ip))
                    return string.Empty;
                return HttpHelper.HttpGet(string.Format("http://api.map.baidu.com/location/ip?ak=CDa36173b7623105124eaf21e6c07569&coor=bd09ll&ip={0}", ip)).Replace("\n", string.Empty).Replace("\r", string.Empty);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 根据地址得到经纬度
        /// </summary>
        /// <param name="address">地址</param>
        /// <returns>Json 字符串</returns>
        public static string GetLongitudeLatitudeFromBaiDuMap(string address)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(address))
                    return string.Empty;
                return HttpHelper.HttpGet(string.Format("http://api.map.baidu.com/geocoder?output=json&address={0}", address)).Replace("\n", string.Empty).Replace("\r", string.Empty);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}