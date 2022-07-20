using System.Text;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Examples.Sdk.Shared.Data.Types.Storage;
using MoralisUnity.Samples.Shared.Attributes;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_LocalDiskStorage_01	
{
	[CustomFilePath(LocalDiskStorage.Title + "/SampleData.txt", CustomFilePathLocation.StreamingAssetsPath)]
	[System.Serializable]
	public class SampleData
	{
		//  Properties ------------------------------------
		public int ClickCount { get { return _clickCount; } set { _clickCount = value; }}
		
		//  Fields ----------------------------------------
		[SerializeField] 
		private int _clickCount = 0;
	}

	
	/// <summary>
	/// Example: Local Disk Storage
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
	public class Example_LocalDiskStorage_01 : Example_UI
	{
		public static SampleData LocalDiskStorage_Load(ref StringBuilder outputText)
		{

			SampleData sampleData = null;

			bool hasObject = LocalDiskStorage.Instance.Has<SampleData>();
			
			outputText.Clear();
			outputText.AppendHeaderLine($"Load()");
			outputText.AppendBullet($"Has<T>() = {hasObject}");
			
			if (hasObject)
			{
				///////////////////////////////////////////
				// Execute: Load<T>
				///////////////////////////////////////////
				sampleData = LocalDiskStorage.Instance.Load<SampleData>();
				outputText.AppendBullet($"Loaded!");
			}
			else
			{
				// Does not exist on disk? Create it
				sampleData = new SampleData();
				outputText.AppendBullet($"Created!");
			}
			
			return sampleData;
		}
		
		
		public static void LocalDiskStorage_Increment(SampleData sampleData, ref StringBuilder outputText)
		{
			
			///////////////////////////////////////////
			// Execute: ++
			///////////////////////////////////////////
			sampleData.ClickCount++;
			
			outputText.Clear();
			outputText.AppendHeaderLine($"sampleData.ClickCount++");
		}
		
		
		public static bool LocalDiskStorage_Save(SampleData sampleData, ref StringBuilder outputText)
		{
			
			///////////////////////////////////////////
			// Execute: Save<T>
			///////////////////////////////////////////
			bool isSuccess = LocalDiskStorage.Instance.Save<SampleData>(sampleData);

			// Update UI
			CustomFilePathAttribute customFilePathAttribute = CustomFilePathAttribute.GetCustomFilePathAttribute<SampleData>();
			outputText.Clear();
			outputText.AppendHeaderLine($"Save()");
			outputText.AppendBullet($"isSuccess = {isSuccess}");
			outputText.AppendBullet($"Location = {customFilePathAttribute.Location}");

			return isSuccess;
		}
	}
}