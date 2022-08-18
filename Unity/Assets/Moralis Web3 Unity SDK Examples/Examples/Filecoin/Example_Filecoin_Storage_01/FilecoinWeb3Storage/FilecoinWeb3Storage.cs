using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{

    /// <summary>
    /// Wrapper for <see cref="UnityWebRequest"/> to be async.
    /// 
    /// See <see cref="https://github.com/crevelop/unitywebrequest-tutorial"/>
    /// 
    /// </summary>
    public class FilecoinWeb3Storage : UnityWebRequestAsync
    {
        //  Properties ------------------------------------


        //  Fields ----------------------------------------
        private readonly string _token;
        private const string _baseUrl = "https://api.web3.storage";


        //  Initialization Methods-------------------------
        public FilecoinWeb3Storage(string token)
        {
            _token = token;
        }

        //  General Methods (With Auth) ---------------------
        
        /// <summary>
        /// Upload file of bytes, returns the cid.
        ///
        /// See <see cref="https://web3.storage/docs/reference/http-api/"/>
        /// 
        /// </summary>
        public async Task<UploadFileResponse> UploadFile(byte[] bytes)
        {
            string url = _baseUrl + $"/upload";
            
            List<IMultipartFormSection> multipartFormSections = new List<IMultipartFormSection>();
            multipartFormSections.Add(new MultipartFormFileSection("file", bytes, "", "image/png"));

            Dictionary<string, string> requestHeaders = new Dictionary<string, string>();
            requestHeaders.Add("Authorization", $"Bearer {_token}");
            RawResponse rawResponse = await Post(_token, url, multipartFormSections, requestHeaders);
            
            // Return object from text
            return JsonConvert.DeserializeObject<UploadFileResponse>(rawResponse.text);
        }


        //  General Methods (No Auth) ---------------------

        /// <summary>
        /// GetStatus of a given cid.
        ///
        /// See <see cref="https://web3.storage/docs/reference/http-api/"/>
        /// 
        /// </summary>
        public async Task<GetStatusResponse> GetStatus(string cid)
        {
            string url = _baseUrl + $"/status/{cid}";
            RawResponse rawResponse = await Get(url);
            
            // Return object from text
            return JsonConvert.DeserializeObject<GetStatusResponse>(rawResponse.text);
        }

        /// <summary>
        /// Get file for a given cid.
        ///
        /// See <see cref="https://web3.storage/docs/reference/http-api/"/>
        /// 
        /// </summary>
        public async Task<GetFileResponse> GetFile(string cid)
        {
            string url = $"https://{cid}.ipfs.w3s.link";
            RawResponse rawResponse = await Get(url);
            
            // Return binary data
            return new GetFileResponse
            {
                data = rawResponse.data,
                url = url
            };
        }

        //  Event Handlers --------------------------------

    }
}