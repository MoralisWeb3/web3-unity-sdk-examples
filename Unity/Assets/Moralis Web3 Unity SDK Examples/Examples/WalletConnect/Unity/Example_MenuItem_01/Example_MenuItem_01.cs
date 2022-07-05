#if UNITY_EDITOR
using MoralisUnity.Kits.AuthenticationKit;
using UnityEditor;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_MenuItem_01	
{
	/// <summary>
	/// Example: MenuItem
	///
	/// Documentation:
	///		
	/// <list type="bullet">
	/// 
	/// <item>See <a href="https://docs.unity3d.com/ScriptReference/MenuItem.html">MenuItem</a></item>
	/// <item>See <a href="https://docs.unity3d.com/ScriptReference/EditorApplication.ExecuteMenuItem.html">ExecuteMenuItem</a></item>
	/// 
	/// </list>
	///
	/// 
	/// Coding Concerns:
	/// 
	/// <list type="bullet">
	/// 
	/// <item>See <see cref="Example_UI"/> for the UI functionality</item>
	/// <item>See below for the core functionality</item>
	/// 
	/// </list>
	/// </summary>
	public class Example_MenuItem_01 : Example_UI
	{
		// The MenuItem will appear in Unity's top menu-bar in this nested structure
		private const string SdkPath = "Tools/Moralis Web3 Unity SDK/";
		private const string ProjectPath = 	SdkPath + "Examples/Web3 Unity SDK Examples/Example_MenuItem_01/";
		
		// The MenuItem will appear with this title
		public const string MenuItemTitle = "Add To Scene: AuthenticationKit";
		
		// Optional: The MenuItem will appear with this vertical ordering priority (0 = top)
		private const int MenuItemPriority = 5000;
		
		// Optional: The MenuItem will execute upon hotkey
		//		# (shift)
		//		% (ctrl/cmd)
		//		& (alt)
		private const string MenuItemHotkey = " #%&a";
		private const string MenuItemPath = ProjectPath + MenuItemTitle + MenuItemHotkey;
		
		/// <summary>
		/// This methods DISPLAYS the MenuItem
		/// </summary>
		[MenuItem(MenuItemPath, false, MenuItemPriority)]
		public static void Unity_ExampleMenuItem()
		{
			///////////////////////////////////////////
			// Execute
			///////////////////////////////////////////

			// Create new undo group
			Undo.IncrementCurrentGroup();
			
			// Find file by GUID (Value from "AuthenticationKit.prefab.meta" file)
			string path = AssetDatabase.GUIDToAssetPath("a41feed31bcc36541a7a9505212ddc63");
			
			// Load Prefab
			GameObject authenticationKitPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
			
			// Move Prefab into the active Scene Hierarchy
			GameObject authenticationKit = GameObject.Instantiate<GameObject>(authenticationKitPrefab);
			authenticationKit.name = authenticationKit.name.Replace("(Clone)", "");
			
			// Save undo group
			Undo.RegisterCreatedObjectUndo(authenticationKit, "Create AuthenticationKit");
			Undo.RegisterFullObjectHierarchyUndo(authenticationKit, "Update AuthenticationKit");
			
			// Position Prefab under the Scene Hierarchy actively selected GameObject, if any
			GameObject activeGameObject = Selection.activeGameObject;
			GameObjectUtility.SetParentAndAlign(authenticationKit, activeGameObject);
			
			// Select Prefab
			Selection.activeObject = authenticationKit;
			
			// Name undo group
			Undo.SetCurrentGroupName("Add AuthenticationKit");
		}
		
		/// <summary>
		/// Optional: This methods ENABLES/DISABLES the MenuItem
		/// </summary>
		[MenuItem(MenuItemPath, true, MenuItemPriority)]
		public static bool Unity_ExampleMenuItem_ValidationFunction()
		{
			// Determines if an instance already exists in the current scene
			AuthenticationKit authenticationKit = GameObject.FindObjectOfType<AuthenticationKit>();
			bool hasAuthenticationKit = authenticationKit != null;
			
			return !hasAuthenticationKit;
		}
			
		/// <summary>
		/// Optional: This methods calls the MenuItem directly
		/// </summary>
		public static void Unity_ExecuteMenuItem()
		{
			EditorApplication.ExecuteMenuItem(MenuItemPath);
		}
	}
}
#endif //#if UNITY_EDITOR
