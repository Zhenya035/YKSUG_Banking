using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YKSUG_Banking.scripts.entity.Request;

namespace YKSUG_Banking.scripts.servises
{
    internal class AuthRequestHandler
    {
        public static async Task SaveAuthData(AuthenticationRequest request)
        {
            //gets path to file
            string authFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "AuthRequest.json");

            using (StreamWriter streamWriter = new StreamWriter(authFile))
            {
                string content = JsonConvert.SerializeObject(request);
                await streamWriter.WriteLineAsync(content);
            }
        }

        public static async Task<AuthenticationRequest> LoadAuthData()
        {
            string authFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "AuthRequest.json");
            FileInfo authInfo = new FileInfo(authFile);

            if (authInfo.Exists)
            {
                if (authInfo.Length > 0)
                {
                    StreamReader streamReader = new StreamReader(authFile);
                    string tempContent = await streamReader.ReadToEndAsync();
                    streamReader.Close();
                    AuthenticationRequest request = JsonConvert.DeserializeObject<AuthenticationRequest>(tempContent);
                    return request;
                }

            }
            return null;
        }
    }
}