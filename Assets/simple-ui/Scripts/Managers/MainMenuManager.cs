using System;
using System.Collections.Generic;
using System.Linq;
using simple_ui.Scripts.UI.Menu;
using SimpleUI.EventChannels;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace SimpleUI.Core
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private List<MenuView> _menuViews = new();
        [SerializeField] private MenuView _initialMenu;
        [SerializeField] private MainMenuOpenMenuEventChannel openMenuEventChannel;
        [SerializeField] private MainMenuOpenPreviousMenuEventChannel openPreviousMenuEventChannel;
        private Stack<MenuView> _openedMenus = new();
        
        private MenuView _focusedView;
        
        private InputActions.MainMenuActions _mainMenuActions;
        protected void Awake()
        {
            _mainMenuActions = new InputActions().MainMenu;
        }

        protected void Start()
        {
            _mainMenuActions.Confirm.performed += OnConfirmPerformed;
            _mainMenuActions.Confirm.canceled += OnConfirmCanceled;
            _mainMenuActions.Cancel.performed += OnCancelPerformed;
            _mainMenuActions.Cancel.canceled += OnCancelCanceled;
            _mainMenuActions.Start.performed += OnStartPerformed;
            _mainMenuActions.Start.canceled += OnStartCanceled;
            _mainMenuActions.Move.performed += OnMovePerformed;
            _mainMenuActions.Move.canceled += OnMoveCanceled;
            openMenuEventChannel.OnOpenMenu += OpenMenu;
            openPreviousMenuEventChannel.OnOpenPreviousMenu += OpenPreviousMenu;
            _focusedView = _initialMenu;
            _initialMenu.Show();
        }

        protected void OnDestroy()
        {
            _mainMenuActions.Confirm.performed -= OnConfirmPerformed;
            _mainMenuActions.Confirm.canceled -= OnConfirmCanceled;
            _mainMenuActions.Cancel.performed -= OnCancelPerformed;
            _mainMenuActions.Cancel.canceled -= OnCancelCanceled;
            _mainMenuActions.Start.performed -= OnStartPerformed;
            _mainMenuActions.Start.canceled -= OnStartCanceled;
            _mainMenuActions.Move.performed -= OnMovePerformed;
            _mainMenuActions.Move.canceled -= OnMoveCanceled;
            openMenuEventChannel.OnOpenMenu -= OpenMenu;
            openPreviousMenuEventChannel.OnOpenPreviousMenu -= OpenPreviousMenu;
        }

        protected void OnEnable()
        {
            _mainMenuActions.Enable();
        }

        protected void OnDisable()
        {
            _mainMenuActions.Disable();
        }
        
        protected void OnStartPerformed(InputAction.CallbackContext context)
        {
            if (_focusedView != null)
                _focusedView.OnStartPerformed();
        }
        
        protected void OnStartCanceled(InputAction.CallbackContext context)
        {
            if (_focusedView != null)
                _focusedView.OnStartCanceled();
        }
        
        protected void OnConfirmPerformed(InputAction.CallbackContext context)
        {
            if (_focusedView != null)
                _focusedView.OnConfirmPerformed();
        }
        
        protected void OnConfirmCanceled(InputAction.CallbackContext context)
        {
            if (_focusedView != null)
                _focusedView.OnConfirmCanceled();
        }

        protected void OnCancelPerformed(InputAction.CallbackContext context)
        {
            if (_focusedView != null)
                _focusedView.OnCancelPerformed();
        }
        
        protected void OnCancelCanceled(InputAction.CallbackContext context)
        {
            if (_focusedView != null)
                _focusedView.OnCancelCanceled();
        }
        
        
        protected void OnMovePerformed(InputAction.CallbackContext context)
        {
            if (_focusedView == null)
                return;
            var value = context.ReadValue<Vector2>();
            _focusedView.OnMovePerformed(value);
        }
        
        protected void OnMoveCanceled(InputAction.CallbackContext context)
        {
            if (_focusedView == null)
                return;
            _focusedView.OnMoveCanceled();
        }

        protected void OpenMenu(string menuName)
        {
            var menu = _menuViews.First(x => x.GetScreenName() == menuName);
            _focusedView.Hide();
            menu.Show();
            _focusedView = menu;
            _openedMenus.Push(menu);
        }

        protected void OpenPreviousMenu()
        {
            var menu = _openedMenus.Pop();
            _focusedView = _openedMenus.Peek();
            _focusedView.Show();
            menu.Hide();

        }

        protected void CloseAll()
        {
            _openedMenus.Clear();
            _focusedView = null;
        }
    }
}