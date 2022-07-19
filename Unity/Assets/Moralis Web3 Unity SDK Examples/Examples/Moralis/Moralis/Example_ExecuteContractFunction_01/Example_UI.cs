using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Examples.Sdk.Shared.Data.Types;
using MoralisUnity.Web3Api.Models;
using UnityEngine;

#pragma warning disable CS1998, CS4014
namespace MoralisUnity.Examples.Sdk.Example_ExecuteContractFunction_01
{
	/// <summary>
	/// This class is UI only.
	/// See <see cref="Example_Web3API_RunContractFunction_01"/> for more interesting, core functionality
	/// </summary>
	public class Example_UI : MonoBehaviour
	{
		//  Properties ------------------------------------
		private ExampleButton ExecuteContractFunctionGetButton { get { return _exampleCanvas.Footer.Button02;}}
		private ExampleButton ExecuteContractFunctionSetButton { get { return _exampleCanvas.Footer.Button03;}}
		
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
			_topBodyText.AppendHeaderLine($"Moralis.ExecuteContractFunction(...)");
			_topBodyText.AppendBullet($"Executes a given function of a contract abi for read/write operations");
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"Click a button below.");

			// Header
			_exampleCanvas.Header.TitleText.text = "Execute Contract"; 
			_exampleCanvas.Header.ChainsDropdown.IsVisible = true;
			_exampleCanvas.Header.ChainsDropdown.OnValueChanged.AddListener(ChainsDropdown_OnValueChanged);
			_exampleCanvas.Header.ChainsDropdown.SetSelectedChain(GreeterContractData.ChainListRequired);
			
			// Panels
			await _exampleCanvas.SetMaxTextLinesForTopPanelHeight(5);
		
			// Footer
			ExecuteContractFunctionGetButton.IsVisible = true;
			ExecuteContractFunctionGetButton.Text.text = $"Execute\ngetGreeting ()";
			ExecuteContractFunctionGetButton.Button.onClick.AddListener(ExecuteContractFunctionGetButton_OnClicked);
			
			ExecuteContractFunctionSetButton.IsVisible = true;
			ExecuteContractFunctionSetButton.Text.text = $"Execute\nsetGreeting ()";
			ExecuteContractFunctionSetButton.Button.onClick.AddListener(ExecuteContractFunctionSetButton_OnClicked);
		}

		
		private async UniTask RefreshUI()
		{
			if (await ExampleHelper.HasMoralisUser() == false)
			{
				return;
			}
			

			ExecuteContractFunctionSetButton.IsInteractable = true;
			_exampleCanvas.TopPanel.BodyText.Text.text = _topBodyText.ToString();
			_exampleCanvas.BottomPanel.BodyText.Text.text = _bottomBodyText.ToString();
			_exampleCanvas.BottomPanel.BodyText.ScrollToTop();

			// Cosmetic delay for UI
			await ExampleHelper.TaskDelayWaitForCosmeticEffect();
		}
		
		/// <summary>
		/// This is called from within example methods
		/// to refresh more often than normal for better
		/// user-experience
		/// </summary>
		private void HotRefreshUI(string message)
		{
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine(message);
			RefreshUI();
		}


		//  Event Handlers --------------------------------
		private async void ExecuteContractFunctionGetButton_OnClicked()
		{
			if (await ExampleHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_bottomBodyText.Clear();
			ChainList chainList = _exampleCanvas.Header.ChainsDropdown.GetSelectedChain();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			_bottomBodyText =
				await Example_ExecuteContractFunction_01.
					Moralis_ExecuteContractFunction_Get(chainList,
						_bottomBodyText, HotRefreshUI);
			
			
			// Display
			await RefreshUI();
		}
		
		private async void ExecuteContractFunctionSetButton_OnClicked()
		{
			if (await ExampleHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_bottomBodyText.Clear();
			ChainList chainList = _exampleCanvas.Header.ChainsDropdown.GetSelectedChain();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			_bottomBodyText =
				await Example_ExecuteContractFunction_01.
					Moralis_ExecuteContractFunction_Set(chainList,
					_bottomBodyText, HotRefreshUI);
			
			
			// Display
			await RefreshUI();
		}
		
		private async void ChainsDropdown_OnValueChanged(ChainEntry chainEntry)
		{
			await RefreshUI();
		}
	}
}
