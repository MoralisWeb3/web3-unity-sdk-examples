using System.Collections.Generic;
using Cysharp.Threading.Tasks;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_MoralisCloud_01
{
	/// <summary>
	/// Example: MoralisCloud
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
	public class Example_MoralisCloud_01 : Example_UI
	{
		//  General Methods -------------------------------	
		public static async UniTask<string> Moralis_Cloud_RunAsync_01()
		{
			///////////////////////////////////////////
			// Execute: RunAsync
			///////////////////////////////////////////
			string result = await Moralis.Cloud.RunAsync<string>("myMethod01", null);
			return result;
		}
		
		public static async UniTask<string> Moralis_Cloud_RunAsync_02()
		{
			///////////////////////////////////////////
			// Execute: RunAsync
			///////////////////////////////////////////
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			parameters.Add("a", 10);
			parameters.Add("b", 20);
			
			string result = await Moralis.Cloud.RunAsync<string>("myMethod02", parameters);
			return result;
		}
	}
}
