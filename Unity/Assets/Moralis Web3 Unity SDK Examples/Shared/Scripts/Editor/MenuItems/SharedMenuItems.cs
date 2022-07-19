﻿using System.Collections.Generic;
using UnityEditor;
using MoralisUnity.Sdk.Constants;
using MoralisUnity.Sdk.UI.ReadMe;
using UnityEngine;
using System.IO;
using MoralisUnity.Examples.Sdk.Shared.Data.Types.Storage;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Shared.Data.Types.Storage;
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
			SharedConstants.Web3UnitySDKExamples + "/" + SharedConstants.OpenReadMe, false,
			SharedConstants.PriorityMoralisWindow_Examples)]
		public static void OpenReadMe()
		{
			ReadMeEditor.SelectReadmeGuid("3b4d333465945474ea57ff6e62ba4f37");
		}


		[MenuItem(MoralisConstants.PathMoralisExamplesWindowMenu + "/" +
				   SharedConstants.Web3UnitySDKExamples + "/" + "Add Example Scenes To Build Settings", false,
			SharedConstants.PriorityMoralisWindow_Examples)]
		public static void AddAllScenesToBuildSettings()
		{
			List<SceneData> sceneDatas = SceneDataStorage.Instance.SceneDatas;

			Debug.Log($"AddAllScenesToBuildSettings() sceneAssets.Count={sceneDatas.Count}");
			EditorBuildSettingsUtility.AddScenesToBuildSettings(sceneDatas);
		}


		[MenuItem(MoralisConstants.PathMoralisExamplesWindowMenu + "/" +
		          SharedConstants.Web3UnitySDKExamples + "/" + "Load Moralis Layout (10x16)", false,
			SharedConstants.PriorityMoralisWindow_Examples)]
		public static void LoadExampleLayout_10x16()
		{
			string guid = "68e09fd97bc6f3f4f9154ccdf9ece35d";
			string path = AssetDatabase.GUIDToAssetPath(guid);
			UnityReflectionUtility.UnityEditor_WindowLayout_LoadWindowLayout(path);
		}
		
		
		[MenuItem(MoralisConstants.PathMoralisExamplesWindowMenu + "/" +
		          SharedConstants.Web3UnitySDKExamples + "/" + "Load Moralis Layout (16x10)", false,
			SharedConstants.PriorityMoralisWindow_Examples)]
		public static void LoadExampleLayout_16x10()
		{
			string guid = "bb0830cff9fd5fa4b9ac04292dc30acc";
			string path = AssetDatabase.GUIDToAssetPath(guid);
			UnityReflectionUtility.UnityEditor_WindowLayout_LoadWindowLayout(path);
		}

		
		
		[MenuItem( SharedConstants.PathMoralisExamplesAssetsMenu + "/" + "Copy Guid", false,
			SharedConstants.PriorityMoralisAssets_Examples)]
		public static void CopyGuidToClipboard()
		{
			// Support only if exactly 1 object is selected in project window
			var objs = Selection.objects;
			if (objs.Length != 1)
			{
				return;
			}

			var obj = objs[0];
			string path = AssetDatabase.GetAssetPath(obj);
			GUID guid = AssetDatabase.GUIDFromAssetPath(path);
			GUIUtility.systemCopyBuffer = guid.ToString();
			Debug.Log($"CopyGuidToClipboard() success! Value '{GUIUtility.systemCopyBuffer}' copied to clipboard.");
		}
		
		
		[MenuItem( SharedConstants.PathMoralisExamplesAssetsMenu + "/" + "Copy Guid", true,
			SharedConstants.PriorityMoralisAssets_Examples)]
		public static bool CopyGuidToClipboard_ValidationFunction()
		{
			// Support only if exactly 1 object is selected in project window
			var objs = Selection.objects;
			return objs.Length == 1;
		}
	}
}
