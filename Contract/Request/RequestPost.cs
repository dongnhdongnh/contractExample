using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Contract.Request
{
    public class ResultAbi
    {
        public string status
        {
            get;
            set;
        }

        public string message
        {
            get;
            set;
        }

        public string data
        {
            get;
            set;
        }
    }

    public class RequestPost
    {
        public static async Task<string> Upload(string actionUrl, string contractName)
        {
            var fileName = "C:\\Users\\Fcode\\Documents\\MyDaico\\Contract\\Contract\\SmartContracts\\" + contractName + ".sol";
            HttpContent fileStreamContent = new ByteArrayContent(File.ReadAllBytes(fileName));
            using (HttpClient client = new HttpClient())
            using (MultipartFormDataContent formData = new MultipartFormDataContent())
            {
                //fileStreamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                //{
                //    FileName = fileName
                //};

                formData.Add(fileStreamContent, "file", "SimpleTest");

                var response = await client.PostAsync(actionUrl, formData);

                response.EnsureSuccessStatusCode();

                var contentString = await response.Content.ReadAsStringAsync();
                var contents = JObject.Parse(contentString);
                ResultAbi result = JsonConvert.DeserializeObject<ResultAbi>(contentString);

                client.Dispose();

                if (result.status == "success")
                {
                    return result.data;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
