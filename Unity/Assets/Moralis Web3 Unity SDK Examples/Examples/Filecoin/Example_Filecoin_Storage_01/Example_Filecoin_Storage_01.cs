using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
	/// <summary>
	/// This example returns multiple values to the UI.
	/// This class holds the response.
	/// </summary>
	public class UploadAndGetFileData
	{
		public StringBuilder OutputText;
		public StringBuilder ErrorText;
		public Sprite Sprite;
		public int BytesLength;
		public string Url;
	}
	
	/// <summary>
	/// Example: Filecoin Storage
	///
	/// Documentation:
	/// 
	/// <list type="bullet">
	///		<item>See <see cref="https://filecoin.io/blog/posts/introducing-web3-storage/"/> </item>
	///		<item>See <see cref="https://web3.storage/docs/reference/http-api/"/> </item>
	/// </list>
	/// 
	/// Coding Concerns:
	/// 
	/// <list type="bullet">
	///		<item>See <see cref="Example_UI"/> for the UI functionality</item>
	///		<item>See below for the core functionality</item>
	/// </list>
	/// </summary>
	public class Example_Filecoin_Storage_01 : Example_UI
	{
		
		//  General Methods -------------------------------	
		public static async UniTask<UploadAndGetFileData>
			Filecoin_UploadAndGetFile(byte[] bytes,
				StringBuilder outputText,
				StringBuilder errorText)
		{

			///////////////////////////////////////////
			// SETUP
			///////////////////////////////////////////
			//		1. Create free account at https://web3.storage/
			//		2. Create token at https://web3.storage/tokens/
			//		3. Past your token below
			//		4. Run the Scene "Example_Filecoin_Storage_01.unity"
			//		5. Optional: See uploaded files at https://web3.storage/account/
			///////////////////////////////////////////
			string token =
				"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkaWQ6ZXRocjoweEY0MjU3Q2IyZDMyOWEzRWIzMTM1MzI1YzgyYzAzNkFlYWMwMkE3NDgiLCJpc3MiOiJ3ZWIzLXN0b3JhZ2UiLCJpYXQiOjE2NjA2NzQxNjI0MTMsIm5hbWUiOiJ3ZWIzLXVuaXR5LXNkay1leGFtcGxlcyJ9.FAtQ2W7HxzLAG68U1clOE5CpjaWYbYvrnlTmeVm53as";

			///////////////////////////////////////////
			// UploadFile
			///////////////////////////////////////////
			Debug.Log("FilecoinWeb3Storage.UploadFile(...)");
			var filecoinRequestAsync01 = new FilecoinWeb3Storage(token);
			UploadFileResponse uploadFileResponse = await filecoinRequestAsync01.UploadFile(bytes);
			Debug.Log($"{uploadFileResponse}");
			string lastUploadedCid = uploadFileResponse.cid;
			
			///////////////////////////////////////////
			// GetStatus
			///////////////////////////////////////////
			Debug.Log("\n\n");
			Debug.Log("\nFilecoinWeb3Storage.GetStatus(...)");
			var filecoinRequestAsync02 = new FilecoinWeb3Storage(token);
			GetStatusResponse getStatusResponse = await filecoinRequestAsync02.GetStatus(lastUploadedCid);
			Debug.Log($"{getStatusResponse}");

			///////////////////////////////////////////
			// GetFile
			///////////////////////////////////////////
			Debug.Log("\n\n");
			Debug.Log("FilecoinWeb3Storage.GetFile(...)");
			var filecoinRequestAsync03 = new FilecoinWeb3Storage(token);
			GetFileResponse getFileResponse = await filecoinRequestAsync03.GetFile(lastUploadedCid);
			Debug.Log($"{getFileResponse}");
			
			return new UploadAndGetFileData
			{
				OutputText = outputText,
				ErrorText = errorText,
				Sprite = SharedHelper.CreateSpriteFromBytes(getFileResponse.data),
				Url = getFileResponse.url,
				
				// For brevity, length is shown instead of full data
				BytesLength = getFileResponse.data.Length
				
			};
		}
	}
}
