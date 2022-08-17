﻿using System;

namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
    public class TextSerializationOption : ISerializationOption
    {
        //  Properties ------------------------------------
        public ReturnType ReturnType { get { return ReturnType.Text; } }
        	
        
        //  Fields ----------------------------------------
        public string ContentType => "application/text";

        
        //  General Methods -------------------------------
        public T Deserialize<T>(string text)
        {
       
            //not needed
            throw new NotImplementedException();
        }
    }
}