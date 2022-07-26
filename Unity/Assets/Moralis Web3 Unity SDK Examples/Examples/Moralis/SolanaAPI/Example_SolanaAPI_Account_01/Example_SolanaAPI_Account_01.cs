using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Examples.Sdk.Shared.Data.Types;
using MoralisUnity.Sdk.Utilities;
using MoralisUnity.SolanaApi.Models;
using MoralisUnity.Web3Api.Models;
using UnityEngine;
using NativeBalance = MoralisUnity.SolanaApi.Models.NativeBalance;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_SolanaAPI_Account_01
{
	/// <summary>
	/// Example: IAccountApi
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
	public class Example_SolanaAPI_Account_01 : Example_UI
	{
		//  Fields ----------------------------------------
		
		/// <summary>
		/// Limit results to conserve text display space
		/// </summary>
		private static readonly LoopLimit LoopLimit = new LoopLimit(3);

		//  General Methods -------------------------------	
		private static string GetExampleAddress()
		{
			// TODO: In production, consider to use: Moralis.GetUser().ethAddress
			return SharedConstants.SolanaAddressForTesting;
		}

		public static async UniTask<StringBuilder> MoralisClient_SolanaApi_Account_Balance(
			StringBuilder outputText)
		{
			
			string address = GetExampleAddress();
			string addressFormatted = Formatters.GetWeb3AddressShortFormat(address);
			NetworkTypes networkTypes = NetworkTypes.mainnet;
			MoralisClient moralisClient = Moralis.GetClient();
			
			outputText.AppendHeaderLine(
				$"SolanaApi.Account.Balance({networkTypes}, {addressFormatted})");

			try
			{
				///////////////////////////////////////////
				// Execute: Balance
				///////////////////////////////////////////
				NativeBalance nativeBalance =
					await moralisClient.SolanaApi.Account.Balance(networkTypes, address);
				
				outputText.AppendBullet($"nativeBalance.Solana = {nativeBalance.Solana}");
			}
			catch (Exception exception)
			{
				AppendBulletExceptionSafe(ref outputText, address, exception);
			}
			
			return outputText;
		}


		public static async UniTask<StringBuilder> MoralisClient_SolanaApi_Account_GetPortfolio(
			StringBuilder outputText)
		{
			string address = GetExampleAddress();
			string addressFormatted = Formatters.GetWeb3AddressShortFormat(address);
			NetworkTypes networkTypes = NetworkTypes.mainnet;
			MoralisClient moralisClient = Moralis.GetClient();

			outputText.AppendHeaderLine(
				$"SolanaApi.Account.GetPortfolio({networkTypes}, {addressFormatted})");
			
			try
			{
				///////////////////////////////////////////
				// Execute: GetPortfolio
				///////////////////////////////////////////
				Portfolio portfolio = 
					await moralisClient.SolanaApi.Account.GetPortfolio(networkTypes, address);
				
				outputText.AppendBullet($"portfolio.Nfts.Count = {portfolio.Nfts.Count}");

				LoopLimit.Reset();
				outputText.AppendBulletLoopLimit(LoopLimit);
				foreach (SplNft splNft in portfolio.Nfts)
				{
					if (LoopLimit.IsAtLimit())
					{
						break;
					}

					outputText.AppendBullet($"splNft.AssociatedTokenAddress = {splNft.AssociatedTokenAddress}", 2);
				}
			}
			catch (Exception exception)
			{
				AppendBulletExceptionSafe(ref outputText, address, exception);
			}

			return outputText;
		}

		
		public static async UniTask<StringBuilder> MoralisClient_SolanaApi_Account_GetNFTs(
			StringBuilder outputText)
		{

			string address = GetExampleAddress();
			string addressFormatted = Formatters.GetWeb3AddressShortFormat(address);
			NetworkTypes networkTypes = NetworkTypes.mainnet;
			MoralisClient moralisClient = Moralis.GetClient();

			outputText.AppendHeaderLine(
				$"SolanaApi.Account.GetNFTs({networkTypes}, {addressFormatted})");

			try
			{
				///////////////////////////////////////////
				// Execute: GetNFTs
				///////////////////////////////////////////
				List<SplNft> splNfts = 
					await moralisClient.SolanaApi.Account.GetNFTs(networkTypes, address);
				
				outputText.AppendBullet($"splNfts.Count = {splNfts.Count}");

				LoopLimit.Reset();
				outputText.AppendBulletLoopLimit(LoopLimit);
				foreach (SplNft splNft in splNfts)
				{
					if (LoopLimit.IsAtLimit())
					{
						break;
					}

					outputText.AppendBullet($"splNft.AssociatedTokenAddress = {splNft.AssociatedTokenAddress}", 2);
				}
			}
			catch (Exception exception)
			{
				AppendBulletExceptionSafe(ref outputText, address, exception);
			}

			return outputText;
		}

		private static void AppendBulletExceptionSafe(ref StringBuilder outputText, string address, Exception exception)
		{
			// KEEP: Shows line number which is helpful for Moralis Customers
			Debug.LogException(exception);
			
			// Show in UI
			outputText.AppendBulletException(exception);
			outputText.AppendLine();
			outputText.AppendErrorLine($"Exception was for address of...");
			outputText.AppendErrorLine($"{address}");
			outputText.AppendLine();
			outputText.AppendErrorLine($"SolanaAPI throws exceptions. {SharedConstants.KnownIssueReported}");
			outputText.AppendLine();
			
		}
	}
}
