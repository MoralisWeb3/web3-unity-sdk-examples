using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
	/// <summary>
	/// This class is UI only.
	/// See <see cref="Example_Filecoin_Storage_01"/> for more interesting, core functionality
	/// </summary>
	public class Example_UI : MonoBehaviour
	{
		//  Properties ------------------------------------
		private ExampleButton OpenImageButton { get { return _exampleCanvas.Footer.Button01;}}
		private ExampleButton SaveImageButton { get { return _exampleCanvas.Footer.Button02;}}
		private ExampleButton ClearImageButton { get { return _exampleCanvas.Footer.Button03;}}
		
		//  Fields ----------------------------------------
		[SerializeField] 
		private ExampleCanvas _exampleCanvas = null;
		
		[SerializeField] 
		private ExamplePanel _panelForSpriteDestination = null;

		[SerializeField] 
		private Sprite _spriteToSave = null;
		
		private readonly StringBuilder _topBodyText = new StringBuilder();
		private StringBuilder _bottomBodyText = new StringBuilder();
		private StringBuilder _bottomBodyTextError = new StringBuilder();
		private UploadAndGetFileData _lastUploadAndGetFileData = null;
		private Sprite _spriteDestination = null;
		private Image _imageDestination = null;
		
		// When we 'clear' the image we actually upload a small white one
		// This value is used to determine "did we just clear or save a REAL image?"
		private const int MaxBytesLengthForClearedImage = 10;
		
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
			_exampleCanvas.Header.TitleText.text = "Filecoin";
			_exampleCanvas.Header.ChainsDropdown.IsVisible = false;
			
			// Panels
			await _exampleCanvas.SetMaxTextLinesForTopPanelHeight(14);
			_bottomBodyText.Clear();
			_topBodyText.Clear();
			_topBodyText.AppendHeaderLine($"FilecoinWeb3Storage.UploadFile(...)");
			_topBodyText.AppendBullet($"Upload and store a file");
			
			_topBodyText.AppendHeaderLine($"FilecoinWeb3Storage.GetStatus(...)");
			_topBodyText.AppendBullet($"Get status of stored file");
			
			_topBodyText.AppendHeaderLine($"FilecoinWeb3Storage.GetFile(...)");
			_topBodyText.AppendBullet($"Download a stored file");
			_topBodyText.AppendLine();
			
			// Dynamically add, for the to-be-loaded Image
			_imageDestination = SharedHelper.CreateNewImageUnderParentAsLastSibling(
				_panelForSpriteDestination.transform.parent, new Vector2(400, 400));
			_imageDestination.GetComponent<CanvasGroup>().SetIsVisible(false);
			
			// Footer
			OpenImageButton.IsVisible = true;
			OpenImageButton.Text.text = $"Open Image\n(Browser)";
			OpenImageButton.Button.onClick.AddListener(OpenImageButton_OnClicked);

			SaveImageButton.IsVisible = true;
			SaveImageButton.Text.text = $"Save Image\n({_spriteToSave.name})";
			SaveImageButton.Button.onClick.AddListener(SaveImageButton_OnClicked);

			ClearImageButton.IsVisible = true;
			ClearImageButton.Text.text = $"Clear Image\n";
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

			OpenImageButton.IsInteractable = _lastUploadAndGetFileData != null 
			                                 && _lastUploadAndGetFileData.BytesLength > MaxBytesLengthForClearedImage
			                                 && !string.IsNullOrEmpty(_lastUploadAndGetFileData.Url);
			
			// Cosmetic delay for UI
			await SharedHelper.TaskDelayWaitForCosmeticEffect();
		}


		private async UniTask CallUploadFolder(byte[] content)
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
			UploadAndGetFileData uploadAndGetFileData =
				await Example_Filecoin_Storage_01.Filecoin_UploadAndGetFile(
					content,
					_bottomBodyText,
					_bottomBodyTextError);

			if (uploadAndGetFileData == null)
			{
				return;
			}

			_lastUploadAndGetFileData = uploadAndGetFileData;
			_bottomBodyText = _lastUploadAndGetFileData.OutputText;
			_bottomBodyTextError = _lastUploadAndGetFileData.ErrorText;
			
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
			
			if (_lastUploadAndGetFileData.Sprite != null)
			{
				_spriteDestination = _lastUploadAndGetFileData.Sprite;

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
			_bottomBodyText.AppendHeaderLine($"Clearing...");
			await RefreshUI();
			
			//The empty string will create an empty image
			byte[] content = new byte[1];
			
			await CallUploadFolder(content);
			RenderImage();
						
			// Display
			_bottomBodyText.AppendBullet($"Success!");
			await RefreshUI();
		}

		private async void OpenImageButton_OnClicked()
		{
			Application.OpenURL(_lastUploadAndGetFileData.Url);
			
			// Display
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
			_bottomBodyText.AppendHeaderLine($"Saving...");
			await RefreshUI();
			
			byte[] content = SharedHelper.ConvertSpriteToContentBytes(_spriteToSave);
			
			await CallUploadFolder(content);
			RenderImage();
			
			// Display
			_bottomBodyText.AppendBullet($"Success!");
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
			_bottomBodyText.AppendHeaderLine($"Loading...");
			await RefreshUI();
			
			RenderImage();
			
			// Display
			_bottomBodyText.Clear();
			_bottomBodyText.AppendHeaderLine($"Loaded Image. Success!");

			if (_spriteDestination == null)
			{
				_bottomBodyText.AppendBullet($"{SharedConstants.NothingAvailable}");
			}
			else
			{
				_bottomBodyText.AppendBullet($"Sprite = {_lastUploadAndGetFileData.Sprite}");
				_bottomBodyText.AppendBullet($"Url = {_lastUploadAndGetFileData.Url}");

				// if (_lastLoadedSprite.Count >= 0 && _lastLoadedSprite[0].Path.Length > 0)
				// {
				// 	_bottomBodyText.AppendBullet($"Path = {_lastLoadedSprite[0].Path}");
				// }
			}
			await RefreshUI();
		}
	}
}
