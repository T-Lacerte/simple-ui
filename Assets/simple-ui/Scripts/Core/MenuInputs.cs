using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace SimpleUI
{
    public class MenuInputs : PersistentSingleton<MenuInputs>
    {
        private InputActions.MainMenuActions _mainMenuActions;
        public event Action OnConfirm;
        public event Action OnCancel;
        public event Action<Vector2> OnMovement;

        protected override void Awake()
        {
            base.Awake();
            _mainMenuActions = new InputActions().MainMenu;
        }

        private void Start()
        {
            _mainMenuActions.Move.performed += OnMovePerformed;
            _mainMenuActions.Confirm.performed += OnConfirmPerformed;
            _mainMenuActions.Cancel.performed += OnCancelPerformed;
        }

        private void OnDestroy()
        {
            _mainMenuActions.Move.performed -= OnMovePerformed;
            _mainMenuActions.Confirm.performed -= OnConfirmPerformed;
            _mainMenuActions.Cancel.performed -= OnCancelPerformed;
        }

        private void OnEnable()
        {
            _mainMenuActions.Enable();
        }

        private void OnDisable()
        {
            _mainMenuActions.Disable();
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            OnMovement?.Invoke(value);
        }
        private void OnConfirmPerformed(InputAction.CallbackContext context) => OnConfirm?.Invoke();
        private void OnCancelPerformed(InputAction.CallbackContext context) => OnCancel?.Invoke();
    }
}
