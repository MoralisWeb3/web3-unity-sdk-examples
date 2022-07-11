using System;
using System.Text;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Sdk.Utilities;
using MoralisUnity.Web3Api.Models;

// ReSharper disable once ConditionIsAlwaysTrueOrFalse
#pragma warning disable CS1998, CS0162
namespace MoralisUnity.Examples.Sdk.Example_Web3API_RunContractFunction_01
{
	/// <summary>
	/// Example: INativeApi
	///
	/// Documentation: <see cref="http://docs.moralis.io/unity"/>
	/// 
	/// Coding Concerns
	/// <list type="bullet">
	/// 
	/// <item>See <see cref="Example_UI"/> for the UI functionality</item>
	/// <item>See below for the core functionality</item>
	/// 
	/// </list>
	/// </summary>
	public class Example_Web3API_RunContractFunction_01 : Example_UI
	{
		//  Fields ----------------------------------------
		
		
		//  General Methods -------------------------------	
		public static async Cysharp.Threading.Tasks.UniTask<StringBuilder>  MoralisClient_Web3Api_Native_RunContractFunction(
			ChainList chainList, StringBuilder outputText)
		{
			
			// Prepare the function values
			string address = GreeterContractData.Address;
			string functionName = GreeterContractData.FunctionName_getGreeting;
			RunContractDto runContractDto = null;
			ChainList chainListRequired = GreeterContractData.ChainListRequired;
			string outputAfterResult = "This function returns results in the format of string. Success!";
			
			// Prepare the contract request
			object[] abiObject = new object[3];
			
			// constructor
			object[] cInputParams = null;
			abiObject[0] = new { inputs = cInputParams, name = "", stateMutability = "nonpayable", type = "constructor" };
			
			// getGreeting
			object[] gInputParams = null;
			object[] gOutputParams = new object[1];
			gOutputParams[0] = new { internalType = "string", name = "", type = "string" };
			abiObject[1] = new { inputs = gInputParams, outputs = gOutputParams, name = "getGreeting", stateMutability = "view", type = "function" };

			// setGreeting
			object[] sInputParams = new object[1];
			sInputParams[0] = new { internalType = "string", name = "greeting", type = "string" };
			object[] sOutputParams = null;
			abiObject[2] = new { inputs = sInputParams, outputs = sOutputParams, name = "setGreeting", stateMutability = "nonpayable", type = "function" };

			runContractDto = new RunContractDto()
			{
				Abi = abiObject,
				Params = null
			};
			
			string addressFormatted = Formatters.GetWeb3AddressShortFormat(address);
			outputText.AppendHeaderLine(
				$"Web3Api.Native.RunContractFunction({addressFormatted}, {functionName}, {runContractDto.Abi.ToString().Length}, {chainList})");
			
			try
			{
				if (chainList != chainListRequired)
				{
					throw new Exception($"Error. You must use {chainListRequired} chain for this specific contract");
				}

				///////////////////////////////////////////
				// Execute: RunContractFunction
				///////////////////////////////////////////
				MoralisClient moralisClient = Moralis.GetClient();
				string result = await moralisClient.Web3Api.Native.RunContractFunction(address, functionName, 
					runContractDto, chainList);
				
				outputText.AppendBullet($"result = {result}");
				outputText.AppendHeaderLine($"Comment");
				outputText.AppendLine($"{outputAfterResult}");
			}
			catch (Exception exception)
			{
				outputText.AppendBulletException(exception);
			}
			
			return outputText;
		}
	}
}