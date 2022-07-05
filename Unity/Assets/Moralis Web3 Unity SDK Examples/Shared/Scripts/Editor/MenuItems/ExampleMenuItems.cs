using System.Collections.Generic;
using UnityEditor;
using MoralisUnity.Sdk.Constants;
using MoralisUnity.Sdk.UI.ReadMe;
using UnityEngine;
using System.IO;
using MoralisUnity.Samples.Shared.Data.Storage;

namespace MoralisUnity.Examples.Sdk.Shared
{
	/// <summary>
	/// The MenuItem attribute allows you to add menu items to the main menu and inspector context menus.
	/// <see cref="https://docs.unity3d.com/ScriptReference/MenuItem.html"/>
	/// </summary>
	public static class ExampleMenuItems
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
			List<SceneAsset> sceneAssets = SceneDataStorage.Instance.SceneAssets;

			Debug.Log($"AddAllScenesToBuildSettings() sceneAssets.Count={sceneAssets.Count}");
			EditorBuildSettingsUtility.AddScenesToBuildSettings(sceneAssets);
		}


		[MenuItem(MoralisConstants.PathMoralisExamplesWindowMenu + "/" +
				   ExampleConstants.Web3UnitySDKExamples + "/" + "Load Example Layout (10x16)", false,
			ExampleConstants.PriorityMoralisWindow_Examples)]
		public static void LoadExampleLayout()
		{
			string path = Path.GetFullPath("Assets/Moralis Web3 Unity SDK Examples/Shared/Layouts/MoralisUnityLayout10x16.wlt");
			UnityReflectionUtility.UnityEditor_WindowLayout_LoadWindowLayout(path);
		}
	}
}
