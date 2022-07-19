using System.Collections.Generic;
using MoralisUnity.Platform.Objects;

namespace MoralisUnity.Examples.Sdk.Shared.Data.Types
{
	/// <summary>
	/// Example: Moralis Object
	/// </summary>
	public class Hero : MoralisObject
	{
		//  Properties ------------------------------------
		public int Strength { get; set; }
		public int Level { get; set; }
		public string Name { get; set; }
		public string Warcry { get; set; }
		public List<string> Bag { get; set; }
		
		//  Fields ----------------------------------------

		//  Initialization Methods ---------------------------------
		public Hero() : base("Hero") 
		{
			Bag = new List<string>();
		}
		
		//  General Methods -------------------------------	
		public override string ToString()
		{
			// Show a short id to conserve display text area
			string id = $"{objectId.Substring(0, 6)}...";
			return $"[Hero (N={Name}, Id={id})]";
		}


		//  Event Handlers --------------------------------
	}
}
