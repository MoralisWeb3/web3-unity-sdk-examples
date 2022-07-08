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
			Moralis_ExecuteContractFunction(
			ChainList chainList, StringBuilder outputText)
		{
			
			// Define contract data
			string functionName = GreeterContractData.FunctionName_setGreeting;
			string address = GreeterContractData.Address;
			string abi = GreeterContractData.Abi;
			ChainList chainListRequired = GreeterContractData.ChainListRequired;
			
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

			// Define contract request
			int randomNumber = UnityEngine.Random.Range(0, 1000); // Randomize string for fun!
			string greeting = string.Format($"Hello World {randomNumber} !");
			object[] args =
			{
				greeting
			};

			string addressFormatted = Formatters.GetWeb3AddressShortFormat(address);
			outputText.AppendHeaderLine(
				$"Moralis.ExecuteContractFunction({addressFormatted}, {abi.ToString().Length}, {functionName}, {args}, {value}, {gas}, {gasPrice})");
			
			try
			{
				///////////////////////////////////////////
				// Execute: ExecuteContractFunction
				///////////////////////////////////////////
				Debug.Log("ExecuteContractFunction Call Pending ...");
				
				string result = await Moralis.ExecuteContractFunction(address, abi, functionName, args, value, gas, gasPrice);
				
				Debug.Log("ExecuteContractFunction Call Completed");
				
				outputText.AppendBullet($"result = {result}");
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
