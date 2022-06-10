using System;
using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Web3Client_SendTransactionAsync_01	
{
	/// <summary>
	/// Example: ITransactionManager
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
	public class Example_Web3Client_SendTransactionAsync_01 : Example_UI
	{
		
		//  General Methods -------------------------------	

		public static async UniTask<StringBuilder>
			Web3Client_Eth_TransactionManager_SendTransactionAsync(
				StringBuilder outputText)
		{
			
			// TODO: Moralis User must hardcode value address here.
			string fromAddress = "";
			
			// TODO: Moralis User must hardcode value address here.
			string toAddress = fromAddress;

			float amountToSendNative = 1;
			var amountToSendWei = UnitConversion.Convert.ToWei(amountToSendNative);

			// Validation
			TransactionInput transactionInput = new TransactionInput()
			{
				Data = String.Empty,
				From = fromAddress,
				To = toAddress,
				Value = new HexBigInteger(amountToSendWei)
			};
			
			bool isValidTransactionInput = Example_UI.IsValidTransactionInput(ref outputText, transactionInput);

			if (isValidTransactionInput)
			{
				try
				{
					string result =
						await Moralis.Web3Client.Eth.TransactionManager.SendTransactionAsync(transactionInput);
					outputText.AppendBullet($"Success! Transferred {amountToSendWei} " +
					                        $"WEI from {fromAddress} to {toAddress}, result = {result}");
				}
				catch (Exception exception)
				{
					outputText.AppendBulletError($"Failed! Transfer of {amountToSendWei} " +
					                             $"WEI from {fromAddress} to {toAddress}, error = {exception.Message}");
				}
			}
			
			return outputText;
		}
	}
}
