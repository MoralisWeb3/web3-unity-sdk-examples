using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{

    /// <summary>
    /// Wrapper for <see cref="UnityWebRequest"/> to be async.
    /// 
    /// See <see cref="https://github.com/crevelop/unitywebrequest-tutorial"/>
    /// 
    /// </summary>
    public class FilecoinRequestAsync
    {
        //  Properties ------------------------------------


        //  Fields ----------------------------------------
        private readonly string _token;
        private const string _baseUrl = "https://api.web3.storage";


        //  Initialization Methods-------------------------
        public FilecoinRequestAsync(string token)
        {
            _token = token;
        }

        //  General Methods (With Auth) ---------------------
        public async Task<UploadResponse> UploadFile(byte[] bytes)
        {
            string url = _baseUrl + $"/upload";
            UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new JsonSerializationOption());
            DownloadHandlerData downloadHandlerData = await unityWebRequestAsync.Post(_token, url, bytes);
            return JsonConvert.DeserializeObject<UploadResponse>(downloadHandlerData.text);
        }



        //  General Methods (No Auth) ---------------------

        public async Task<StatusResponse> GetStatus(string cid)
        {
            string url = _baseUrl + $"/status/{cid}";
            UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new JsonSerializationOption());
            DownloadHandlerData downloadHandlerData = await unityWebRequestAsync.Get(url);
            return JsonConvert.DeserializeObject<StatusResponse>(downloadHandlerData.text);
        }

        public async Task<GetFileResponse> GetFile(string cid)
        {
            //e.g. https://bafybeiaqsybxdb5sxitsofxk5ek7bt7nrigp52bjeakdo6x65x5h3i7aye.ipfs.w3s.link
            string url = $"https://{cid}.ipfs.w3s.link/";
            UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new JsonSerializationOption());
            DownloadHandlerData downloadHandlerData = await unityWebRequestAsync.Get(url);
            return new GetFileResponse
            {
                data = downloadHandlerData.data
            };
        }

        //  Event Handlers --------------------------------

    }
}