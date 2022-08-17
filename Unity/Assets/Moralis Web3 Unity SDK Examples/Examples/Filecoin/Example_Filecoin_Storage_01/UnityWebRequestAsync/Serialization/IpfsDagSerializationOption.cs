using System;

namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
    public class IpfsDagSerializationOption : ISerializationOption
    {
        //  Properties ------------------------------------
        public ReturnType ReturnType { get { return ReturnType.Binary; } }
        	
        
        //  Fields ----------------------------------------
        public string ContentType => "application/vnd.ipld.car";

        
        //  General Methods -------------------------------
        public T Deserialize<T>(string text)
        {
            //Not needed for binary
            throw new NotImplementedException();
        }
    }
}