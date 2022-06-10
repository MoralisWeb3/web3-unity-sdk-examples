using Cysharp.Threading.Tasks;
using WalletConnectSharp.Unity;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Session_Sign_01	
{

	/// <summary>
	/// Example: Sign
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
	public class Example_Session_Sign_01 : Example_UI
	{

		public static async UniTask<string> WalletConnect_Session_EthPersonalSign(
			WalletConnect walletConnect, 
			string message, 
			string address)
		{

			// <see cref="https://www.codementor.io/@yosriady/signing-and-verifying-ethereum-signatures-vhe8ro3h6#background"/>
			// <see cref="https://support.mycrypto.com/how-to/getting-started/how-to-sign-and-verify-messages-on-ethereum/"/>
			// <see cref="https://docs.moralis.io/misc/faq#why-do-you-use-the-signing-messages-and-other-dapps-dont"/>
			
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////
#if UNITY_WEBGL
            var result = await Web3GL.Sign(message);
#else
			var result = await walletConnect.Session.EthPersonalSign(address, message);
#endif

			return result;
		}
	}
}
