using System;
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
            var authFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "AuthRequest.json");

            using (var streamWriter = new StreamWriter(authFile))
            {
                var content = JsonConvert.SerializeObject(request);
                await streamWriter.WriteLineAsync(content);
            }
        }

        public static async Task<AuthenticationRequest> LoadAuthData()
        {
            var authFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "AuthRequest.json");
            var authInfo = new FileInfo(authFile);

            if (authInfo.Exists)
                if (authInfo.Length > 0)
                {
                    var streamReader = new StreamReader(authFile);
                    var tempContent = await streamReader.ReadToEndAsync();
                    streamReader.Close();
                    var request = JsonConvert.DeserializeObject<AuthenticationRequest>(tempContent);
                    return request;
                }

            return null;
        }
    }
}