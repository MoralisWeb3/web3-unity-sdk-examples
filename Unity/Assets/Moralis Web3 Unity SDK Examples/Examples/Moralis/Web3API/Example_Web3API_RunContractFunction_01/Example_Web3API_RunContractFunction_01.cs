using System;
using System.Text;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Sdk.Utilities;
using MoralisUnity.Web3Api.Models;
using UnityEngine.Networking;
using UniTask = Cysharp.Threading.Tasks.UniTask;

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
		
		// The contract used in this sample is setup only for mumbai
		public const ChainList ChainListRequired = ChainList.mumbai;

		
		//  General Methods -------------------------------	
		
		public static async Cysharp.Threading.Tasks.UniTask<StringBuilder> MoralisClient_Web3Api_Native_RunContractFunction(
			ChainList chainList, StringBuilder outputText)
		{
			
			string address = ExampleConstants.AddressForContractTesting; 
			string addressFormatted = Formatters.GetWeb3AddressShortFormat(address);
			MoralisClient moralisClient = Moralis.GetClient();
			
			// Function ABI input parameters
			object[] inputParams = new object[1];
			inputParams[0] = new { internalType = "uint256", name = "id", type = "uint256" };
			
			// Function ABI Output parameters
			object[] outputParams = new object[1];
			outputParams[0] = new { internalType = "string", name = "", type = "string" };
			
			// Function ABI
			object[] abi = new object[1];
			abi[0] = new { inputs = inputParams, name = "uri", outputs = outputParams, stateMutability = "view", type = "function" };

			// Define request object
			RunContractDto runContractDto = new RunContractDto()
			{
				Abi = abi,
				Params = new { id = "15310200874782" }
			};
			string functionName = "uri";

			outputText.AppendHeaderLine(
				$"Web3Api.Native.RunContractFunction({addressFormatted}, {functionName}, {runContractDto.Abi}, {chainList})");

			try
			{
				if (chainList != ChainListRequired)
				{
					throw new Exception($"Error. You must use {ChainListRequired} chain for this contract");
				}

				///////////////////////////////////////////
				// Execute: RunContractFunction
				///////////////////////////////////////////
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
				outputText.AppendBulletException(exception);
			}
			
			return outputText;
		}
	}
}
