using Cysharp.Threading.Tasks;
using MoralisUnity.Examples.Sdk.Shared.Data.Types.Storage;
using MoralisUnity.Sdk.Exceptions;
using MoralisUnity.Sdk.Interfaces;
using MoralisUnity.Sdk.Utilities;
using UnityEngine;

namespace MoralisUnity.Examples.Sdk.Shared
{
   /// <summary>
   /// Displays "Authenticate" button or the current "0x..." address
   /// </summary>
   public class ExampleAuthenticationUI : MonoBehaviour , IInitializableAsync
   {
      
      //  Events  ---------------------------------------
      [HideInInspector]
      public ExampleAuthenticationUIStateUnityEvent OnStateChanged = new ExampleAuthenticationUIStateUnityEvent();
      
      [HideInInspector]
      public StringUnityEvent OnActiveAddressChanged = new StringUnityEvent();

      
      //  Properties  -----------------------------------
      public bool IsInitialized { get { return _isInitialized;} }
      
      /// <summary>
      /// Members can call this to ensure "Are we initialized?"
      /// </summary>
      void IInitializableAsync.RequireIsInitialized()
      {
         if (!_isInitialized)
         {
            throw new InitializationRequiredException(this);
         }
      }
      
      public ExampleButton Button { get { return _button; } }

      public ExampleAuthenticationUIState State
      {
         get
         {
            return _state;
         }
         private set
         {
            _state = value;
            OnStateChanged.Invoke(_state);
         }
      }
      
      public bool IsVisible
      {
         get
         {
            return _canvasGroup.GetIsVisible();
         }
         set
         {
            _canvasGroup.SetIsVisible(value);
         }
      }
      
      public bool IsInteractable
      {
         get
         {
            return _canvasGroup.interactable;
         }
         set
         {
            _canvasGroup.interactable = value;
         }
      }
      
      //Wrap API for easier use by customers in root example scripts
      public bool HasActiveAddress { get { return !string.IsNullOrEmpty(ActiveAddress);}}
      
      //Wrap API for easier use by customers in root example scripts
      public string ActiveAddress
      {
         get
         {
            (this as IInitializableAsync).RequireIsInitialized();
            return ExampleRuntimeStorage.Instance.ActiveAddress;
         }
         set
         {
                ExampleRuntimeStorage.Instance.ActiveAddress = value;
         }
      }
      
      //Wrap API for easier use by customers in root example scripts
      public async UniTask<string> ResetActiveAddress()
      {
         (this as IInitializableAsync).RequireIsInitialized();
         return await ExampleRuntimeStorage.Instance.ResetActiveAddress();
      }
      
      
      //  Fields  ---------------------------------------
      [SerializeField] 
      private ExampleButton _button = null;
      
      [SerializeField] 
      private CanvasGroup _canvasGroup = null;
      private ExampleAuthenticationUIState _state = ExampleAuthenticationUIState.None;

      private bool _isInitialized = false;
      
      
      //  Unity Methods  --------------------------------

      
      //  General Methods  --------------------------------
      public async UniTask InitializeAsync()
      {
         if (_isInitialized)
         {
            throw new InitializedAlreadyException(this);
         }

         ExampleRuntimeStorage.Instance.OnActiveAddressChanged.AddListener(ExampleManager_OnActiveAddressChanged);
         OnStateChanged.AddListener(This_OnStateChanged);
         
         // Trigger refresh
         if (ExampleRuntimeStorage.Instance.HasActiveAddress)
         {
            ExampleManager_OnActiveAddressChanged(ExampleRuntimeStorage.Instance.ActiveAddress);
         }
         else
         {
            await ExampleRuntimeStorage.Instance.ResetActiveAddress();
         }
         _isInitialized = true;
      }
      
      
      //  Event Handlers  --------------------------------
      private async void ExampleManager_OnActiveAddressChanged(string address)
      {
         
         if (await SharedHelper.HasMoralisUser())
         {
            State = ExampleAuthenticationUIState.Authenticated;
         }
         else
         {
            State = ExampleAuthenticationUIState.NotAuthenticated;
         }
         
         OnActiveAddressChanged.Invoke(address);
      }
      
      
      private void This_OnStateChanged(ExampleAuthenticationUIState state)
      {
         switch (State)
         {
            case ExampleAuthenticationUIState.Authenticated:
               _button.Text.text = SharedFormatter.GetWeb3AddressShortFormat(ExampleRuntimeStorage.Instance.ActiveAddress);
               break;
            case ExampleAuthenticationUIState.NotAuthenticated:
               _button.Text.text = SharedConstants.Authenticate;
               break;
            default:
               SwitchDefaultException.Throw(State);
               break;
         }
      }
   }
}


