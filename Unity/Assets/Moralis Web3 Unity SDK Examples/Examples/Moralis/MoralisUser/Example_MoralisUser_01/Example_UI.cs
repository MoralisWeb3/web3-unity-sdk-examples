using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_MoralisUser_01	
{
	/// <summary>
	/// This class is UI only.
	/// See <see cref="Example_MoralisUser_01"/> for more interesting, core functionality
	/// </summary>
	public class Example_UI : MonoBehaviour
	{
		//  Properties ------------------------------------
		private ExampleButton GetUserButton { get { return _exampleCanvas.Footer.Button03;}}
		
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

			// Mimic user input to populate the UI
			GetUserButton_OnClicked();
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
			_exampleCanvas.Header.TitleText.text = "User Objects";
			_exampleCanvas.Header.ChainsDropdown.IsVisible = false;
			
			// Panels
			await _exampleCanvas.SetMaxTextLinesForTopPanelHeight(5);
			_topBodyText.AppendHeaderLine($"Moralis.GetUserAsync()");
			_topBodyText.AppendBullet(
				$"The user object contains information about the currently logged in user. " +
				$"Upon successful login, it is stored locally until logout");
			
			// Footer
			GetUserButton.IsVisible = true;
			GetUserButton.Text.text = "Get User";
			GetUserButton.Button.onClick.AddListener(GetUserButton_OnClicked);

		}


		private async UniTask RefreshUI()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			_exampleCanvas.TopPanel.BodyText.Text.text = _topBodyText.ToString();
			_exampleCanvas.BottomPanel.BodyText.Text.text = _bottomBodyText.ToString();
			_exampleCanvas.BottomPanel.BodyText.ScrollToTop();

			// Cosmetic delay for UI
			await SharedHelper.TaskDelayWaitForCosmeticEffect();
		}


		//  Event Handlers --------------------------------
		private async void GetUserButton_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"{SharedConstants.Loading}");
			await RefreshUI();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			_bottomBodyText = await Example_MoralisUser_01.Moralis_GetUserAsync(_bottomBodyText);
			
			// Display
			await RefreshUI();
		}
	}
}