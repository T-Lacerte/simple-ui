using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace simple_ui.Scripts.UI.Menu
{
    public abstract class MenuView : MonoBehaviour
    {
        protected bool StartPressed { get; private set; } = false;
        protected bool ConfirmPressed { get; private set; } = false;
        protected bool CancelPressed { get; private set; } = false;
        
        public abstract string GetScreenName();
        public virtual void OnStartPerformed()
        {
            StartPressed = true;
        }

        public virtual void OnStartCanceled()
        {
            StartPressed = false;
        }

        public virtual void OnConfirmPerformed()
        {
            ConfirmPressed = true;
        }

        public virtual void OnConfirmCanceled()
        {
            ConfirmPressed = false;
        }

        public virtual void OnCancelPerformed()
        {
            CancelPressed = true;
        }

        public virtual void OnCancelCanceled()
        {
            CancelPressed = false;
        }

        public virtual void OnMovePerformed(Vector2 movement)
        {
            
        }

        public virtual void OnMoveCanceled()
        {
            
        }
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}