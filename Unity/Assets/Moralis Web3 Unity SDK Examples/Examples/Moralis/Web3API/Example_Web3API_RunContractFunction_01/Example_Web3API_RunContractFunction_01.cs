using System;
using System.Text;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Sdk.Utilities;
using MoralisUnity.Web3Api.Models;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS1998
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

			// Define contract data
			string functionName = GreeterContractData.FunctionName_getGreeting;
			string address = GreeterContractData.Address;
			string abi = GreeterContractData.Abi;
			ChainList chainListRequired = GreeterContractData.ChainListRequired;
			
			// Define contract request
			RunContractDto runContractDto = new RunContractDto()
			{
				Abi = abi,
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
				string result = await moralisClient.Web3Api.Native.RunContractFunction(address, functionName, runContractDto, chainList);
				
				if (!string.IsNullOrEmpty(result))
				{
					// Format the url
					string resultTrimmed = result.TrimEnd('"').TrimStart('"');
					
					// Call the url
					UnityWebRequest unityWebRequest = UnityWebRequest.Get(resultTrimmed);
					var x  = await unityWebRequest.SendWebRequest();
					
					// Display the url
					outputText.AppendBullet($"result = {unityWebRequest.downloadHandler.text}");
				}
				else
				{
					outputText.AppendBullet($"result = {result}");
				}
			}
			catch (Exception exception)
			{
				Debug.Log("asd: " + exception.Message);
				outputText.AppendBulletException(exception);
			}
			
			return outputText;
		}
	}
}
