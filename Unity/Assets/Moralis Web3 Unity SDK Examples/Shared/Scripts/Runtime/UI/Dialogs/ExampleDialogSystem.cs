using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoralisUnity.Examples.Sdk.Shared
{
    /// <summary>
    /// Non-Reusable, custom implementation of the reusable
    /// <see cref="DialogSystem"/>
    /// </summary>
    [Serializable]
    public class ExampleDialogSystem : DialogSystem
    {
        //  Properties ---------------------------------------
    
        //  Fields ---------------------------------------
        [SerializeField] 
        private DialogUI _dialogUIPrefabWithEditableText = null;

        //  Constructor ---------------------------------------
        
        //  Other Methods ---------------------------------------
        
        /// <summary>
        /// Shows custom title/ and editable body. Has ok/cancel buttons
        /// </summary>
        public DialogUI ShowDialogBoxCustomEditableText(string titleText, string bodyText,
            Action logoutAction, 
            Action resetAction, 
            Action confirmationAction, 
            Action cancelAction)
        {
            return ShowDialogBox<DialogUI>(
                _dialogUIPrefabWithEditableText,
                titleText,
                bodyText,
                new List<DialogButtonData>
                {
                    new DialogButtonData(SharedConstants.LogOut, delegate
                    {
                        logoutAction();
                        HideDialogBox();
                    }),
                    new DialogButtonData(SharedConstants.DialogReset, delegate
                    {
                        // For reset, do not call HideDialogBox();
                        resetAction.Invoke();
                    }),
                    new DialogButtonData(SharedConstants.Ok, delegate
                    {
                        confirmationAction.Invoke();
                        HideDialogBox();
                    }),
                    new DialogButtonData(SharedConstants.Cancel, delegate
                    {
                        cancelAction.Invoke();
                        HideDialogBox();
                    })
                });
        }
    }
}