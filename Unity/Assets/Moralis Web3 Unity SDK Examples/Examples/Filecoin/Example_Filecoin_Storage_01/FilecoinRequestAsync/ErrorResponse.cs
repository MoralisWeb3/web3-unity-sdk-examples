namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
    [System.Serializable]
    public class ErrorResponse : IResponse
    {
        public string name { get; set; }
        public string message { get; set; }
        
        public override string ToString()
        {
            return $"[{this.GetType().Name}(name = {name}, message = {message})]";
        }
    }
}