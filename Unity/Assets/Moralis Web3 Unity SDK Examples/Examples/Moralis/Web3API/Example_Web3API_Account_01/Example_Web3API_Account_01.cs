using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Examples.Sdk.Shared.Data.Types;
using MoralisUnity.Sdk.Utilities;
using MoralisUnity.Web3Api.Models;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Web3API_Account_01
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
	public class Example_Web3API_Account_01 : Example_UI
	{
		//  Fields ----------------------------------------
		
		/// <summary>
		/// Limit results to conserve text display space
		/// </summary>
		private static readonly LoopLimit LoopLimit = new LoopLimit(3);


		//  General Methods -------------------------------	
		public static async UniTask<StringBuilder> MoralisClient_Web3Api_Account_GetNativeBalance(
			ChainList chainList, StringBuilder outputText)
		{

			// Setup Parameters
			string address = SharedHelper.GetExampleAddressForChainList(chainList);
			string addressFormatted = Formatters.GetWeb3AddressShortFormat(address);
			MoralisClient moralisClient = Moralis.GetClient();
			
			outputText.AppendHeaderLine(
				$"Web3Api.Account.GetNativeBalance({addressFormatted}, {chainList})");

			try
			{
				///////////////////////////////////////////
				// Execute: GetNativeBalance
				///////////////////////////////////////////
				NativeBalance nativeBalance =
					await moralisClient.Web3Api.Account.GetNativeBalance(address, chainList);
				
				// Show Results in UI
				outputText.AppendBullet($"nativeBalance.Balance = {nativeBalance.Balance}");
			}
			catch (Exception exception)
			{
				outputText.AppendBulletException(exception);
			}
			
			outputText.AppendHeaderLine(
				$"Web3Api.Account.GetTransactions({addressFormatted}, {chainList})");

			try
			{
				///////////////////////////////////////////
				// Execute: GetTransactions
				///////////////////////////////////////////
				TransactionCollection transactionCollection = 
					await moralisClient.Web3Api.Account.GetTransactions(address, chainList);
				
				// Show Results in UI
				outputText.AppendBullet($"transactionCollection.Result.Count = {transactionCollection.Result.Count}");

				if (transactionCollection.Result.Count > 0)
                {
					LoopLimit.Reset();
					outputText.AppendBulletLoopLimit(LoopLimit);
					foreach (Transaction transaction in transactionCollection.Result)
					{
						if (LoopLimit.IsAtLimit())
						{
							break;
						}

						outputText.AppendBullet($"transaction.Value = {transaction.Value}", 2);
					}
				}
	
			}
			catch (Exception exception)
			{
				outputText.AppendBulletException(exception);
			}

			return outputText;
		}
   

        public static async UniTask<StringBuilder> MoralisClient_Web3Api_Account_GetTokenTransfers(
			ChainList chainList, StringBuilder outputText)
		{
			// Setup Parameters
			string address = SharedHelper.GetExampleAddressForChainList(chainList);
			string addressFormatted = Formatters.GetWeb3AddressShortFormat(address);
			MoralisClient moralisClient = Moralis.GetClient();

			outputText.AppendHeaderLine(
				$"Web3Api.Account.GetTokenBalances({addressFormatted}, {chainList})");
			
			try
			{
				///////////////////////////////////////////
				// Execute: GetTokenBalances
				///////////////////////////////////////////
				List<Erc20TokenBalance> tokenBalances =
					await moralisClient.Web3Api.Account.GetTokenBalances(address, chainList);
				
				// Show Results in UI
				outputText.AppendBullet($"tokenBalances.Count = {tokenBalances.Count}", 1);

				if (tokenBalances.Count > 0)
				{
					LoopLimit.Reset();
					outputText.AppendBulletLoopLimit(LoopLimit);
					foreach (Erc20TokenBalance tokenBalance in tokenBalances)
					{
						if (LoopLimit.IsAtLimit())
						{
							break;
						}

						outputText.AppendBullet($"tokenBalance = {tokenBalance.Balance}", 2);
					}
				}
			}
			catch (Exception exception)
			{
				outputText.AppendBulletException(exception);
			}

			outputText.AppendHeaderLine(
				$"Web3Api.Account.GetTokenTransfers({addressFormatted}, {chainList})");
			
			try
			{
				///////////////////////////////////////////
				// Execute: GetTokenTransfers
				///////////////////////////////////////////
				Erc20TransactionCollection erc20TransactionCollection =
					await moralisClient.Web3Api.Account.GetTokenTransfers(address, chainList);
				
				// Show Results in UI
				outputText.AppendBullet(
					$"erc20TransactionCollection.Result.Count = {erc20TransactionCollection.Result.Count}", 1);

				if (erc20TransactionCollection.Result.Count > 0)
                {
					LoopLimit.Reset();
					outputText.AppendBulletLoopLimit(LoopLimit);
					foreach (Erc20Transaction erc20Transaction in erc20TransactionCollection.Result)
					{
						if (LoopLimit.IsAtLimit())
						{
							break;
						}

						string ercAddressFormatted = Formatters.GetWeb3AddressShortFormat(erc20Transaction.Address);
						outputText.AppendBullet($"erc20Transaction.Address = {ercAddressFormatted}", 2);
					}
				}
			}
			catch (Exception exception)
			{
				outputText.AppendBulletException(exception);
			}

			return outputText;
		}

		
		public static async UniTask<StringBuilder> MoralisClient_Web3Api_Account_GetNFTTransfers(
			ChainList chainList, StringBuilder outputText)
		{
			// Setup Parameters
			string address = SharedHelper.GetExampleAddressForChainList(chainList);
			string addressFormatted = Formatters.GetWeb3AddressShortFormat(address);
			MoralisClient moralisClient = Moralis.GetClient();

			outputText.AppendHeaderLine(
				$"Web3Api.Account.GetNFTs({addressFormatted}, {chainList})");

			try
			{
				///////////////////////////////////////////
				// Execute: GetNFTs
				///////////////////////////////////////////
				NftOwnerCollection nftOwnerCollection =
					await moralisClient.Web3Api.Account.GetNFTs(address, chainList);
				
				// Show Results in UI
				outputText.AppendBullet(
					$"nftOwnerCollection.Result.Count = {nftOwnerCollection.Result.Count}", 1);

				if (nftOwnerCollection.Result.Count > 0)
                {
					LoopLimit.Reset();
					outputText.AppendBulletLoopLimit(LoopLimit);
					foreach (NftOwner nftOwner in nftOwnerCollection.Result)
					{
						if (LoopLimit.IsAtLimit())
						{
							break;
						}

						outputText.AppendBullet($"nftOwner.Name = {nftOwner.Name}", 2);
					}
				}
			}
			catch (Exception exception)
			{
				outputText.AppendBulletException(exception);
			}

			outputText.AppendHeaderLine(
				$"Web3Api.Account.GetNFTTransfers({addressFormatted}, {chainList})");

			try
			{
				///////////////////////////////////////////
				// Execute: GetNFTTransfers
				///////////////////////////////////////////
				NftTransferCollection nftTransferCollection =
					await moralisClient.Web3Api.Account.GetNFTTransfers(address, chainList);
				
				// Show Results in UI
				outputText.AppendBullet(
					$"nftTransferCollection.Result.Count = {nftTransferCollection.Result.Count}", 1);

				if (nftTransferCollection.Result.Count > 0)
				{
					LoopLimit.Reset();
					outputText.AppendBulletLoopLimit(LoopLimit);
					foreach (NftTransfer nftTransfer in nftTransferCollection.Result)
					{
						if (LoopLimit.IsAtLimit())
						{
							break;
						}

						outputText.AppendBullet($"nftTransfer.Amount = {nftTransfer.Amount}", 2);
					}
				}
			}
			catch (Exception exception)
			{
				outputText.AppendBulletException(exception);
			}

			string tokenAddress = SharedHelper.GetExampleTokenAddressForChainList(chainList);
			string tokenAddressFormatted = Formatters.GetWeb3AddressShortFormat(tokenAddress);
			outputText.AppendHeaderLine(
				$"Web3Api.Account.GetNFTsForContract({addressFormatted}, {tokenAddressFormatted}, {chainList})");

			try
			{
				///////////////////////////////////////////
				// Execute: GetNFTsForContract
				///////////////////////////////////////////
				NftOwnerCollection nftOwnerCollection =
					await moralisClient.Web3Api.Account.GetNFTsForContract(address, tokenAddress, chainList);

				// Show Results in UI
				outputText.AppendBullet(
					$"nftOwnerCollection.Result.Count = {nftOwnerCollection.Result.Count}", 1);

				if (nftOwnerCollection.Result.Count > 0)
				{
					LoopLimit.Reset();
					outputText.AppendBulletLoopLimit(LoopLimit);
					foreach (NftOwner nftOwner in nftOwnerCollection.Result)
					{
						if (LoopLimit.IsAtLimit())
						{
							break;
						}

						outputText.AppendBullet($"nftOwner.Amount = {nftOwner.Amount}", 2);
					}
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
