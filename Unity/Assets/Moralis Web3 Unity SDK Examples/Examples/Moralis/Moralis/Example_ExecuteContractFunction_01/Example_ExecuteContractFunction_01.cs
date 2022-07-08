using System;
using System.Text;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Platform.Objects;
using MoralisUnity.Platform.Queries;
using MoralisUnity.Sdk.Utilities;
using MoralisUnity.Web3Api.Models;
using Nethereum.Hex.HexTypes;
using UnityEngine;
using WalletConnectSharp.Unity;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_ExecuteContractFunction_01
{
	public class MoriaGatesEvent : MoralisObject
	{
		public const string Name = "MoriaGatesEvent";
		public string result { get; set; }
        
		public MoriaGatesEvent() : base(MoriaGatesEvent.Name) {}
	}
	
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
		
		private static MoralisQuery<MoriaGatesEvent> _getEventsQuery;
		private static MoralisLiveQueryCallbacks<MoriaGatesEvent> _queryCallbacks;
		
		//  General Methods -------------------------------	
		public static async Cysharp.Threading.Tasks.UniTask<StringBuilder>  
			Moralis_ExecuteContractFunction(
			ChainList chainList, StringBuilder outputText)
		{
			
			_getEventsQuery = await Moralis.GetClient().Query<MoriaGatesEvent>();
			_queryCallbacks = new MoralisLiveQueryCallbacks<MoriaGatesEvent>();
			_queryCallbacks.OnUpdateEvent += HandleContractEventResponse;
			MoralisLiveQueryController.AddSubscription<MoriaGatesEvent>(MoriaGatesEvent.Name, _getEventsQuery, _queryCallbacks);
			
			
			//TODO: Call this on destroy?
			//MoralisLiveQueryController.RemoveSubscriptions(MoriaGatesEvent.Name);

			
			// Setup Web 3
			if (WalletConnect.Instance == null)
			{
				throw new Exception(
					$"ExecuteContractFunction() failed. " +
					$"WalletConnect.Instance must not be null. " +
					$"Add the WalletConnect.prefab to your scene.");
			}

			await Moralis.SetupWeb3();

			// Define contract data
			string functionName = GreeterContractData.FunctionName_setGreeting;
			string address = GreeterContractData.Address;
			string abi = GreeterContractData.Abi;
			ChainList chainListRequired = GreeterContractData.ChainListRequired;
			
			// Estimate the gas
			HexBigInteger value = new HexBigInteger(0);
			HexBigInteger gas = new HexBigInteger(0);
			HexBigInteger gasPrice = new HexBigInteger(0);

			// Define contract request
			int randomNumber = UnityEngine.Random.Range(0, 1000); // Randomize string for fun!
			string greeting = string.Format($"Hello World {randomNumber} !");
			object[] args =
			{
				greeting
			};

			string addressFormatted = Formatters.GetWeb3AddressShortFormat(address);
			outputText.AppendHeaderLine(
				$"Web3Api.Native.RunContractFunction({addressFormatted}, {abi.ToString().Length}, {functionName}, {args}, {value}, {gas}, {gasPrice})");
			string result = "NOPE";
			
			try
			{
				if (chainList != chainListRequired)
				{
					throw new Exception($"Error. You must use {chainListRequired} chain for this specific contract");
				}

				///////////////////////////////////////////
				// Execute: RunContractFunction
				///////////////////////////////////////////
				Debug.Log("ExecuteContractFunction Call Pending ...");
				
				result = await Moralis.ExecuteContractFunction(address, abi, functionName, args, value, gas, gasPrice);
				
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
		private static void HandleContractEventResponse(MoriaGatesEvent newEvent, int requestId)
		{
			Debug.Log($"HandleContractEventResponse() {newEvent} and {requestId}");
		}
	}
}
