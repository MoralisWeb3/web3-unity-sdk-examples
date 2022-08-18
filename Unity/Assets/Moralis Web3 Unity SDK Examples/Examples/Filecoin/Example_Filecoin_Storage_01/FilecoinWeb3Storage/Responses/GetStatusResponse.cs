using System.Text;
using MoralisUnity.Examples.Sdk.Shared.Data.Types;

#pragma warning disable
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
            ToStringObject toStringObject = new ToStringObject(GetType().Name, true);
            toStringObject.AddMember("peerId", peerId);
            toStringObject.AddMember("peerName", peerName);
            toStringObject.AddMember("region", region);
            toStringObject.AddMember("status", status);
            toStringObject.AddMember("updated", updated);
            return toStringObject.ToString();
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
            ToStringObject toStringObject = new ToStringObject(GetType().Name, true);
            toStringObject.AddMember("dealId", dealId);
            toStringObject.AddMember("storageProvider", storageProvider);
            toStringObject.AddMember("status", status);
            toStringObject.AddMember("dealId", dealId);
            toStringObject.AddMember("pieceCid", pieceCid);
            toStringObject.AddMember("dataCid", dataCid);
            toStringObject.AddMember("dataModelSelector", dataModelSelector);
            toStringObject.AddMember("activation", activation);
            toStringObject.AddMember("created", created);
            toStringObject.AddMember("updated", updated);
            return toStringObject.ToString();
        }
    }
    
    [System.Serializable]
    public class GetStatusResponse 
    {
        public string cid { get; set; }
        public int dagSize { get; set; }
        public string created { get; set; }
        public Pin[] pins { get; set; }
        public Deal[] deals { get; set; }

        public override string ToString()
        {
            bool isMultiline = true;
            bool isExpandingChildren = false;
            ToStringObject toStringObject = new ToStringObject(GetType().Name, isMultiline);
            toStringObject.AddMember("cid", cid);
            toStringObject.AddMember("dagSize", dagSize);
            toStringObject.AddMember("created", created);

            if (isExpandingChildren)
            {
                StringBuilder p = new StringBuilder();
                foreach (Pin pin in pins)
                {
                    if (isMultiline)
                    {
                        p.AppendLine(pin.ToString());
                    }
                    else
                    {
                        p.Append(pin);
                    }
                }
                toStringObject.AddMember("pins", p.ToString());
            
                StringBuilder d = new StringBuilder();
                foreach (Deal deal in deals)
                {
                    if (isMultiline)
                    {
                        d.AppendLine(deal.ToString());
                    }
                    else
                    {
                        d.Append(deal);
                    }
                }
                toStringObject.AddMember("deals", d.ToString());
            }
            
            // For brevity, length is shown instead of full data
            toStringObject.AddMember("pins.length", pins.Length);
            
            // For brevity, length is shown instead of full data
            toStringObject.AddMember("deals.length", deals.Length);
            return toStringObject.ToString();
        }
    }
}