
using MoralisUnity.Examples.Sdk.Shared.Data.Types;

namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
    
    [System.Serializable]
    public class UploadFileResponse 
    {
        public string cid { get; set; }

        public override string ToString()
        {
            ToStringObject toStringObject = new ToStringObject(this.GetType().Name);
            toStringObject.AddMember("Cid", cid);
            return toStringObject.ToString();
        }
    }
}