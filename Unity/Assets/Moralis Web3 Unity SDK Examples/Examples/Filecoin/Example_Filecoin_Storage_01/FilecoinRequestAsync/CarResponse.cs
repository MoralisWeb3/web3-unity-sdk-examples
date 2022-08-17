namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
    [System.Serializable]
    public class CarResponse : IResponse
    {
        public byte[] bytes { get; set; }
        
        public override string ToString()
        {
            return $"[{this.GetType().Name}(bytes = {bytes})]";
        }
    }
}