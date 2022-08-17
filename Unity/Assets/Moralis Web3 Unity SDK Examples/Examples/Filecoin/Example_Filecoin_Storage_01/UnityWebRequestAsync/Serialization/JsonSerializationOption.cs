using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
    public class JsonSerializationOption : ISerializationOption
    {
        //  Properties ------------------------------------
        public ReturnType ReturnType { get { return ReturnType.RequiresDeserialization; } }
        		
        
        //  Fields ----------------------------------------
        public string ContentType => "application/json";

        
        //  General Methods -------------------------------
        public T Deserialize<T>(string text)
        {
            try
            {
                //Works
                var result = JsonConvert.DeserializeObject<T>(text);
                return result;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Deserialize 1 () failed for {typeof(T).Name}. Message = {ex.Message}");
                Debug.LogWarning($"\n\n{text}\n\n");
            }
            
            try
            {
                var result = JsonConvert.DeserializeObject<List<T>>(text);
                Debug.Log("result[0]: " + result[0]);
                return result[0];
            }
            catch (Exception ex)
            {
                Debug.LogError($"Deserialize 2 () failed for {typeof(T).Name}. Message = {ex.Message}");
                Debug.LogWarning($"\n\n{text}\n\n");
                return default(T);
            }
        }
    }
}