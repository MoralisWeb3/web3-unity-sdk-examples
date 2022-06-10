

using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Web3Api.Models;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Web3API_Storage_01
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
	public class Example_Web3API_Storage_01 : Example_UI
	{
		
		//  General Methods -------------------------------	
		public static async UniTask<ExampleResponse>
			MoralisClient_Web3Api_Storage_UploadFolder(string content,
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
					await moralisClient.Web3Api.Storage.UploadFolder(ipfsFileRequests);
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
