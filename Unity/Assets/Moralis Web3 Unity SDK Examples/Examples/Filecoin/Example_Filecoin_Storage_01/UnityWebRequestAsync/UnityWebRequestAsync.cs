using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{

    public class DownloadHandlerData
    {
        public byte[] data;
        public string text;

        public DownloadHandlerData(DownloadHandler downloadHandler)
        {
            data = downloadHandler.data;
            text = downloadHandler.text;
        }
    }
    
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

        public async Task<DownloadHandlerData> Get(string url)
        {
            try
            {
                using var unityWebRequest = UnityWebRequest.Get(url);
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
                    Debug.LogError($"Failed: {unityWebRequest.error}");
                }
                else
                {
                    string responseContentType = unityWebRequest.GetResponseHeader("Content-Type");
                    Debug.Log($"responseContentType = {responseContentType}");
                    return new DownloadHandlerData(unityWebRequest.downloadHandler);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(Get)} failed: {ex.Message}");
                
            }

            return null;

        }
        
        public async Task<DownloadHandlerData> Post(string token, string url, byte[] bytes)
        {
            try
            {
          
    
                
                Debug.Log($" >>>>> token {token}");
                Debug.Log($" >>>>> url {url}");
                Debug.Log($" >>>>> bytes {bytes.Length}");
                
                //FROM DOCUMENTATION
                // curl -X 'POST' \
                // 'https://api.web3.storage/upload' \
                // -H 'accept: application/json' \
                // -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkaWQ6ZXRocjoweEY0MjU3Q2IyZDMyOWEzRWIzMTM1MzI1YzgyYzAzNkFlYWMwMkE3NDgiLCJpc3MiOiJ3ZWIzLXN0b3JhZ2UiLCJpYXQiOjE2NjA2NzQxNjI0MTMsIm5hbWUiOiJ3ZWIzLXVuaXR5LXNkay1leGFtcGxlcyJ9.FAtQ2W7HxzLAG68U1clOE5CpjaWYbYvrnlTmeVm53as' \
                // -H 'Content-Type: multipart/form-data' \
                // -F 'file=@art_watercolor.jpg;type=image/jpeg' \
                // -F 'file=@photo.png;type=image/png'
                
                //OFF-THE-SUBJECT LINKS
                //formData.Add(new MultipartFormFileSection("file", bytes, filename, "application/octet-stream"));

                List<IMultipartFormSection> multipartFormSections = new List<IMultipartFormSection>();

                multipartFormSections.Add(new MultipartFormFileSection("file", bytes, "", "image/png"));

                byte[] boundary = UnityWebRequest.GenerateBoundary();
                using var unityWebRequest = UnityWebRequest.Post(url, multipartFormSections);
                unityWebRequest.SetRequestHeader("Authorization", $"Bearer {token}");
                //unityWebRequest.SetRequestHeader("Content-Type", "multipart/form-data");
                var operation = unityWebRequest.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();  
                }

                if (unityWebRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Failed: {unityWebRequest.error}");
                }
                else
                {
                    string responseContentType = unityWebRequest.GetResponseHeader("Content-Type");
                    Debug.Log($"responseContentType = {responseContentType}");
                    return new DownloadHandlerData(unityWebRequest.downloadHandler);
                }
  
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(Post)} failed: {ex.Message}");
            }
            return null;
        }
        
        
        
        // public async Task<Sprite> GetImage(string url)
        // {
        //     Sprite sprite = await SharedHelper.CreateSpriteFromImageUrl(url);
        //     return sprite;
        // }
        
        //  Event Handlers --------------------------------

    }
}