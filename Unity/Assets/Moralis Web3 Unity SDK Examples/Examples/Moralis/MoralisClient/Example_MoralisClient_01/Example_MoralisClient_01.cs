using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_MoralisClient_01
{
	/// <summary>
	/// Example: Moralis Client
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
	public class Example_MoralisClient_01 : Example_UI
	{

		//  General Methods -------------------------------	
		public static async UniTask<StringBuilder> Moralis_GetClient(StringBuilder outputText)
		{
			///////////////////////////////////////////
			// Execute: GetClient
			///////////////////////////////////////////
			MoralisClient moralisClient = Moralis.GetClient();
			
			outputText.Clear();
			outputText.AppendHeaderLine($"Moralis.GetClient()");
			outputText.AppendLine();
			outputText.AppendBullet($"Dapp Id = {moralisClient.ApplicationId}");
			outputText.AppendBullet($"Dapp Url = {moralisClient.ServerUrl}");
			outputText.AppendBullet($"EthAddress: {moralisClient.EthAddress}");
			outputText.AppendLine();

			if (string.IsNullOrEmpty(moralisClient.EthAddress))
			{
				outputText.AppendErrorLine($"EthAddress must not be null or empty. " +
				                           $"This is a known issue and has been reported to Moralis in " +
				                           $"Web3 Unity SDK v1.2.1.");
				outputText.AppendLine();
			}

			return outputText;
		}
	}
}
