using System.Threading.Tasks;
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

        
        //  General Methods (No Auth) ---------------------
        public async Task<CarResponse> GetCarOld(string cid)
        {
            string url = _baseUrl + $"/car/{cid}";
            UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new IpfsDagSerializationOption());
            
            byte[] bytes = await unityWebRequestAsync.Get<byte[]>(_token, url);
            CarResponse carResponse = new CarResponse();
            carResponse.bytes = bytes;
            return carResponse;
        }
        
        public async Task<T> GetCar<T>(string cid)
        {
            string url = $"https://{cid}.ipfs.w3s.link";
            url =
                "https://bafybeidd2gyhagleh47qeg77xqndy2qy3yzn4vkxmk775bg2t5lpuy7pcu.ipfs.w3s.link/not-distributed.jpg";
            url =
                "https://static01.nyt.com/images/2022/08/16/arts/15emmy-walken/15emmy-walken-threeByTwoMediumAt2X.jpg";
            UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new ImageSerializationOption());
            
            return await unityWebRequestAsync.Get<T>(_token, url);;
        }
        
        
        public async Task<StatusResponse> GetStatus(string cid)
        {
            string url = _baseUrl + $"/status/{cid}";
            UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new JsonSerializationOption());
            return await unityWebRequestAsync.Get<StatusResponse>(_token, url);
        }
        
        
        //  General Methods (With Auth) ---------------------
        public async Task<UploadResponse> Upload(string data)
        {
            string url = _baseUrl + $"/upload";
            UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new JsonSerializationOption());
            return await unityWebRequestAsync.Post<UploadResponse>(_token, url, data);
        }
        
        public async Task<UploadsResponse> GetUploads()
        {
            string url = _baseUrl + $"/user/uploads";
            UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new JsonSerializationOption());
            return await unityWebRequestAsync.Get<UploadsResponse>(_token, url);
        }
        

        
        //  Event Handlers --------------------------------
    }
}