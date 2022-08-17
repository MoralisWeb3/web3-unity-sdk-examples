

using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Web3Api.Models;
using Newtonsoft.Json;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Filecoin_Storage_01
{
	
	/// <summary>
	/// Rarely, this example must return multiple values to the UI.
	/// This class holds the response.
	/// </summary>
	public class ExampleResponse
	{
		public StringBuilder OutputText;
		public StringBuilder ErrorText;
		public List<IpfsFile> LastLoadedIpfsFiles;
	}

	public class FilecoinData
	{
		public StringBuilder OutputText;
		public StringBuilder ErrorText;
		public Sprite Sprite;
	}
	
	[Serializable]
	public class MyCustomObject
	{
		public string Message = "";
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
		public static async UniTask<FilecoinData>
			Filecoin_Storage(string content,
				StringBuilder outputText,
				StringBuilder errorText)
		{

			//THis is an image file
			//string cid = "bafybeiccc4roluamu6vxg244txlnktci5k6s3tzliyxyzlhpd5dten3bnu";

			// This is a json file
			string cid = "bafkreiaxsb5qhyahtgifpg7rhz325gparzmwkkgvwzhgcbr5eyfodpfn6a";

			string token =
				"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkaWQ6ZXRocjoweEY0MjU3Q2IyZDMyOWEzRWIzMTM1MzI1YzgyYzAzNkFlYWMwMkE3NDgiLCJpc3MiOiJ3ZWIzLXN0b3JhZ2UiLCJpYXQiOjE2NjA2NzQxNjI0MTMsIm5hbWUiOiJ3ZWIzLXVuaXR5LXNkay1leGFtcGxlcyJ9.FAtQ2W7HxzLAG68U1clOE5CpjaWYbYvrnlTmeVm53as";


			MyCustomObject myCustomObject = new MyCustomObject();
			myCustomObject.Message = $"here is some data number ${UnityEngine.Random.Range(100, 999)}";
			string data = JsonConvert.SerializeObject(myCustomObject);

			Debug.Log("\n-----SetUpload()--------");
			var filecoinRequestAsync01 = new FilecoinRequestAsync(token);
			UploadResponse uploadResponse = await filecoinRequestAsync01.Upload(data);
			Debug.Log($"\tuploadResponse = {uploadResponse}");

			Debug.Log("\n-----GetStatus()--------");
			var filecoinRequestAsync02 = new FilecoinRequestAsync(token);
			StatusResponse statusResponse = await filecoinRequestAsync02.GetStatus(cid);
			Debug.Log($"\tstatusResponse = {statusResponse}");

			// FAILSS - CANNOT PARSE ON MY END
			// Debug.Log("\n-----GetUploads()--------");
			// var filecoinRequestAsync03 = new FilecoinRequestAsync(token);
			// UploadsResponse uploadsResponse = await filecoinRequestAsync03.GetUploads();
			// Debug.Log($"\tuploadsResponse = {uploadsResponse}");


			//
			// Debug.Log("\n-----GetCar()--------");
			// var filecoinRequestAsync04 = new FilecoinRequestAsync(token);
			// var carResponse = await filecoinRequestAsync04.GetCar<MyCustomObject>(cid);
			// Debug.Log($"\tcarResponse = {carResponse}");
			//

			var filecoinRequestAsync05 = new FilecoinRequestAsync(token);
			Sprite sprite = await filecoinRequestAsync05.GetImage();

			FilecoinData filecoinData = new FilecoinData
			{
				OutputText = outputText,
				ErrorText = errorText,
				Sprite = sprite
			};
			return filecoinData;
		}

		public static async UniTask<ExampleResponse>
			Filecoin_StorageOld(string content,
				StringBuilder outputText, 
				StringBuilder errorText)
		{

			// Define file information.
			IpfsFileRequest ipfsFileRequest = new IpfsFileRequest()
			{
				Path = "moralis/ipfsFileRequest2.png",
				Content = content
			};
			
			// Prepare
			outputText.Clear();
			outputText.AppendHeaderLine($"new IpfsFileRequest()");
			outputText.AppendBullet($"Path = {ipfsFileRequest.Path}");
			
			// Rendering text for "ipfsFileRequest.Content" is too long.
			// So use "ipfsFileRequest.Content.Length" instead
			outputText.AppendBullet($"Content.Length = {ipfsFileRequest.Content.Length}");
			
			// Request is a list; Supports one or more images
			List<IpfsFileRequest> ipfsFileRequests = new List<IpfsFileRequest>();
			ipfsFileRequests.Add(ipfsFileRequest);

			MoralisClient moralisClient = Moralis.GetClient();
			List<IpfsFile> lastLoadedIpfsFiles = new List<IpfsFile>();
			
			try
			{
				errorText.Clear();
				lastLoadedIpfsFiles = 
					await Moralis.Web3Api.Storage.UploadFolder(ipfsFileRequests);
			}
			catch (Exception exception)
			{
				errorText.AppendErrorLine($"UploadFolder() e.Message = {exception.Message}");
				Debug.LogError($"{errorText.ToString()}");
			}

			return new ExampleResponse ()
			{
				OutputText = outputText,
				ErrorText =  errorText,
				LastLoadedIpfsFiles = lastLoadedIpfsFiles
			};
		}
	}
}
