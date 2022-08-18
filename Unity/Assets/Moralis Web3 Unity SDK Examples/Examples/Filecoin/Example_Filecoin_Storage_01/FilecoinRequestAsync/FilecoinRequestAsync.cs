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
        public async Task<UploadResponse> Upload(string filename, byte[] bytes)
        {
            string url = _baseUrl + $"/upload";
            UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new JsonSerializationOption());
            DownloadHandlerData downloadHandlerData = await unityWebRequestAsync.Post(_token, url, filename, bytes);
            return JsonConvert.DeserializeObject<UploadResponse>(downloadHandlerData.text);
        }
        

        
        //  General Methods (No Auth) ---------------------
        
        public async Task<StatusResponse> GetStatus(string cid)
        {
            string url = _baseUrl + $"/status/{cid}";
            UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new JsonSerializationOption());
            DownloadHandlerData downloadHandlerData = await unityWebRequestAsync.Get(_token, url);
            return JsonConvert.DeserializeObject<StatusResponse>(downloadHandlerData.text);
        }

        public async Task<UploadsResponse> GetUploads()
        {
            string url = _baseUrl + $"/user/uploads";
            UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new JsonSerializationOption());
            DownloadHandlerData downloadHandlerData =  await unityWebRequestAsync.Get(_token, url);
            return JsonConvert.DeserializeObject<UploadsResponse>(downloadHandlerData.text);
        }

        public async Task<CarResponse> GetFile(string cid)
        {
            //e.g. https://bafybeiaqsybxdb5sxitsofxk5ek7bt7nrigp52bjeakdo6x65x5h3i7aye.ipfs.w3s.link
            string url =  $"https://{cid}.ipfs.w3s.link/";
            UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new ImageSerializationOption());
            DownloadHandlerData downloadHandlerData = await unityWebRequestAsync.Get(_token, url);
            return new CarResponse
            {
                data = downloadHandlerData.data
            };
        }
        
        // public async Task<CarResponse> GetCarOld(string cid)
        // {
        //     string url = _baseUrl + $"/car/{cid}";
        //     UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new IpfsDagSerializationOption());
        //     
        //     byte[] bytes = await unityWebRequestAsync.Get<byte[]>(_token, url);
        //     CarResponse carResponse = new CarResponse();
        //     carResponse.bytes = bytes;
        //     return carResponse;
        // }
        
        // public async Task<CarResponse> GetCarRaw(string cid)
        // {
        //     string url = _baseUrl + $"/car/{cid}";
        //     //url = $"https://{cid}.ipfs.w3s.link";
        //     //url = "https://bafybeidd2gyhagleh47qeg77xqndy2qy3yzn4vkxmk775bg2t5lpuy7pcu.ipfs.w3s.link/not-distributed.jpg"; //random link from online
        //     //url = "https://static01.nyt.com/images/2022/08/16/arts/15emmy-walken/15emmy-walken-threeByTwoMediumAt2X.jpg"; //random link from online
        //    // url = "https://ipfs.io/ipfs/QmUanajiSMEbxWA7dtPu2heExSxNdJ35H33iGD1TJz5NeH"; //random link from online
        //    
        //     UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new ImageSerializationOption());
        //     DownloadHandlerData downloadHandlerData = await unityWebRequestAsync.Get(_token, url);
        //     
        //     Debug.Log("Return is : " + downloadHandlerData.data);
        //     return new CarResponse
        //     {
        //         data = downloadHandlerData.data
        //     };
        // }
        
        // public async Task<CarResponse> GetCarFromGateway(string cid)
        // {
        //     //e.g. https://bafybeiaqsybxdb5sxitsofxk5ek7bt7nrigp52bjeakdo6x65x5h3i7aye.ipfs.w3s.link
        //     string url =  $"https://{cid}.ipfs.w3s.link";
        //     UnityWebRequestAsync unityWebRequestAsync = new UnityWebRequestAsync(new ImageSerializationOption());
        //     DownloadHandlerData downloadHandlerData = await unityWebRequestAsync.Get(_token, url);
        //     
        //     Debug.Log("Return is : " + downloadHandlerData.data);
        //     return new CarResponse
        //     {
        //         data = downloadHandlerData.data
        //     };
        // }
        //

        //  Event Handlers --------------------------------
        
    }
}

/*

                if (unityWebRequest.downloadHandler.text != null)
                    {
                        ErrorResponse errorResponse = _serializationOption.Deserialize<ErrorResponse>(unityWebRequest.downloadHandler.text);
                        Debug.Log("Get() Failed errorResponse = " + errorResponse);
                        return default(T);
                    }
                    
                    Debug.LogError($"Failed error = {unityWebRequest.error}");
                }

                Debug.LogWarning($"////// {_serializationOption.ReturnType} ////// ");

                
                
                switch (_serializationOption.ReturnType)
                {
                    case ReturnType.RequiresDeserialization:
                        result = _serializationOption.Deserialize<T>(unityWebRequest.downloadHandler.text);
                        break;
                    case ReturnType.Text:
                        Debug.Log("text : " + unityWebRequest.downloadHandler.text);
                        result = default(T);
                        break;
                    case ReturnType.Image:
                        Debug.Log("1");
                        // Get downloaded asset bundle
                        var texture = DownloadHandlerTexture.GetContent(unityWebRequest);
                        Debug.Log("2");
                        Debug.Log("texture: " + texture.dimension);
                        Debug.Log("3");
                        break;
                      
                    case ReturnType.Binary:
                        
                        byte[] results = unityWebRequest.downloadHandler.data;
                        if (results != null)
                        {
                            result = (T)(object)results;
                        }
                        
                        break;
                    default:
                        throw new SwitchDefaultException(_serializationOption.ReturnType);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(Get)} failed: {ex.Message}");
                return default;
            }
            */