using System;
using System.Collections.Generic;
using simple_ui.Scripts.UI.Menu;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace SimpleUI.Core
{
    public class MenuManager : PersistentSingleton<MenuManager>
    {
        private InputActions.MainMenuActions _mainMenuActions;

        private Stack<MenuView> _openedMenus;
        private MenuView _focusedView;

        protected override void Awake()
        {
            base.Awake();
            _mainMenuActions = new InputActions().MainMenu;
            _openedMenus = new Stack<MenuView>();
        }

        protected void Start()
        {
            _mainMenuActions.Confirm.performed += OnConfirmPerformed;
            _mainMenuActions.Confirm.canceled += OnConfirmCanceled;
            _mainMenuActions.Cancel.performed += OnCancelPerformed;
            _mainMenuActions.Cancel.canceled += OnCancelCanceled;
            _mainMenuActions.Move.performed += OnMovePerformed;
            _mainMenuActions.Move.canceled += OnMoveCanceled;
        }

        protected void OnDestroy()
        {
            _mainMenuActions.Confirm.performed -= OnConfirmPerformed;
            _mainMenuActions.Confirm.canceled -= OnConfirmCanceled;
            _mainMenuActions.Cancel.performed -= OnCancelPerformed;
            _mainMenuActions.Cancel.canceled -= OnCancelCanceled;
            _mainMenuActions.Move.performed -= OnMovePerformed;
            _mainMenuActions.Move.canceled -= OnMoveCanceled;
        }

        protected void OnEnable()
        {
            _mainMenuActions.Enable();
        }

        protected void OnDisable()
        {
            _mainMenuActions.Disable();
        }

        private void OnConfirmPerformed(InputAction.CallbackContext context)
        {
            if (_focusedView != null)
                _focusedView.OnConfirmPerformed();
        }
        
        private void OnConfirmCanceled(InputAction.CallbackContext context)
        {
            if (_focusedView != null)
                _focusedView.OnConfirmCanceled();
        }

        private void OnCancelPerformed(InputAction.CallbackContext context)
        {
            if (_focusedView != null)
                _focusedView.OnCancelPerformed();
        }
        
        private void OnCancelCanceled(InputAction.CallbackContext context)
        {
            if (_focusedView != null)
                _focusedView.OnCancelCanceled();
        }
        
        
        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            if (_focusedView == null)
                return;
            var value = context.ReadValue<Vector2>();
            _focusedView.OnMovePerformed(value);
        }
        
        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            if (_focusedView == null)
                return;
            _focusedView.OnMoveCanceled();
        }

        private void OpenMenu(MenuView menu)
        {
            if (menu != null)
                _focusedView.Hide();
            menu.Show();
            _focusedView = menu;
        }

        private void CloseMenu()
        {
            
        }

        private void CloseAll()
        {
            _openedMenus.Clear();
            _focusedView = null;
        }
        
    }
}