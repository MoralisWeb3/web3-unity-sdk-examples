
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared;
using MoralisUnity.Platform.Queries;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Query_01	
{
	/// <summary>
	/// Example: MoralisUser
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
	public class Example_Query_01 : Example_UI
	{
		//  General Methods -------------------------------	
		public static async UniTask<Hero> Moralis_Create(StringBuilder outputText)
		{
			
			///////////////////////////////////////////
			// Execute: Create
			///////////////////////////////////////////
			Hero hero = Moralis.Create<Hero>();
			hero.Name = "Zuko";
			hero.Strength = 50;
			hero.Level = 15;
			hero.Warcry = "Honor!!!";
			hero.Bag.Add("Leather Armor");
			hero.Bag.Add("Crown Prince Hair clip.");
			await hero.SaveAsync();

			return hero;
		}

		public static async UniTask<List<Hero>> Moralis_Query(StringBuilder outputText)
		{
			///////////////////////////////////////////
			// Execute: Query
			///////////////////////////////////////////
			MoralisQuery<Hero> moralisQuery1 = await Moralis.Query<Hero>();
			MoralisQuery<Hero> moralisQuery2 =  moralisQuery1.WhereEqualTo("Level", 15);
			
			IEnumerable<Hero> result = await moralisQuery2.FindAsync();
			List<Hero> results = result.ToList();
			return results;
		}

		public static async UniTask<StringBuilder> Moralis_Delete(
			StringBuilder outputText, 
			Hero heroToDelete)
		{
			///////////////////////////////////////////
			// Execute: DeleteAsync
			///////////////////////////////////////////
			await Moralis.GetClient().DeleteAsync<Hero>(heroToDelete);
			
			// Display
			outputText.AppendBullet($"heroToDelete = {heroToDelete.objectId}");
			return outputText;
		}
	}
}
