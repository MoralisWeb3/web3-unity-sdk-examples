using System;
using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Sdk.Utilities;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Web3Client_SendTransactionAsync_01	
{

	/// <summary>
	/// This class is UI only.
	/// See <see cref="Example_Web3API_RunContractFunction_01"/> for more interesting, core functionality
	/// </summary>
	public class Example_UI : MonoBehaviour
	{
		//  Properties ------------------------------------
		private ExampleButton TransferButton
		{
			get { return _exampleCanvas.Footer.Button03; }
		}

		//  Fields ----------------------------------------
		[SerializeField] private ExampleCanvas _exampleCanvas = null;

		private readonly StringBuilder _topBodyText = new StringBuilder();
		private StringBuilder _bottomBodyText = new StringBuilder();


		//  Unity Methods ---------------------------------
		protected async void Start()
		{
			await SetupMoralis();
			await SetupUI();
			await RefreshUI();

			// Mimic user input to populate the UI
			TransferButton_OnClicked();
		}


		//  General Methods -------------------------------	
		private async UniTask SetupMoralis()
		{
			Moralis.Start();
		}


		private async UniTask SetupUI()
		{
			// Canvas
			await _exampleCanvas.InitializeAsync();

			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}

			// Header
			_exampleCanvas.Header.TitleText.text = "Send Transaction";
			_exampleCanvas.Header.ChainsDropdown.IsVisible = false;
			_exampleCanvas.Header.AuthenticationUI.OnActiveAddressChanged.AddListener(
				AuthenticationUI_OnActiveAddressChanged);

			// Panels
			await _exampleCanvas.SetMaxTextLinesForTopPanelHeight(5);
			_topBodyText.Clear();
			_topBodyText.AppendHeaderLine(
				$"Moralis.Web3Client.Eth.TransactionManager.SendTransactionAsync(...)");
			_topBodyText.AppendBullet(
				$"Transfer some value of balance from one address to another address");

			// Footer
			TransferButton.IsVisible = true;
			TransferButton.Text.text = $"Transfer";
			TransferButton.Button.onClick.AddListener(TransferButton_OnClicked);
		}


		private async UniTask RefreshUI()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}

			// Panels
			_exampleCanvas.TopPanel.BodyText.Text.text = _topBodyText.ToString();
			_exampleCanvas.TopPanel.BodyText.ScrollToTop();
			_exampleCanvas.BottomPanel.BodyText.Text.text = _bottomBodyText.ToString();

			// Cosmetic delay for UI
			await SharedHelper.TaskDelayWaitForCosmeticEffect();
		}




		//  Event Handlers --------------------------------
		private void AuthenticationUI_OnActiveAddressChanged(string address)
		{
			TransferButton_OnClicked();
		}

		private async void TransferButton_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}

			// Prepare
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"{SharedConstants.Loading}");
			await RefreshUI();
			_bottomBodyText.Clear();

			// Logging ignores the live parameter value to save space
			_bottomBodyText.AppendHeaderLine(
				$"Moralis.Web3Client.Eth.TransactionManager.SendTransactionAsync(...)");

			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			_bottomBodyText =
				await Example_Web3Client_SendTransactionAsync_01.Web3Client_Eth_TransactionManager_SendTransactionAsync(
					_bottomBodyText);


			// Display
			await RefreshUI();
		}
		
		/// <summary>
		/// Do series of checks and build error logs
		/// </summary>
		public static bool IsValidTransactionInput (
			ref StringBuilder outputText, TransactionInput transactionInput)
		{
			bool isValid1 = transactionInput.Value.Value > 0;
			if (!isValid1)
			{
				outputText.AppendBulletError($"Failed. Invalid parameter. amountToSendWei = {transactionInput.Value}");
			}
		
			bool isValid2 = Validators.IsValidWeb3AddressFormat(transactionInput.From);
			if (!isValid2)
			{
				outputText.AppendBulletError($"Failed. Invalid parameter. fromAddress = {transactionInput.From}");
			}

			bool isValid3 = Validators.IsValidWeb3AddressFormat(transactionInput.To);
			if (!isValid3)
			{
				outputText.AppendBulletError($"Failed. Invalid parameter. toAddress = {transactionInput.To}");
			}

			bool isValid4 = transactionInput.From != transactionInput.To;
			if (!isValid4)
			{
				outputText.AppendBulletError(
					$"Failed. fromAddress must not equal toAddress. \n\nTo fix, hardcode new value(s) in {nameof(Example_Web3Client_SendTransactionAsync_01)}.cs");
			}

			return isValid1 && isValid2 && isValid3 && isValid4;
		}
	}
}
