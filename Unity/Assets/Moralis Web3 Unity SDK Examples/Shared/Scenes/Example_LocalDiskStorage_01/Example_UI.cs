using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_LocalDiskStorage_01
{
	/// <summary>
	/// This class is UI only.
	/// See <see cref="Example_SolanaAPI_Account_01"/> for more interesting, core functionality
	/// </summary>
	public class Example_UI : MonoBehaviour
	{
		//  Properties ------------------------------------
		private ExampleButton LoadButton { get { return _exampleCanvas.Footer.Button03;}}
		private ExampleButton IncrementButton { get { return _exampleCanvas.Footer.Button02;}}
		private ExampleButton SaveButton { get { return _exampleCanvas.Footer.Button01;}}
		
		//  Fields ----------------------------------------
		[SerializeField] 
		private ExampleCanvas _exampleCanvas = null;
		
		private readonly StringBuilder _topBodyText = new StringBuilder();
		private StringBuilder _bottomBodyText = new StringBuilder();
		private SampleData _sampleData = new SampleData();
		
		//  Unity Methods ---------------------------------
		protected virtual async void Start()
		{
			await SetupMoralis();
			await SetupUI();
			await RefreshUI();

			// Mimic user input to populate the UI
			LoadButton_OnClicked();
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
			_exampleCanvas.Header.TitleText.text = "Local Disk Storage";
			_exampleCanvas.Header.ChainsDropdown.IsVisible = false;
			
			// Panels
			await _exampleCanvas.SetMaxTextLinesForTopPanelHeight(10);
			
			_topBodyText.Clear();
			_topBodyText.AppendHeaderLine($"LocalDiskStorage.Save<T>(...)");
			_topBodyText.AppendBullet($"Save object to disk");
			_topBodyText.AppendHeaderLine($"_sampleData.ClickCount++");
			_topBodyText.AppendBullet($"Increment value without saving to disk");
			_topBodyText.AppendHeaderLine($"LocalDiskStorage.Load<T>()");
			_topBodyText.AppendBullet($"Load object from disk");
		
			// Footer
			LoadButton.IsVisible = true;
			LoadButton.Text.text = $"Load";
			LoadButton.Button.onClick.AddListener(LoadButton_OnClicked);

			IncrementButton.IsVisible = true;
			IncrementButton.Text.text = $"Increment";
			IncrementButton.Button.onClick.AddListener(IncrementButton_OnClicked);

			SaveButton.IsVisible = true;
			SaveButton.Text.text = $"Save";
			SaveButton.Button.onClick.AddListener(SaveButton_OnClicked);
		}

		
		private async UniTask RefreshUI()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Update
			_bottomBodyText.AppendHeaderLine($"Result");
			_bottomBodyText.AppendBullet($"sampleData.ClickCount = {_sampleData.ClickCount}");
			
			// Render
			_exampleCanvas.TopPanel.BodyText.Text.text = _topBodyText.ToString();
			_exampleCanvas.BottomPanel.BodyText.Text.text = _bottomBodyText.ToString();
			_exampleCanvas.BottomPanel.BodyText.ScrollToTop();
			
			// Cosmetic delay for UI
			await SharedHelper.TaskDelayWaitForCosmeticEffect();
		}


		//  Event Handlers --------------------------------
		private async void LoadButton_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_bottomBodyText.Clear();
			await RefreshUI();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			_sampleData = Example_LocalDiskStorage_01.LocalDiskStorage_Load(ref _bottomBodyText);

			// Display
			await RefreshUI();
		}

		private async void IncrementButton_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_bottomBodyText.Clear();
			await RefreshUI();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			Example_LocalDiskStorage_01.LocalDiskStorage_Increment(_sampleData, ref _bottomBodyText);
			
			// Display
			await RefreshUI();
		}

		private async void SaveButton_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_bottomBodyText.Clear();
			await RefreshUI();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			bool isSuccess = Example_LocalDiskStorage_01.LocalDiskStorage_Save(_sampleData, ref _bottomBodyText);
			
			// Display
			await RefreshUI();
		}
		
		

	}
}
