using MoralisUnity.Examples.Sdk.Shared.Data.Types;

namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
    [System.Serializable]
    public class GetFileResponse 
    {
        public byte[] data { get; set; }
        public string url { get; set; }
        
        public override string ToString()
        {
            ToStringObject toStringObject = new ToStringObject(this.GetType().Name);
            toStringObject.AddMember("Url", url);
            
            // For brevity, length is shown instead of full data
            toStringObject.AddMember("Data.Length", data.Length);
            return toStringObject.ToString();
        }
    }
}