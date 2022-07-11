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
			ChainList chainListRequired = GreeterContractData.ChainListRequired;
			string outputAfterResult = "This function result is string format. Success!";
			
			// NOTE: RunContractFunction requires Abi in **object[]** format
			object[] abi = GreeterContractData.GetAbiObject();
			
			// Prepare the contract request
			RunContractDto runContractDto = new RunContractDto()
			{
				Abi = abi,
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