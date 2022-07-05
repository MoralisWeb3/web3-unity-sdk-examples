using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_MenuItem_01	
{
	
	/// <summary>
	/// This class is UI only.
	/// See <see cref="Example_MenuItem_01"/> for more interesting, core functionality
	/// </summary>
	public class Example_UI : MonoBehaviour
	{
		//  Properties ------------------------------------
		private ExampleButton SignButton { get { return _exampleCanvas.Footer.Button03;}}
		
		//  Fields ----------------------------------------
		[SerializeField] 
		private ExampleCanvas _exampleCanvas = null;
		
		private readonly StringBuilder _topBodyText = new StringBuilder();
		private readonly StringBuilder _bottomBodyText = new StringBuilder();
		
		
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
			
			// Header
			_exampleCanvas.Header.TitleText.text = "Unity Menu Item";
			_exampleCanvas.Header.ChainsDropdown.IsVisible = false;
			
			// Panels
			await _exampleCanvas.SetMaxTextLinesForTopPanelHeight(10);
			_topBodyText.Clear();
			_topBodyText.AppendHeaderLine($"[MenuItem]");
			_topBodyText.AppendBullet($"Click Menu: Tools ► {Example_MenuItem_01.MenuItemTitle}");
			_topBodyText.AppendHeaderLine($"EditorApplication.ExecuteMenuItem(...)");
			_topBodyText.AppendBullet($"Or click 'Execute Menu Item' below");

			// Footer
			SignButton.IsVisible = true;
			SignButton.Text.text = $"Execute Menu Item";
			SignButton.Button.onClick.AddListener(ExecuteMenuItemButton_OnClicked);
		}
		
		
		private async UniTask RefreshUI()
		{
			if (await ExampleHelper.HasMoralisUser() == false)
			{
				return;
			}

			// Panels
			_exampleCanvas.TopPanel.BodyText.Text.text = _topBodyText.ToString();
			_exampleCanvas.TopPanel.BodyText.ScrollToTop();
			_exampleCanvas.BottomPanel.BodyText.Text.text = _bottomBodyText.ToString();

			// Cosmetic delay for UI
			await ExampleHelper.TaskDelayWaitForCosmeticEffect();
		}

		//  Event Handlers --------------------------------
		private async void ExecuteMenuItemButton_OnClicked()
		{
			if (await ExampleHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			SignButton.IsInteractable = false;
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"{ExampleConstants.Loading}");
			await RefreshUI();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			Example_MenuItem_01.Unity_ExecuteMenuItem();
			
			// Display
			SignButton.IsInteractable = true;
			_bottomBodyText.Clear();
			await RefreshUI();
		}
	}
}
