using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoralisUnity.Examples.Sdk.Shared.Data.Types
{
    /// <summary>
    /// Render complex objects with custom, prettyfied text layout for debugging
    /// </summary>
    public class ToStringObject
    {
        //  Properties ------------------------------------
        
		
        //  Fields ----------------------------------------
        private string _name = "";
        private bool _isMultiline = false;
        private Dictionary<string, object> _members = new Dictionary<string, object>();

		
        //  Initialization Methods-------------------------
        public ToStringObject(string name, bool isMultiline = false)
        {
            _name = name;
            _isMultiline = isMultiline;
        }
        
		
        //  General Methods -------------------------------
        public void AddMember(string name, object value)
        {
            _members.Add(name, value);
        }
        
        
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (_isMultiline)
            {
                stringBuilder.Append($"[{_name} ...\n\n");

                string lastKey = _members.Keys.Last();
                foreach (KeyValuePair<string, object> kvp in _members)
                {
                    stringBuilder.AppendLine($"{kvp.Key} = {kvp.Value}");
                    if (kvp.Key == lastKey)
                    {
                        stringBuilder.Append($"\n\n");
                    }
                }
                
            }
            else
            {
                stringBuilder.Append($"[{_name}(");

                string lastKey = _members.Keys.Last();
                foreach (KeyValuePair<string, object> kvp in _members)
                {
                    stringBuilder.Append($"{kvp.Key} = {kvp.Value}");
                    if (kvp.Key != lastKey)
                    {
                        stringBuilder.Append($", ");
                    }
                }
                stringBuilder.Append($")]");
            }
            
            return stringBuilder.ToString();
        }
        		
        //  Event Handlers --------------------------------
    }
}