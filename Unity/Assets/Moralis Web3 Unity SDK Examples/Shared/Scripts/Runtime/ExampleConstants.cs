﻿using MoralisUnity.Sdk.Constants;

namespace MoralisUnity.Examples.Sdk.Shared
{
    /// <summary>
    /// Helper values
    /// </summary>
    public static class ExampleConstants
    {
        //  Fields  -----------------------------------------------
        
        ///////////////////////////////////////////
        // MenuItem Path
        ///////////////////////////////////////////
        public const string Web3UnitySDKExamples = "Web3 Unity SDK Examples";
        public const string OpenReadMe = MoralisConstants.Open + " " + "ReadMe";
        public const string PathMoralisExamplesCreateAssetMenu = Moralis + MoralisConstants.Web3UnitySDK + "/Examples/" + Web3UnitySDKExamples + "/";
        private const string PathMoralisExamplesToolsMenu = "Tools/" + Moralis + "/" + MoralisConstants.Web3UnitySDK  +"/Examples/" + Web3UnitySDKExamples + "/";
        
        ///////////////////////////////////////////
        // MenuItem Priority
        ///////////////////////////////////////////

        // Skipping ">10" shows a horizontal divider line.
        public const int PriorityMoralisWindow_Primary = 10;
        public const int PriorityMoralisWindow_Secondary = 100;
        public const int PriorityMoralisWindow_Examples = 1000;
        public const int PriorityMoralisWindow_Samples = 10000;
        
        ///////////////////////////////////////////
        // Display Text
        ///////////////////////////////////////////
        public const string Moralis = "Moralis";
        public const string Web3UnitySDK = "Web3 Unity SDK";
        public const string Web3UnitySDKVersion = "v1.2.4"; // This may be out of date. Check to Manifest.json, then re-update here
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
        
        ///////////////////////////////////////////
        // Web3 Addresses
        ///////////////////////////////////////////
        // A random account (not mine) with much history for testing - https://solscan.io/account/3yFwqXBfZY4jBVUafQ1YEXw189y2dN3V5KQq9uzBDy1E
        public const string AddressForSolanaTesting = "3yFwqXBfZY4jBVUafQ1YEXw189y2dN3V5KQq9uzBDy1E";

        // A random account (not mine) with much history for testing - https://etherscan.io/address/0xda9dfa130df4de4673b89022ee50ff26f6ea73cf
        public const string AddressForTesting = "0xDA9dfA130Df4dE4673b89022EE50ff26f6EA73Cf";
        public const string TokenAddressForTesting = "0x00000000219ab540356cbb839cbe05303d7705fa";
        public static string MissingWalletConnectPrefab = "Method failed. WalletConnect.Instance must not be null. Add the WalletConnect.prefab to your scene.";
    }
}