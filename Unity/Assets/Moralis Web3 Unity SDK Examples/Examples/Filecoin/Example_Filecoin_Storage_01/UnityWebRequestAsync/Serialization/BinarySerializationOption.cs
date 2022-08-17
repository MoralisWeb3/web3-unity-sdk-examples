using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
    public class BinarySerializationOption : ISerializationOption
    {
        //  Properties ------------------------------------
        public ReturnType ReturnType { get { return ReturnType.Binary; } }
        	
        
        //  Fields ----------------------------------------
        public string ContentType => "application/x-binary";

        
        //  General Methods -------------------------------
        public T Deserialize<T>(string text)
        {
            //Not needed for binary
            throw new NotImplementedException();
        }
    }
}