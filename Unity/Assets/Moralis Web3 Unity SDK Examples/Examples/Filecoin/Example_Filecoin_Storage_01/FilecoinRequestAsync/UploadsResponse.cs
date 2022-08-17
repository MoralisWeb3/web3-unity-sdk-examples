namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
    [System.Serializable]
    public class UploadsResponsePart : IResponse
    {
        public string _id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
        public string cid { get; set; }
        public string dagSize { get; set; }
        public Pin[] pins { get; set; }
        public Deal[] deals { get; set; }

        public override string ToString()
        {
            return $"[{this.GetType().Name}(_id = {_id})]";
        }
    }
    
    [System.Serializable]
    public class UploadsResponse : IResponse
    {
        public UploadsResponsePart[] x { get; set; }

        public override string ToString()
        {
            return $"[{this.GetType().Name}(UploadResponses.Length = {x.Length})]";
        }
    }
}