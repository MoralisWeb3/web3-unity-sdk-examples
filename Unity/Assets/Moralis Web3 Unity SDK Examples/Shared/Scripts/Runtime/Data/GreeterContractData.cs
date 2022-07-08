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
		public const string Address  = "0x101b8778224de99Eab54CC91596f88738854F8aB";
		public const string Abi  = "[{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"inputs\":[],\"name\":\"getGreeting\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"\",\"type\":\"string\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"string\",\"name\":\"greeting\",\"type\":\"string\"}],\"name\":\"setGreeting\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";
		
	}
}
