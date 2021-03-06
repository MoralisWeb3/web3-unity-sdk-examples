using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_MoralisCloud_01
{
	/// <summary>
	/// This class is UI only.
	/// See <see cref="Example_MoralisCloud_01"/> for more interesting, core functionality
	/// </summary>
	public class Example_UI : MonoBehaviour
	{
		//  Properties ------------------------------------
		private ExampleButton CallMethod01Button { get { return _exampleCanvas.Footer.Button01;}}
		private ExampleButton CallMethod02Button { get { return _exampleCanvas.Footer.Button02;}}
		private ExampleButton OpenUrlButton { get { return _exampleCanvas.Footer.Button03;}}
		
		//  Fields ----------------------------------------
		[SerializeField] 
		private ExampleCanvas _exampleCanvas = null;
		
		[SerializeField] 
		private TextAsset _cloudFunctionsTextAsset = null;
		private readonly StringBuilder _topBodyText = new StringBuilder();
		private readonly StringBuilder _bottomBodyText = new StringBuilder();
		
		//  Unity Methods ---------------------------------
		protected async void Start()
		{
			await SetupMoralis();
			await SetupUI();
			await RefreshUI();
			CallMethod01Button_OnClicked();
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
			_exampleCanvas.Header.TitleText.text = "Cloud Functions";
			_exampleCanvas.Header.ChainsDropdown.IsVisible = false;
			
			// Panels
			await _exampleCanvas.SetMaxTextLinesForTopPanelHeight(18);
			_exampleCanvas.TopPanel.TitleText.text = "Server Side (JS)";
			_exampleCanvas.BottomPanel.TitleText.text = "Client Side Output";
			
			List<string> lines = SharedHelper.ConvertTextAssetToLines(_cloudFunctionsTextAsset, 3);
			_topBodyText.AppendHeaderLine($"Moralis.Cloud.RunAsync<T>(...)");
			_topBodyText.AppendBullet($"Call a method on the Moralis Cloud");
			_topBodyText.AppendHeaderLine($"{SharedConstants.SceneSetupInstructions}");
			_topBodyText.Append("\n" + String.Join("\n", lines));
			
			// Footer
			CallMethod01Button.IsVisible = true;
			CallMethod01Button.Text.text = $"Call myMethod01";
			CallMethod01Button.Button.onClick.AddListener(CallMethod01Button_OnClicked);

			CallMethod02Button.IsVisible = true;
			CallMethod02Button.Text.text = $"Call myMethod02";
			CallMethod02Button.Button.onClick.AddListener(CallMethod02Button_OnClicked);

			OpenUrlButton.IsVisible = true;
			OpenUrlButton.Text.text = $"Open Moralis Cloud";
			OpenUrlButton.Button.onClick.AddListener(OpenUrlButton_OnClicked);
		}
		
		
		private async UniTask RefreshUI()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			_exampleCanvas.TopPanel.BodyText.Text.text = _topBodyText.ToString();
			_exampleCanvas.TopPanel.BodyText.ScrollToTop();
			_exampleCanvas.BottomPanel.BodyText.Text.text = _bottomBodyText.ToString();
			_exampleCanvas.BottomPanel.BodyText.ScrollToTop();

			// Cosmetic delay for UI
			await SharedHelper.TaskDelayWaitForCosmeticEffect();
		}


		//  Event Handlers --------------------------------
		private async void CallMethod01Button_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}

			// Prepare
			_exampleCanvas.IsInteractable(false);
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			string result = await Example_MoralisCloud_01.Moralis_Cloud_RunAsync_01();

			// Display
			_bottomBodyText.Clear();
			_bottomBodyText.AppendHeaderLine ($"myMethod01 ()");
			_bottomBodyText.AppendBullet($"result = '{result}'");
			if (string.IsNullOrEmpty(result))
			{
				_bottomBodyText.AppendErrorLine($"{SharedConstants.CloudFunctionNotFound}");
			}
			
			_exampleCanvas.IsInteractable(true);
			await RefreshUI();
		}
		
		
		private async void CallMethod02Button_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_exampleCanvas.IsInteractable(false);
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			string result = await Example_MoralisCloud_01.Moralis_Cloud_RunAsync_02();

			// Display
			_bottomBodyText.Clear();
			_bottomBodyText.AppendHeaderLine
				($"myMethod02 (10,20)");

			_bottomBodyText.AppendBullet($"result = '{result}'");
			if (string.IsNullOrEmpty(result))
			{
				_bottomBodyText.AppendErrorLine($"{SharedConstants.CloudFunctionNotFound}");
			}

			_exampleCanvas.IsInteractable(true);
			await RefreshUI();
		}
		
		
		private void OpenUrlButton_OnClicked()
		{
			Application.OpenURL(SharedConstants.MoralisServersUrl);
		}
	}
}
