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
			string address = "";
			string functionName = "";
			RunContractDto runContractDto = null;
			ChainList chainListRequired = GreeterContractData.ChainListRequired;
			string outputAfterResult = "";
			
			// There are two examples scripts you can try. Try each.
			const bool useExampleScript01 = true;
			if (useExampleScript01)
			{
				///////////////////////////////////////////
				// Abi Format: Manually Created in C#
				///////////////////////////////////////////
				//
				object[] inputParams = new object[1];
				inputParams[0] = new { internalType = "uint256", name = "id", type = "uint256" };
				// 
				object[] outputParams = new object[1];
				outputParams[0] = new { internalType = "string", name = "", type = "string" };
				// 
				object[] abi = new object[1];
				abi[0] = new { inputs = inputParams, name = "uri", outputs = outputParams, stateMutability = "view", type = "function" };

				// Prepare the function values
				outputAfterResult = "This function returns results in the format of Json. Success!";
				address = "0x698d7D745B7F5d8EF4fdB59CeB660050b3599AC3"; 
				functionName = "uri";
				
				// Prepare the contract request
				runContractDto = new RunContractDto()
				{
					Abi = abi,
					Params = new { id = "15310200874782" }
				};
			}
			else 
			{
				// Prepare the function values
				outputAfterResult = "This function returns results in the format of string. Success!";
				address = GreeterContractData.Address;
				functionName = GreeterContractData.FunctionName_getGreeting;
				
				///////////////////////////////////////////
				// Abi Format:	String pasted here,
				//				from Hardhat deployment
				///////////////////////////////////////////
				string abi = GreeterContractData.Abi;
			
				// Prepare the contract request
				runContractDto = new RunContractDto()
				{
					Abi = abi,
					Params = new { blah = "fun" }
				};
			}
			
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
