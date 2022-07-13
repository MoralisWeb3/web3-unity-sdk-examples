using MoralisUnity.Web3Api.Models;

namespace MoralisUnity.Examples.Sdk.Shared
{
	/// <summary>
	/// Stores the values needed to call the contract
	/// </summary>
	public static class GreeterContractData
	{
		//  Fields ----------------------------------------
		public const ChainList ChainListRequired = ChainList.mumbai;
		public const string FunctionName_getGreeting = "getGreeting";
		public const string FunctionName_setGreeting = "setGreeting";

        ///////////////////////////////////////////
        // TODO: Did you redeploy the contract via
        //		 Hardhat? Then, paste new 2 new
        //		 values here
        ///////////////////////////////////////////
        public const string Address  = "0xbe8b96BB1247cE02321785235473F9559f81921f";
        public const string Abi  = "[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"inputs\":[],\"name\":\"getGreeting\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"string\",\"name\":\"greeting\",\"type\":\"string\"}],\"name\":\"setGreeting\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"string\",\"name\":\"greeting\",\"type\":\"string\"}],\"name\":\"setGreetingSafe\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";

        ///////////////////////////////////////////
        // TODO: Did you redeploy the contract via
        //		 Hardhat? Then, manually recreate
        //		 the object[] below from Abi above
        ///////////////////////////////////////////
		public static object[] GetAbiObject ()
		{
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
