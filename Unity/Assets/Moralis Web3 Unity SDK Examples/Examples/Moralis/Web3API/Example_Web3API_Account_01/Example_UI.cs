using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Web3Api.Models;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Web3API_Account_01
{
	public enum UserOperationType
	{
		Null,
		GetNativeInfoButton,
		GetNFTInfoButton,
		GetTokenInfoButton
	}
	
	/// <summary>
	/// This class is UI only.
	/// See <see cref="Example_Web3API_Account_01"/> for more interesting, core functionality
	/// </summary>
	public class Example_UI : MonoBehaviour
	{
		//  Properties ------------------------------------
		private ExampleButton GetNativeInfoButton { get { return _exampleCanvas.Footer.Button01;}}
		private ExampleButton GetNFTInfoButton { get { return _exampleCanvas.Footer.Button02;}}
		private ExampleButton GetTokenInfoButton { get { return _exampleCanvas.Footer.Button03;}}
		
		//  Fields ----------------------------------------
		[SerializeField] 
		private ExampleCanvas _exampleCanvas = null;
		
		private readonly StringBuilder _topBodyText = new StringBuilder();
		private StringBuilder _bottomBodyText = new StringBuilder();
		private readonly StringBuilder _bottomBodyTextError = new StringBuilder();
		private UserOperationType _lastUserOperationType = UserOperationType.Null;
		
		
		//  Unity Methods ---------------------------------
		protected async void Start()
		{
			await SetupMoralis();
			await SetupUI();
			await RefreshUI();
			
			// Mimic user input to populate the UI
			GetNativeInfoButton_OnClicked();
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
			_exampleCanvas.Header.TitleText.text = "Accounts";
			_exampleCanvas.Header.ChainsDropdown.IsVisible = true;
			_exampleCanvas.Header.ChainsDropdown.OnValueChanged.AddListener(ChainsDropdown_OnValueChanged);
			_exampleCanvas.Header.ChainsDropdown.SetSelectedChain(ChainList.eth);
			
			// Panels
			await _exampleCanvas.SetMaxTextLinesForTopPanelHeight(10);
		
			// Footer
			GetNativeInfoButton.IsVisible = true;
			GetNativeInfoButton.Text.text = $"Get Info (Native)";
			GetNativeInfoButton.Button.onClick.AddListener(GetNativeInfoButton_OnClicked);

			GetNFTInfoButton.IsVisible = true;
			GetNFTInfoButton.Text.text = $"Get Info (NFT)";
			GetNFTInfoButton.Button.onClick.AddListener(GetNFTInfoButton_OnClicked);
			
			GetTokenInfoButton.IsVisible = true;
			GetTokenInfoButton.Text.text = $"Get Info (Token)";
			GetTokenInfoButton.Button.onClick.AddListener(GetTokenInfoButton_OnClicked);
		}

		
		private async UniTask RefreshUI()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			_exampleCanvas.TopPanel.BodyText.Text.text = _topBodyText.ToString();
			if (_bottomBodyTextError.Length == 0)
			{
				_exampleCanvas.BottomPanel.BodyText.Text.text = _bottomBodyText.ToString();
			}
			else
			{
				_exampleCanvas.BottomPanel.BodyText.Text.text = _bottomBodyTextError.ToString();
			}
			_exampleCanvas.BottomPanel.BodyText.ScrollToTop();
			
			// Cosmetic delay for UI
			await SharedHelper.TaskDelayWaitForCosmeticEffect();
		}


		//  Event Handlers --------------------------------
		private async void GetNativeInfoButton_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_lastUserOperationType = UserOperationType.GetNativeInfoButton;
			
			_topBodyText.Clear();
			_topBodyText.AppendHeaderLine($"Web3Api.Account.GetNativeBalance(...)");
			_topBodyText.AppendBullet($"Gets native balance for a specific address");
			_topBodyText.AppendHeaderLine($"Web3Api.Account.GetTransactions(...)");
			_topBodyText.AppendBullet($"Gets native transactions in descending order based on block number");
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"{SharedConstants.Loading}");
			await RefreshUI();
			_bottomBodyText.Clear();
			
			ChainList chainList = _exampleCanvas.Header.ChainsDropdown.GetSelectedChain();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			_bottomBodyText = 
				await Example_Web3API_Account_01.MoralisClient_Web3Api_Account_GetNativeBalance(
					chainList, _bottomBodyText);

			// Display
			await RefreshUI();
		}

		

		private async void GetTokenInfoButton_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_lastUserOperationType = UserOperationType.GetTokenInfoButton;
			
			_topBodyText.Clear();
			_topBodyText.AppendHeaderLine($"Web3Api.Account.GetTokenBalances(...)");
			_topBodyText.AppendBullet($"Gets token balances for a specific address");
			_topBodyText.AppendHeaderLine( $"Web3Api.Account.GetTokenTransfers(...)");
			_topBodyText.AppendBullet($"Gets ERC20 token transactions in descending order based on block number");
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"{SharedConstants.Loading}");
			await RefreshUI();
			_bottomBodyText.Clear();
			
			ChainList chainList = _exampleCanvas.Header.ChainsDropdown.GetSelectedChain();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			_bottomBodyText = 
				await Example_Web3API_Account_01.MoralisClient_Web3Api_Account_GetTokenTransfers(
					chainList, _bottomBodyText);
			
			// Display
			await RefreshUI();
		}
		
		
		private async void GetNFTInfoButton_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_lastUserOperationType = UserOperationType.GetNFTInfoButton;
			
			_topBodyText.Clear();
			_topBodyText.AppendHeaderLine($"Web3Api.Account.GetNFTs(...)");
			_topBodyText.AppendBullet($"Gets NFTs owned by the given address");
			_topBodyText.AppendHeaderLine($"Web3Api.Account.GetNFTTransfers(...)");
			_topBodyText.AppendBullet($"Gets the transfers of the tokens matching the given parameters");
			_topBodyText.AppendHeaderLine($"Web3Api.Account.GetNFTsForContract(...)");
			_topBodyText.AppendBullet($"Gets NFTs owned by the given address");
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"{SharedConstants.Loading}");
			await RefreshUI();
			_bottomBodyText.Clear();
			
			ChainList chainList = _exampleCanvas.Header.ChainsDropdown.GetSelectedChain();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			_bottomBodyText = 
				await Example_Web3API_Account_01.MoralisClient_Web3Api_Account_GetNFTTransfers(
					chainList, _bottomBodyText);
			
			// Display
			await RefreshUI();
		}
		
		
		private async void ChainsDropdown_OnValueChanged(ChainEntry chainEntry)
		{
			// Replay the last button click whenever dropdown changes
			switch (_lastUserOperationType)
			{
				case UserOperationType.GetNativeInfoButton:
					GetNativeInfoButton_OnClicked();
					break;
				case UserOperationType.GetNFTInfoButton:
					GetNFTInfoButton_OnClicked();
					break;
				case UserOperationType.GetTokenInfoButton:
					GetTokenInfoButton_OnClicked();
					break;
				default:
					// Allow default
					break;
			}
			
			await RefreshUI();
		}
	}
}
