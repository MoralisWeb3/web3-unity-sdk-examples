namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
    [System.Serializable]
    public class UploadFileResponse : IResponse
    {
        public string cid { get; set; }

        public override string ToString()
        {
            return $"[{this.GetType().Name}(cid = {cid})]";
        }
    }
}