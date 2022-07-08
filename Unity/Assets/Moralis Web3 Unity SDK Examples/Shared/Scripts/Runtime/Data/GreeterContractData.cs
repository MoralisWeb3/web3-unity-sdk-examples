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
		public const string Address = "0x6E79224C690b2b7ef60329309785739Bf6f06f1c";
		public const string Abi =
			"[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":false,\"internalType\":\"string\",\"name\":\"result\",\"type\":\"string\"}],\"name\":\"OnSetGreetingCompleted\",\"type\":\"event\"},{\"inputs\":[],\"name\":\"getGreeting\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"pure\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"string\",\"name\":\"greeting\",\"type\":\"string\"}],\"name\":\"setGreeting\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
	}
}
