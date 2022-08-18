using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
	
	/// <summary>
	/// Rarely, this example must return multiple values to the UI.
	/// This class holds the response.
	/// </summary>
	public class UploadAndGetFileData
	{
		public StringBuilder OutputText;
		public StringBuilder ErrorText;
		public Sprite Sprite;
	}
	
	/// <summary>
	/// Example: IStorageApi
	///
	/// Documentation: <see cref="http://docs.moralis.io/unity"/>
	/// 
	/// Coding Concerns
	/// <list type="bullet">
	/// 
	/// <item>See <see cref="Example_UI"/> for the UI functionality</item>
	/// <item>See below for the core functionality</item>
	/// 
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

			string token =
				"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkaWQ6ZXRocjoweEY0MjU3Q2IyZDMyOWEzRWIzMTM1MzI1YzgyYzAzNkFlYWMwMkE3NDgiLCJpc3MiOiJ3ZWIzLXN0b3JhZ2UiLCJpYXQiOjE2NjA2NzQxNjI0MTMsIm5hbWUiOiJ3ZWIzLXVuaXR5LXNkay1leGFtcGxlcyJ9.FAtQ2W7HxzLAG68U1clOE5CpjaWYbYvrnlTmeVm53as";

			Debug.Log("\n\n");
			Debug.Log("----- UploadFile() --------");
			var filecoinRequestAsync01 = new FilecoinRequestAsync(token);
			UploadResponse uploadResponse = await filecoinRequestAsync01.UploadFile(bytes);
			Debug.Log($"\tuploadResponse = {uploadResponse}");
			string lastUploadedCid = uploadResponse.cid;
			
			
			Debug.Log("\n\n");
			Debug.Log("\n----- GetStatus() --------");
			var filecoinRequestAsync02 = new FilecoinRequestAsync(token);
			StatusResponse statusResponse = await filecoinRequestAsync02.GetStatus(lastUploadedCid);
			Debug.Log($"\tstatusResponse = {statusResponse}");

			
			Debug.Log("\n\n");
			Debug.Log("----- GetFile() --------");
			var filecoinRequestAsync03 = new FilecoinRequestAsync(token);
			GetFileResponse getFileResponse = await filecoinRequestAsync03.GetFile(lastUploadedCid);
			Debug.Log($"\tgetFileResponse = {getFileResponse.data}");
			
			
			UploadAndGetFileData uploadAndGetFileData = new UploadAndGetFileData
			{
				OutputText = outputText,
				ErrorText = errorText,
				Sprite = SharedHelper.CreateSpriteFromBytes(getFileResponse.data)
			};
			return uploadAndGetFileData;
		}
	}
}
