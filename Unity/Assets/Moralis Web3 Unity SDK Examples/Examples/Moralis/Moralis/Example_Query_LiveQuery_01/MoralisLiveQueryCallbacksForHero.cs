using MoralisUnity.Examples.Sdk.Shared;
using UnityEngine;

#pragma warning disable CS1998
namespace MoralisUnity.Examples.Sdk.Example_Query_LiveQuery_01	
{
	/// <summary>
	/// Example: Live Query 
	/// </summary>
	public class MoralisLiveQueryCallbacksForHero : MoralisLiveQueryCallbacks<Hero>
	{
		//  Properties ------------------------------------
		
		//  Fields ----------------------------------------
		private const string Title = "Callbacks";
		
		//  Constructor Methods ---------------------------
		public MoralisLiveQueryCallbacksForHero()
		{
			// Here is example syntax for other callbacks

			/*
			OnConnectedEvent += () =>
			{
				Debug.Log($"OnConnectedEvent()");
			};
			
			OnCreateEvent += (item, id) =>
			{
				Debug.Log($"OnCreateEvent() item={item}, id={id}");
			};

			OnDeleteEvent += (item, id) =>
			{
				Debug.Log($"OnDeleteEvent() item={item}, id={id}");
			};

			OnEnterEvent += (item, id) =>
			{
				Debug.Log($"OnEnterEvent() item={item}, id={id}");
			};
			
			OnErrorEvent += evt =>
			{
				Debug.Log($"OnErrorEvent() evt={evt}");
			};

			OnSubscribedEvent += (id) =>
			{
				Debug.Log($"OnSubscribedEvent() id={id}");
			};
			
			OnUnsubscribedEvent += id =>
			{
				Debug.Log($"OnUnsubscribedEvent() id={id}");
			};
			*/

		}

		//  General Methods -------------------------------	
		
		//  Event Handlers --------------------------------
	}
}
