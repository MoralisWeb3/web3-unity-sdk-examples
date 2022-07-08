using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Web3Api.Models;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Web3API_RunContractFunction_01
{
	/// <summary>
	/// This class is UI only.
	/// See <see cref="Example_Web3API_RunContractFunction_01"/> for more interesting, core functionality
	/// </summary>
	public class Example_UI : MonoBehaviour
	{
		//  Properties ------------------------------------
		private ExampleButton RunContractFunctionButton { get { return _exampleCanvas.Footer.Button03;}}
		
		//  Fields ----------------------------------------
		[SerializeField] 
		private ExampleCanvas _exampleCanvas = null;
		
		private readonly StringBuilder _topBodyText = new StringBuilder();
		private StringBuilder _bottomBodyText = new StringBuilder();

		
		//  Unity Methods ---------------------------------
		protected async void Start()
		{
			await SetupMoralis();
			await SetupUI();
			await RefreshUI();
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

			if (await ExampleHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Text
			_topBodyText.Clear();
			_topBodyText.AppendHeaderLine($"Web3Api.Native.RunContractFunction(...)");
			_topBodyText.AppendBullet($"Executes a given function of a contract abi for read/write operations");
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"Click a button below.");
			
			// Header
			_exampleCanvas.Header.TitleText.text = "Run Contract"; 
			_exampleCanvas.Header.ChainsDropdown.IsVisible = true;
			_exampleCanvas.Header.ChainsDropdown.OnValueChanged.AddListener(ChainsDropdown_OnValueChanged);
			_exampleCanvas.Header.ChainsDropdown.SetSelectedChain(GreeterContractData.ChainListRequired);
			
			// Panels
			await _exampleCanvas.SetMaxTextLinesForTopPanelHeight(5);
		
			// Footer
			RunContractFunctionButton.IsVisible = true;
			RunContractFunctionButton.Text.text = $"Run Contract Function";
			RunContractFunctionButton.Button.onClick.AddListener(RunContractFunctionButton_OnClicked);
		}

		
		private async UniTask RefreshUI()
		{
			if (await ExampleHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			_exampleCanvas.TopPanel.BodyText.Text.text = _topBodyText.ToString();
			_exampleCanvas.BottomPanel.BodyText.Text.text = _bottomBodyText.ToString();
			_exampleCanvas.BottomPanel.BodyText.ScrollToTop();

			// Cosmetic delay for UI
			await ExampleHelper.TaskDelayWaitForCosmeticEffect();
		}


		//  Event Handlers --------------------------------
		private async void RunContractFunctionButton_OnClicked()
		{
			if (await ExampleHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"{ExampleConstants.Loading}");
			await RefreshUI();
			_bottomBodyText.Clear();
			
			ChainList chainList = _exampleCanvas.Header.ChainsDropdown.GetSelectedChain();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			_bottomBodyText =
				await Example_Web3API_RunContractFunction_01.
					MoralisClient_Web3Api_Native_RunContractFunction(chainList,
					_bottomBodyText);
			
			
			// Display
			await RefreshUI();
		}
		
		private async void ChainsDropdown_OnValueChanged(ChainEntry chainEntry)
		{
			await RefreshUI();
		}
	}
}
