using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Web3Api.Models;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Web3API_Storage_01
{
	/// <summary>
	/// This class is UI only.
	/// See <see cref="Example_Web3API_Storage_01"/> for more interesting, core functionality
	/// </summary>
	public class Example_UI : MonoBehaviour
	{
		//  Properties ------------------------------------
		private ExampleButton LoadImageButton { get { return _exampleCanvas.Footer.Button01;}}
		private ExampleButton SaveImageButton { get { return _exampleCanvas.Footer.Button02;}}
		private ExampleButton ClearImageButton { get { return _exampleCanvas.Footer.Button03;}}
		
		//  Fields ----------------------------------------
		[SerializeField] 
		private ExampleCanvas _exampleCanvas = null;
		
		[SerializeField] 
		private ExamplePanel _panelForSpriteDestination = null;

		[SerializeField] 
		private Sprite _spriteToSave = null;
		
		private Sprite _spriteDestination = null;
		private List<IpfsFile> _lastLoadedIpfsFiles = new List<IpfsFile>();
		private Image _imageDestination = null;
		private readonly StringBuilder _topBodyText = new StringBuilder();
		private StringBuilder _bottomBodyText = new StringBuilder();
		private StringBuilder _bottomBodyTextError = new StringBuilder();
		
		//  Unity Methods ---------------------------------
		protected async void Start()
		{
			await SetupMoralis();
			await SetupUI();
			await RefreshUI();

			// Mimic user input to populate the UI
			SaveImageButton_OnClicked();
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
			_exampleCanvas.Header.TitleText.text = "Storage";
			_exampleCanvas.Header.ChainsDropdown.IsVisible = false;
			
			// Panels
			await _exampleCanvas.SetMaxTextLinesForTopPanelHeight(6);
			_bottomBodyText.Clear();
			_topBodyText.Clear();
			_topBodyText.AppendHeaderLine($"Web3Api.Storage.UploadFolder(...)");
			_topBodyText.AppendBullet($"Uploads multiple files and place them in a folder directory.");
			
			// Dynamically add, for the to-be-loaded Image
			_imageDestination = SharedHelper.CreateNewImageUnderParentAsLastSibling(
				_panelForSpriteDestination.transform.parent, new Vector2(400, 400));
			_imageDestination.GetComponent<CanvasGroup>().SetIsVisible(false);
			
			// Footer
			LoadImageButton.IsVisible = true;
			LoadImageButton.Text.text = $"Load Image\n(Ipfs)";
			LoadImageButton.Button.onClick.AddListener(LoadImageButton_OnClicked);

			SaveImageButton.IsVisible = true;
			SaveImageButton.Text.text = $"Save Image\n(Ipfs)";
			SaveImageButton.Button.onClick.AddListener(SaveImageButton_OnClicked);

			ClearImageButton.IsVisible = true;
			ClearImageButton.Text.text = $"Clear Image\n(Ipfs)";
			ClearImageButton.Button.onClick.AddListener(ClearImageButton_OnClicked);
		}

		
		protected async UniTask RefreshUI()
		{
			// This sometimes is called DURING OnDestroy
			// so return instead of throwing an error
			if (await SharedHelper.HasMoralisUser() == false || 
			    _exampleCanvas == null)
			{
				return;
			}
			
			// Text
			_exampleCanvas.TopPanel.BodyText.Text.text = _topBodyText.ToString();
			if (_bottomBodyTextError.Length == 0)
			{
				_exampleCanvas.BottomPanel.BodyText.Text.text = _bottomBodyText.ToString();
			}
			else
			{
				_exampleCanvas.BottomPanel.BodyText.Text.text = _bottomBodyTextError.ToString();
			}
			
			// Cosmetic delay for UI
			await SharedHelper.TaskDelayWaitForCosmeticEffect();
		}


		private async UniTask CallUploadFolder(string content)
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Cosmetic delay for UI
			_exampleCanvas.IsInteractable(false);
			await SharedHelper.TaskDelayWaitForCosmeticEffect();
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
			ExampleResponse exampleResponse =
				await Example_Web3API_Storage_01.MoralisClient_Web3Api_Storage_UploadFolder(
					content,
					_bottomBodyText,
					_bottomBodyTextError);

			_bottomBodyText = exampleResponse.OutputText;
			_bottomBodyTextError = exampleResponse.ErrorText;
			_lastLoadedIpfsFiles = exampleResponse.LastLoadedIpfsFiles;
			
			// Cosmetic delay for UI
			_exampleCanvas.IsInteractable(true);
			await SharedHelper.TaskDelayWaitForCosmeticEffect();
			
		}
		
		private async void RenderImage()
		{
			// This sometimes is called DURING OnDestroy
			// so return instead of throwing an error
			if (await SharedHelper.HasMoralisUser() == false || 
			    _exampleCanvas == null)
			{
				return;
			}
			
			if (_lastLoadedIpfsFiles.Count >= 1 && _lastLoadedIpfsFiles[0].Path.Length > 0)
			{
				_spriteDestination = await SharedHelper.CreateSpriteFromImageUrl(_lastLoadedIpfsFiles[0].Path);

				// Nullcheck for smooth OnDestroy
				if (_imageDestination != null)
				{
					_imageDestination.sprite = _spriteDestination;
					_imageDestination.GetComponent<CanvasGroup>().SetIsVisible(true);
				}
				await RefreshUI();
			}
		}
		
		
		//  Event Handlers --------------------------------
		private async void ClearImageButton_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_imageDestination.sprite = null;
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"Clearing...");
			await RefreshUI();
			
			//The empty string will create an empty image
			string content = ""; 
			
			await CallUploadFolder(content);
			RenderImage();
						
			// Display
			_bottomBodyText.AppendHeaderLine($"Success!");
			await RefreshUI();
		}

		
		private async void SaveImageButton_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_imageDestination.sprite = null;
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"Saving...");
			await RefreshUI();
			
			string content = SharedHelper.ConvertSpriteToContentString(_spriteToSave);
			await CallUploadFolder(content);
			RenderImage();
			
			// Display
			_bottomBodyText.AppendHeaderLine($"Success!");
			await RefreshUI();
		}



		private async void LoadImageButton_OnClicked()
		{
			if (await SharedHelper.HasMoralisUser() == false)
			{
				return;
			}
			
			// Prepare
			_imageDestination.sprite = null;
			_bottomBodyText.Clear();
			_bottomBodyText.AppendLine($"Loading...");
			await RefreshUI();
			
			RenderImage();
			
			// Display
			_bottomBodyText.Clear();
			_bottomBodyText.AppendHeaderLine($"Load Image");

			if (_spriteDestination == null)
			{
				_bottomBodyText.AppendBullet($"{SharedConstants.NothingAvailable}");
			}
			else
			{
				_bottomBodyText.AppendBullet($"Image loaded and displayed below this text");

				if (_lastLoadedIpfsFiles.Count >= 0 && _lastLoadedIpfsFiles[0].Path.Length > 0)
				{
					_bottomBodyText.AppendBullet($"Path = {_lastLoadedIpfsFiles[0].Path}");
				}
			}
			await RefreshUI();
		}
	}
}
