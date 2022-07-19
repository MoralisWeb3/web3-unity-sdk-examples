using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Examples.Sdk.Shared.Data.Types;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Query_LiveQuery_01	
{
	/// <summary>
	/// This class is UI only.
	/// See <see cref="Example_Query_LiveQuery_01"/> for more interesting, core functionality
	/// </summary>
	public class Example_UI : MonoBehaviour
	{
		//  Properties ------------------------------------
		private ExampleButton QueryHeroButton { get { return _exampleCanvas.Footer.Button01;}}
		private ExampleButton CreateHeroButton { get { return _exampleCanvas.Footer.Button02;}}
		private ExampleButton DeleteHeroButton { get { return _exampleCanvas.Footer.Button03;}}
		protected ExampleCanvas ExampleCanvas { get { return _exampleCanvas;}}
		
		//  Fields ----------------------------------------
		[SerializeField] 
		private ExampleCanvas _exampleCanvas = null;
		
		private readonly StringBuilder _topBodyText = new StringBuilder();
		private StringBuilder _bottomBodyText = new StringBuilder();
		
		
		//  Unity Methods ---------------------------------
		protected virtual async void Start()
		{
			await SetupMoralis();
			await SetupUI();
			await RefreshUI();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			await Example_Query_LiveQuery_01.MoralisLiveQueryController_AddSubscription();
			
			// Mimic user input to populate the UI
			QueryHeroButton_OnClicked();
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
			_exampleCanvas.Header.TitleText.text = "Live Query";
			_exampleCanvas.Header.ChainsDropdown.IsVisible = false;
			
			// Panels
			await _exampleCanvas.SetMaxTextLinesForTopPanelHeight(9);
			_topBodyText.Clear();
			_topBodyText.AppendHeaderLine($"MoralisClient.Create<Hero>()");
			_topBodyText.AppendBullet(
				$"Creating your own objects to support NPCs, characters, and game objects is as " +
				$"simple as creating a Plain Old C# Object (POCO)");
			
			_topBodyText.AppendHeaderLine($"MoralisClient.Query<Hero>()");
			_topBodyText.AppendBullet(
				$"Queries provide a way to retrieve information from your Moralis database");
			_topBodyText.AppendLine();
			
			// Footer
			CreateHeroButton.IsVisible = true;
			CreateHeroButton.Text.text = $"Create Hero";
			CreateHeroButton.Button.onClick.AddListener(CreateHeroButton_OnClicked);

			DeleteHeroButton.IsVisible = true;
			DeleteHeroButton.Text.text = $"Delete Hero";
			DeleteHeroButton.Button.onClick.AddListener(DeleteHeroButton_OnClicked);
			
			QueryHeroButton.IsVisible = true;
			QueryHeroButton.Text.text = $"Query Hero";
			QueryHeroButton.Button.onClick.AddListener(QueryHeroButton_OnClicked);
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
			_exampleCanvas.BottomPanel.BodyText.ScrollToTop();
			
			// Cosmetic delay for UI
			await SharedHelper.TaskDelayWaitForCosmeticEffect();
		}

		
		//  Event Handlers --------------------------------
		private async void CreateHeroButton_OnClicked()
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
			Hero heroCreated = await Example_Query_LiveQuery_01.Moralis_Create(_bottomBodyText);
			
			// Display
			_bottomBodyText.Clear();
			_bottomBodyText.AppendHeaderLine($"moralisClient.Create<Hero>()");
			_bottomBodyText.AppendBullet($"{heroCreated}");
			await RefreshUI();
		}
		
		
		private async void DeleteHeroButton_OnClicked()
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
			List<Hero> results = await Example_Query_LiveQuery_01.Moralis_Query(_bottomBodyText);

			Hero heroToDelete = null;
			if (results.Count > 0)
			{
				heroToDelete = results[0];
			}
			
			// Display
			_bottomBodyText.Clear();
			_bottomBodyText.AppendHeaderLine($"MoralisClient.DeleteAsync<Hero>()");
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			if (heroToDelete != null)
			{
				_bottomBodyText = 
					await Example_Query_LiveQuery_01.Moralis_Delete(_bottomBodyText, heroToDelete);
			}
			else
			{
				_bottomBodyText.AppendBullet($"{SharedConstants.NothingAvailable}");
			}

			await RefreshUI();
		}
		
		
		private async void QueryHeroButton_OnClicked()
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
			List<Hero> results = await Example_Query_LiveQuery_01.Moralis_Query(_bottomBodyText);

			// Display
			_bottomBodyText.Clear();
			_bottomBodyText.AppendHeaderLine($"moralisClient.Query<Hero>()");

			if (results.Count > 0)
			{
				foreach (Hero hero in results)
				{
					_bottomBodyText.AppendBullet($"{hero}");
				}
			}
			else
			{
				_bottomBodyText.AppendBullet($"{SharedConstants.NothingAvailable}");
			}
			
			await RefreshUI();
		}
	}
}
