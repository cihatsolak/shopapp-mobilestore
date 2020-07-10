using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace ShopApp.Web.Framework.Extensions
{
    public static class TempDataExtension
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object _object;

            tempData.TryGetValue(key, out _object);

            return _object == null ? null : JsonConvert.DeserializeObject<T>((string)_object);
        }
    }
}
