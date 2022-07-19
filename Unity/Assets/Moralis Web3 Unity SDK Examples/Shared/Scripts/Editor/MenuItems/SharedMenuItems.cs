using System.Collections.Generic;
using UnityEditor;
using MoralisUnity.Sdk.Constants;
using MoralisUnity.Sdk.UI.ReadMe;
using UnityEngine;
using System.IO;
using MoralisUnity.Examples.Sdk.Shared.Data.Types.Storage;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Shared.Utilities;

namespace MoralisUnity.Examples.Sdk.Shared.MenuItems
{
	/// <summary>
	/// The MenuItem attribute allows you to add menu items to the main menu and inspector context menus.
	/// <see cref="https://docs.unity3d.com/ScriptReference/MenuItem.html"/>
	/// </summary>
	public static class SharedMenuItems
	{

		[MenuItem(MoralisConstants.PathMoralisExamplesWindowMenu + "/" +
			ExampleConstants.Web3UnitySDKExamples + "/" + ExampleConstants.OpenReadMe, false,
			ExampleConstants.PriorityMoralisWindow_Examples)]
		public static void OpenReadMe()
		{
			ReadMeEditor.SelectReadmeGuid("3b4d333465945474ea57ff6e62ba4f37");
		}


		[MenuItem(MoralisConstants.PathMoralisExamplesWindowMenu + "/" +
				   ExampleConstants.Web3UnitySDKExamples + "/" + "Add Example Scenes To Build Settings", false,
			ExampleConstants.PriorityMoralisWindow_Examples)]
		public static void AddAllScenesToBuildSettings()
		{
			List<SceneData> sceneDatas = SceneDataStorage.Instance.SceneDatas;

			Debug.Log($"AddAllScenesToBuildSettings() sceneAssets.Count={sceneDatas.Count}");
			EditorBuildSettingsUtility.AddScenesToBuildSettings(sceneDatas);
		}


		[MenuItem(MoralisConstants.PathMoralisExamplesWindowMenu + "/" +
				   ExampleConstants.Web3UnitySDKExamples + "/" + "Load Moralis Layout (10x16)", false,
			ExampleConstants.PriorityMoralisWindow_Examples)]
		public static void LoadExampleLayout_10x16()
		{
			string path = Path.GetFullPath("Assets/Moralis Web3 Unity SDK Examples/Shared/Layouts/MoralisLayout10x16.wlt");
			UnityReflectionUtility.UnityEditor_WindowLayout_LoadWindowLayout(path);
		}
		
		
		[MenuItem(MoralisConstants.PathMoralisExamplesWindowMenu + "/" +
		          ExampleConstants.Web3UnitySDKExamples + "/" + "Load Moralis Layout (16x10)", false,
			ExampleConstants.PriorityMoralisWindow_Examples)]
		public static void LoadExampleLayout_16x10()
		{
			string path = Path.GetFullPath("Assets/Moralis Web3 Unity SDK Examples/Shared/Layouts/MoralisLayout16x10.wlt");
			UnityReflectionUtility.UnityEditor_WindowLayout_LoadWindowLayout(path);
		}

		
	}
}
