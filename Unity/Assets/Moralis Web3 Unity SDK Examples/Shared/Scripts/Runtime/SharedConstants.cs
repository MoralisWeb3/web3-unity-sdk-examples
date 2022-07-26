using MoralisUnity.Sdk.Constants;

namespace MoralisUnity.Examples.Sdk.Shared
{
    /// <summary>
    /// Helper values
    /// </summary>
    public static class SharedConstants
    {
        //  Fields  -----------------------------------------------
        
        ///////////////////////////////////////////
        // MenuItem Path
        ///////////////////////////////////////////
        public const string Web3UnitySDKExamples = "Web3 Unity SDK Examples";
        public const string OpenReadMe = MoralisConstants.Open + " " + "ReadMe";
        public const string PathMoralisExamplesCreateAssetMenu = Moralis + MoralisConstants.Web3UnitySDK + "/Examples/" + Web3UnitySDKExamples + "/";
        private const string PathMoralisExamplesToolsMenu = "Tools/" + Moralis + "/" + MoralisConstants.Web3UnitySDK  +"/Examples/" + Web3UnitySDKExamples + "/";
        public const string PathMoralisExamplesAssetsMenu = "Assets/Moralis Web3 Unity SDK/Examples";
        
        ///////////////////////////////////////////
        // MenuItem Priority
        ///////////////////////////////////////////

        // Skipping ">10" shows a horizontal divider line.
        public const int PriorityMoralisTools_Primary = 10;
        public const int PriorityMoralisTools_Secondary = 100;
        public const int PriorityMoralisTools_Examples = 1000;
        public const int PriorityMoralisTools_Examples_Sub = 5000;
        public const int PriorityMoralisTools_Samples = 10000;
        public const int PriorityMoralisAssets_Examples = 1;
        
        ///////////////////////////////////////////
        // Display Text
        ///////////////////////////////////////////
        public const string Moralis = "Moralis";
       
        public const string Web3UnitySDK = "Web3 Unity SDK";
        public const string Web3UnitySDKVersion = "v1.2.4"; // This may be out of date. Check to Manifest.json, then re-update here
        public const string ProductWithVersion = SharedConstants.Moralis + SharedConstants.Web3UnitySDK + " " + SharedConstants.Web3UnitySDKVersion;
        public const string KnownIssueReported = "Known issue: Reported to Moralis in " + ProductWithVersion + ".";
        public const string Chains = "Chains";
        public const string Main = "Main";
        public const string Details = "Details";
        public const string Loading = "Loading ...";
        public const string Success = "Success!";
        public const string PendingTransactionMessage ="Please confirm transaction in your MOBILE wallet." ; 
        public const string NothingAvailable = "Nothing available";
        public const string Authenticate = "Authenticate";
        public const string Results = "Results";
        public const string Type = "Type";
        public const string LogOut = "Log Out";
        public const string NotExpectedSoFix = "Not Expected. Fix.";
        public const string CloudFunctionNotFound = "Empty result. Ensure Cloud Function exists on server.";
        public const string TopPanelBodyTextMustLogInFirst1 = "load the '{0}' Scene to Log In. Then return to the '{1}' Scene to continue.\n"; //starts lower case on purpose
        public const string YouAreNotLoggedIn = "You are not logged in.";
        //
        public const string DialogConfirmation = "Confirmation";
        public const string DialogAreYouSure = "Are you sure?";
        public const string DialogReset = "Reset";

        public const string DialogLoading = "Loading...";
        public const string DialogTitleTextAuthenticate = "Authentication";
        public static readonly string BodyTextAuthenticate = $"Click '{Authenticate}' to log in.";
        public static readonly string DialogBodyTextAuthenticate = $"Click '{Ok}' to {TopPanelBodyTextMustLogInFirst1}";
        public const string DialogTitleAddress = "Address";
        //
        public const string Ok = "Ok";
        public const string Cancel = "Cancel";
        //
        public const string SceneSetupInstructions = "Scene Setup Instructions";
        
        ///////////////////////////////////////////
        // Clickable Urls
        ///////////////////////////////////////////
        public const string MoralisServersUrl = "https://admin.moralis.io/";

        public static string MissingWalletConnectPrefab = "Method failed. WalletConnect.Instance must not be null. Add the WalletConnect.prefab to your scene.";


        ///////////////////////////////////////////
        // Web3 Addresses - Solana
        ///////////////////////////////////////////
        // A random account (not mine) with much history for testing - https://solscan.io/account/3yFwqXBfZY4jBVUafQ1YEXw189y2dN3V5KQq9uzBDy1E
        public const string SolanaAddressForTesting = "3yFwqXBfZY4jBVUafQ1YEXw189y2dN3V5KQq9uzBDy1E";


        ///////////////////////////////////////////
        // Web3 Addresses - ETH
        ///////////////////////////////////////////

        // A random account (not mine) with much history for testing - https://etherscan.io/address/0xda9dfa130df4de4673b89022ee50ff26f6ea73cf
        public const string EthAddressForTesting = "0xDA9dfA130Df4dE4673b89022EE50ff26f6EA73Cf";
        public const string EthTokenAddressForTesting = "0x00000000219ab540356cbb839cbe05303d7705fa";

        ///////////////////////////////////////////
        // Web3 Addresses - Cronos Testnet
        ///////////////////////////////////////////

        // A random account (not mine) with much history for testing - https://testnet.cronoscan.com/address/0xffd3a0042c75dda5d41e7aa620ecb970237f513a
        public const string CronosTestnetExampleAddress = "0xffd3a0042C75DDa5d41e7aa620ecB970237F513a";
        public const string CronosTestnetExampleTokenAddress = "0x66ec7b182018B5Ab622e0971222211BFf7Dafc9B";

        // A random account (not mine) with much history for testing - https://cronoscan.com/address/0x5c7f8a570d578ed84e63fdfa7b1ee72deae1ae23
        public const string CronosExampleAddress = "0x5C7F8A570d578ED84E63fdFA7b1eE72dEae1AE23";
        public const string CronosExampleTokenAddress = "0x5C7F8A570d578ED84E63fdFA7b1eE72dEae1AE23";




    }
}