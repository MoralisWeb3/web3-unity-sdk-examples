using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_SolanaAPI_Account_01
{
	/// <summary>
	/// This class is UI only.
	/// See <see cref="Example_SolanaAPI_Account_01"/> for more interesting, core functionality
	/// </summary>
	public class Example_UI : MonoBehaviour
	{
		//  Properties ------------------------------------
		private ExampleButton GetBalanceButton { get { return _exampleCanvas.Footer.Button03;}}
		private ExampleButton GetNFTsButton { get { return _exampleCanvas.Footer.Button02;}}
		private ExampleButton GetPortfolioButton { get { return _exampleCanvas.Footer.Button01;}}
		
		//  Fields ----------------------------------------
		[SerializeField] 
		private ExampleCanvas _exampleCanvas = null;
		
		//TODO: Change all StringBuilder in all classes to be readonly
		private readonly StringBuilder _topBodyText = new StringBuilder();
		private StringBuilder _bottomBodyText = new StringBuilder();
		private readonly StringBuilder _bottomBodyTextError = new StringBuilder();
		
		
		//  Unity Methods ---------------------------------
		protected virtual async void Start()
		{
			await SetupMoralis();
			await SetupUI();
			await RefreshUI();
			
			// Mimic user input to populate the UI
			GetBalanceButton_OnClicked();
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
			_exampleCanvas.Header.TitleText.text = "Solana Accounts";
			_exampleCanvas.Header.ChainsDropdown.IsVisible = false;
			
			// Panels
			await _exampleCanvas.SetMaxTextLinesForTopPanelHeight(4);
		
			// Footer
			GetBalanceButton.IsVisible = true;
			GetBalanceButton.Text.text = $"Get Balance";
			GetBalanceButton.Button.onClick.AddListener(GetBalanceButton_OnClicked);

			GetNFTsButton.IsVisible = true;
			GetNFTsButton.Text.text = $"Get NFTs";
			GetNFTsButton.Button.onClick.AddListener(GetNFTsButton_OnClicked);

			GetPortfolioButton.IsVisible = true;
			GetPortfolioButton.Text.text = $"Get Portfolio";
			GetPortfolioButton.Button.onClick.AddListener(GetPortfolioButton_OnClicked);
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
		private async void GetBalanceButton_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_topBodyText.Clear();
			_topBodyText.AppendHeaderLine($"SolanaApi.Account.Balance(...)");
			_topBodyText.AppendBullet($"Gets native balance for a specific address");
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"{SharedConstants.Loading}");
			await RefreshUI();
			_bottomBodyText.Clear();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			_bottomBodyText = 
				await Example_SolanaAPI_Account_01.MoralisClient_SolanaApi_Account_Balance(
					_bottomBodyText);

			// Display
			await RefreshUI();
		}

		

		private async void GetPortfolioButton_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_topBodyText.Clear();
			_topBodyText.AppendHeaderLine($"SolanaApi.Account.GetPortfolio(...)");
			_topBodyText.AppendBullet($"Gets portfolio for specific address");
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"{SharedConstants.Loading}");
			await RefreshUI();
			_bottomBodyText.Clear();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			_bottomBodyText = 
				await Example_SolanaAPI_Account_01.MoralisClient_SolanaApi_Account_GetPortfolio(
					_bottomBodyText);
			
			// Display
			await RefreshUI();
		}
		
		
		private async void GetNFTsButton_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_topBodyText.Clear();
			_topBodyText.AppendHeaderLine($"SolanaApi.Account_GetNFTs(...)");
			_topBodyText.AppendBullet($"Gets NFTs owned by the given address");
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"{SharedConstants.Loading}");
			await RefreshUI();
			_bottomBodyText.Clear();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			_bottomBodyText = 
				await Example_SolanaAPI_Account_01.MoralisClient_SolanaApi_Account_GetNFTs(
					_bottomBodyText);
			
			// Display
			await RefreshUI();
		}
	}
}
