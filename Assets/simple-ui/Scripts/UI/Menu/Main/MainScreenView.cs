using System;
using System.Collections.Generic;
using simple_ui.Scripts.UI.Elements.SelectableElements;
using SimpleUI.EventChannels;
using UnityEngine;

namespace simple_ui.Scripts.UI.Menu
{
    public class MainScreenView : MenuView
    {
        [SerializeField] private MainMenuOpenMenuEventChannel openMenuEventChannel;
        [SerializeField] private MainMenuOpenPreviousMenuEventChannel openPreviousMenuEventChannel;
        [SerializeField] private List<UIElement>  uiElements = new();
        [SerializeField] private UIButton _playButton;
        [SerializeField] private UIButton _optionsButton;
        [SerializeField] private UIButton _quitButton;
        private int selectedElementIndex = 0;

        private void Start()
        {
            _playButton.ActivationAction += NavigateToPlay;
            _optionsButton.ActivationAction += NavigateToOptions;
            _quitButton.ActivationAction += Quit;
            uiElements[selectedElementIndex].Selected = true;
        }

        private void OnDestroy()
        {
            _playButton.ActivationAction -= NavigateToPlay;
            _optionsButton.ActivationAction -= NavigateToOptions;
            _quitButton.ActivationAction -= Quit;
        }

        private void SelectPreviousElement()
        {
            uiElements[selectedElementIndex].Selected = false;
            selectedElementIndex = selectedElementIndex == 0 ? uiElements.Count - 1 : selectedElementIndex - 1;
            uiElements[selectedElementIndex].Selected = true;
        }

        private void SelectNextElement()
        {
            uiElements[selectedElementIndex].Selected = false;
            selectedElementIndex = selectedElementIndex == uiElements.Count - 1 ? 0 : selectedElementIndex + 1;
            uiElements[selectedElementIndex].Selected = true;
        }

        public override string GetScreenName()
        {
            return MainMenuNames.MAIN_SCREEN;
        }
        
        public override void OnCancelCanceled()
        {
            if (CancelPressed)
            {
                openMenuEventChannel.Publish(MainMenuNames.TITLE_SCREEN);
            }
            base.OnCancelCanceled();
        }

        private void NavigateToPlay()
        {
            
        }
        
        private void NavigateToOptions()
        {
            
        }

        private void Quit()
        {
            
        }

        public override void OnMovePerformed(Vector2 movement)
        {
            base.OnMovePerformed(movement);
            if (movement.y > 0)
            {
                SelectPreviousElement();
            }
            else if (movement.y < 0)
            {
                SelectNextElement();
            }
        }
        

        public override void OnConfirmPerformed()
        {
            base.OnConfirmPerformed();
            uiElements[selectedElementIndex].Pressed = true;
        }

        public override void OnConfirmCanceled()
        {
            if (ConfirmPressed)
            {
                uiElements[selectedElementIndex].Pressed = false;
            }
            base.OnConfirmCanceled();
        }
    }
}