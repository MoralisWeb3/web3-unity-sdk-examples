using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{

    /// <summary>
    /// DownloadHandler gets disposed and cannot be returned directly
    /// Returning DownloadHandlerData gives flexibility to the calling
    /// scope to use either text or data as desired
    /// </summary>
    public class RawResponse
    {
        public readonly byte[] data;
        public readonly string text;

        public RawResponse(DownloadHandler downloadHandler)
        {
            data = downloadHandler.data;
            text = downloadHandler.text;
        }
    }
    
    /// <summary>
    /// </summary>
    public class UnityWebRequestAsync
    {
        //  Properties ------------------------------------
        
        //  Fields ----------------------------------------

        
        //  Initialization Methods-------------------------
        public UnityWebRequestAsync()
        {
        }

        //  General Methods -------------------------------

        public async Task<RawResponse> Get(string url)
        {
            try
            {
                using var unityWebRequest = UnityWebRequest.Get(url);
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
                    return new RawResponse(unityWebRequest.downloadHandler);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(Get)} failed: {ex.Message}");
                
            }

            return null;

        }
        
        /// <summary>
        ///See https://web3.storage/docs/reference/http-api/ for the required CURL format
        ///
        /// curl -X 'POST' \
        /// 'https://api.web3.storage/upload' \
        /// -H 'accept: application/json' \
        /// -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkaWQ6ZXRocjoweEY0MjU3Q2IyZDMyOWEzRWIzMTM1MzI1YzgyYzAzNkFlYWMwMkE3NDgiLCJpc3MiOiJ3ZWIzLXN0b3JhZ2UiLCJpYXQiOjE2NjA2NzQxNjI0MTMsIm5hbWUiOiJ3ZWIzLXVuaXR5LXNkay1leGFtcGxlcyJ9.FAtQ2W7HxzLAG68U1clOE5CpjaWYbYvrnlTmeVm53as' \
        /// -H 'Content-Type: multipart/form-data' \
        /// -F 'file=@art_watercolor.jpg;type=image/jpeg' \
        /// -F 'file=@photo.png;type=image/png'
        /// </summary>
        /// <returns></returns>
        public async Task<RawResponse> Post(string token, string url, List<IMultipartFormSection> multipartFormSections, Dictionary<string, string> requestHeaders = null)
        {
            try
            {
                using var unityWebRequest = UnityWebRequest.Post(url, multipartFormSections);

                if (requestHeaders != null)
                {
                    foreach (KeyValuePair<string, string> kvp in requestHeaders)
                    {
                        unityWebRequest.SetRequestHeader(kvp.Key, kvp.Value);
                    }
                }
               
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
                    return new RawResponse(unityWebRequest.downloadHandler);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(Post)} failed: {ex.Message}");
            }
            return null;
        }
        
        //  Event Handlers --------------------------------

    }
}