using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Platform.Objects;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_MoralisUser_01	
{
	/// <summary>
	/// Example: MoralisUser
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
	public class Example_MoralisUser_01 : Example_UI
	{
		public static async UniTask<StringBuilder> Moralis_GetUserAsync(StringBuilder outputText)
		{
			
			///////////////////////////////////////////
			// Execute: GetUserAsync
			///////////////////////////////////////////
			MoralisUser moralisUser = await Moralis.GetUserAsync();
			
			outputText.Clear();
			outputText.AppendHeaderLine($"Moralis.GetUserAsync()");
			outputText.AppendBullet(
				$"moralisUser.email = {moralisUser.email}");
			outputText.AppendBullet(
				$"moralisUser.password = {moralisUser.password}");
			outputText.AppendBullet(
				$"moralisUser.username = {moralisUser.username}");
			outputText.AppendBullet(
				$"moralisUser.sessionToken = {moralisUser.sessionToken}");
			
			// Show all contents of the authData
			outputText.AppendBullet(
				$"moralisUser.authData...");
			foreach (KeyValuePair<string, IDictionary<string, object>> kvp in moralisUser.authData)
			{
				outputText.AppendBullet(
					$"{kvp.Key}...", 2);
				
				if (kvp.Value != null)
				{
					foreach (var kvp2 in kvp.Value)
					{
						outputText.AppendBullet(
							$"{kvp2.Key} = {kvp2.Value}", 3);
					}
				}
			}

			return outputText;
		}
	}
}