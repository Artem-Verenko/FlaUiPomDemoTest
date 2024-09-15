using Newtonsoft.Json;

namespace FlaUiPomDemoTest.Utilities
{
    public static class SearchInJsons
    {

        // search in list of json files
        public static string SearchInJsonFiles(string searchKey, string[] jsonFiles)
        {
            string result = null;
            foreach (var jsonFile in jsonFiles)
            {
                result = SearchInJsonFile(searchKey, jsonFile);
                if (result != null)
                {
                    break;
                }
            }
            return result;
        }

        private static string SearchInJsonFile(string searchKey, string jsonFile)
        {
            // read the json file
            string json = System.IO.File.ReadAllText(jsonFile);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);

            // search for the key
            foreach (var item in jsonObj)
            {
                if (item.Key == searchKey)
                {
                    return item.Value;
                }
            }

            return null;
        }
    }
}
