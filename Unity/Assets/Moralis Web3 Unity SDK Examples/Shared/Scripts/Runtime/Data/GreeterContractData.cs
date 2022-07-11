using MoralisUnity.Web3Api.Models;

namespace MoralisUnity.Examples.Sdk.Shared
{
	/// <summary>
	/// Stores the 
	/// </summary>
	public static class GreeterContractData
	{
		//  Fields ----------------------------------------
		public const ChainList ChainListRequired = ChainList.mumbai;
		public const string FunctionName_getGreeting = "getGreeting";
		public const string FunctionName_setGreeting = "setGreeting";

		public const string Address  = "0xAC7FbA74256774493F6EAB6bd028025AC470235c";
		public const string Abi  = "[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"inputs\":[],\"name\":\"getGreeting\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"string\",\"name\":\"greeting\",\"type\":\"string\"}],\"name\":\"setGreeting\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";

		public static object[] GetAbiObject ()
		{
			///////////////////////////////////////////
			// Abi Format: Manually Created in C#
			// Use "GreeterContractData.Abi" as a reference
			///////////////////////////////////////////
			object[] newAbi = new object[3];
			
			// constructor
			object[] cInputParams = new object[0];
			newAbi[0] = new { inputs = cInputParams, name = "", stateMutability = "nonpayable", type = "constructor" };
			
			// getGreeting
			object[] gInputParams = new object[0];
			object[] gOutputParams = new object[1];
			gOutputParams[0] = new { internalType = "string", name = "", type = "string" };
			newAbi[1] = new { inputs = gInputParams, outputs = gOutputParams, name = "getGreeting", stateMutability = "view", type = "function" };

			// setGreeting
			object[] sInputParams = new object[1];
			sInputParams[0] = new { internalType = "string", name = "greeting", type = "string" };
			object[] sOutputParams = null;
			newAbi[2] = new { inputs = sInputParams, outputs = sOutputParams, name = "setGreeting", stateMutability = "nonpayable", type = "function" };

			return newAbi;
		}

	}
}
