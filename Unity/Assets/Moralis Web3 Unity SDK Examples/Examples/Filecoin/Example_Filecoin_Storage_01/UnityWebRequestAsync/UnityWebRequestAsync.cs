using System;
using System.Threading.Tasks;
using MoralisUnity.Sdk.Exceptions;
using UnityEngine;
using UnityEngine.Networking;

namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
    
    /// <summary>
    /// Wrapper for <see cref="UnityWebRequest"/> inspired by https://github.com/crevelop/unitywebrequest-tutorial.
    ///
    /// EXAMPLE USAGE:
    /// 
    /// 			var url = "https://jsonplaceholder.typicode.com/todos/1";
    ///             var httpClient = new UnityWebRequestAsync(new JsonSerializationOption());
    ///             var result = await httpClient.Get<User>(url);
    ///
    /// 
    /// </summary>
    public class UnityWebRequestAsync
    {
        //  Properties ------------------------------------
        //public string SamplePublicText { get { return _samplePublicText; } set { _samplePublicText = value; }}
        
        //  Fields ----------------------------------------
        private readonly ISerializationOption _serializationOption;

        
        //  Initialization Methods-------------------------
        public UnityWebRequestAsync(ISerializationOption serializationOption)
        {
            _serializationOption = serializationOption;
        }

        //  General Methods -------------------------------
        public async Task<T> Get<T>(string token, string url)
        {
            T result = default(T);
            
            try
            {
                using var unityWebRequest = UnityWebRequest.Get(url);
                unityWebRequest.SetRequestHeader("Authorization", $"Bearer {token}");

                unityWebRequest.SetRequestHeader("Content-Type", _serializationOption.ContentType);
                Debug.Log($"Get() url = {url}");
                Debug.Log($"Get() Content-Type = {_serializationOption.ContentType}");

                var operation = unityWebRequest.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();  
                }

                if (unityWebRequest.result != UnityWebRequest.Result.Success)
                {
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
            
            return result;
        }
        
        public async Task<T> Post<T>(string token, string url, string data)
        {
            try
            {
                using var www = UnityWebRequest.Post(url, data);
                www.SetRequestHeader("Authorization", $"Bearer {token}");
                var operation = www.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();  
                }

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Failed: {www.error}");
                }
                
                var result = _serializationOption.Deserialize<T>(www.downloadHandler.text);
                return result;

                
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(Post)} failed: {ex.Message}");
                return default;
            }
        }
        
        //  Event Handlers --------------------------------
    }
}