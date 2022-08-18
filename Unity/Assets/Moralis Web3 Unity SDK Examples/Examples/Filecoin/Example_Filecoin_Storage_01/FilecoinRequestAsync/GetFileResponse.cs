namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
    [System.Serializable]
    public class GetFileResponse : IResponse
    {
        public byte[] data { get; set; }
        
        public override string ToString()
        {
            return $"[{this.GetType().Name}(data = {data})]";
        }
    }
}