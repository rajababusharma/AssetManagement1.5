using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace AssetManagement.APIServices
{
    internal class JsonDataService
    {
        public void SaveJsonData(object data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            Preferences.Set("jsonDataKey", jsonData); // Save data to Preferences
        }

        public T LoadJsonData<T>()
        {
            string jsonData = Preferences.Get("jsonDataKey", string.Empty); // Get data from Preferences
            if (!string.IsNullOrEmpty(jsonData))
            {
                return JsonConvert.DeserializeObject<T>(jsonData); // Deserialize the JSON data
            }

            return default(T); // Return default value if no data found
        }
    }
}
