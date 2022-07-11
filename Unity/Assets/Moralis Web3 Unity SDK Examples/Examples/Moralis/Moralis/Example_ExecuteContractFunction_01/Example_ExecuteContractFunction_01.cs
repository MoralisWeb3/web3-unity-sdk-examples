using System;
using System.Text;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Sdk.Utilities;
using MoralisUnity.Web3Api.Models;
using Nethereum.Hex.HexTypes;
using UnityEngine;
using WalletConnectSharp.Unity;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_ExecuteContractFunction_01
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
	public class Example_ExecuteContractFunction_01 : Example_UI
	{
		//  Fields ----------------------------------------
		
		//  General Methods -------------------------------	
		
		public static async Cysharp.Threading.Tasks.UniTask<StringBuilder>
			Moralis_ExecuteContractFunction_Set(
				ChainList chainList, StringBuilder outputText)
		{
			
			// Define functionName
			string functionName = GreeterContractData.FunctionName_setGreeting;
			
			// Define contract request
			int randomNumber = UnityEngine.Random.Range(0, 1000); // Randomize string for fun!
			string greeting = string.Format($"Hello World {randomNumber} !");
			object[] args =
			{
				greeting
			};
			
			return await Moralis_ExecuteContractFunction(functionName, args, chainList, outputText);
		}
		
		
		public static async Cysharp.Threading.Tasks.UniTask<StringBuilder>
			Moralis_ExecuteContractFunction_Get(
				ChainList chainList, StringBuilder outputText)
		{
			
			// Define functionName
			string functionName = GreeterContractData.FunctionName_getGreeting;
			
			// Define contract request
			object[] args = null;
			
			return await Moralis_ExecuteContractFunction(functionName, args, chainList, outputText);
		}


		private static async Cysharp.Threading.Tasks.UniTask<StringBuilder>  
			Moralis_ExecuteContractFunction(
				string functionName, object[] args,
				ChainList chainList, StringBuilder outputText)
		{
			
			// Define contract data
			string address = GreeterContractData.Address;
			string abi = GreeterContractData.Abi;
			ChainList chainListRequired = GreeterContractData.ChainListRequired;
			string outputAfterResult = "This function returns the TransactionHash as result in the format of string. Success!";
			
			// Validate
			if (chainList != chainListRequired)
			{
				throw new Exception($"Error. You must use {chainListRequired} chain for this specific contract");
			}
			
			// Setup Web 3
			if (WalletConnect.Instance == null)
			{
				throw new Exception(
					$"ExecuteContractFunction() failed. " +
					$"WalletConnect.Instance must not be null. " +
					$"Add the WalletConnect.prefab to your scene.");
			}
	
			await Moralis.SetupWeb3();
			
			// Estimate the gas
			HexBigInteger value = new HexBigInteger(0);
			HexBigInteger gas = new HexBigInteger(0);
			HexBigInteger gasPrice = new HexBigInteger(2000); //change to o?


			string addressFormatted = Formatters.GetWeb3AddressShortFormat(address);
			string argsString = "";
			if (args == null)
			{
				argsString = "null";
			}
			else
			{
				argsString = args.ToString();
			}
			
			outputText.AppendHeaderLine(
				$"Moralis.ExecuteContractFunction({addressFormatted}, {abi.ToString().Length}, {functionName}, {argsString}, {value}, {gas}, {gasPrice})");
			
			try
			{
				///////////////////////////////////////////
				// Execute: ExecuteContractFunction
				///////////////////////////////////////////
				Debug.Log("ExecuteContractFunction Call Pending ...");
				
				string result = await Moralis.ExecuteContractFunction(address, abi, functionName, args, value, gas, gasPrice);
				
				Debug.Log("ExecuteContractFunction Call Completed");
				
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
		
		//  Event Handlers --------------------------------
	}
}
