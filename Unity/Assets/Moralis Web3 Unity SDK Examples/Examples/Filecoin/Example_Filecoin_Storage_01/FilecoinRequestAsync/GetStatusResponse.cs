namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
    [System.Serializable]
    public class Pin
    {
        public string peerId { get; set; }
        public string peerName { get; set; }
        public string region { get; set; }
        public string status { get; set; }
        public string updated { get; set; }

        public override string ToString()
        {
            return $"[{this.GetType().Name}(peerId = {peerId}, peerName = {peerName}, region = {region}, status = {status}, updated = {updated})]";
        }
    }
    
    [System.Serializable]
    public class Deal
    {
        public string dealId { get; set; }
        public string storageProvider { get; set; }
        public string status { get; set; }
        public string pieceCid { get; set; }
        public string dataCid { get; set; }
        public string dataModelSelector { get; set; }
        public string activation { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
        
        public override string ToString()
        {
            return $"[{this.GetType().Name}(dataCid = {dataCid}, pieceCid = {pieceCid})]";
        }
    }
    
    [System.Serializable]
    public class GetStatusResponse : IResponse
    {
        public string cid { get; set; }
        public int dagSize { get; set; }
        public string created { get; set; }
        public Pin[] pins { get; set; }
        public Deal[] deals { get; set; }

        public override string ToString()
        {
            return $"[{this.GetType().Name}(cid = {cid}, dagSize = {dagSize}, created = {created})]";
        }
    }
}