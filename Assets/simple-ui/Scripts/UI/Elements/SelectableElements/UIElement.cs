using System;
using UnityEngine;

namespace simple_ui.Scripts.UI.Elements.SelectableElements
{
    public abstract class UIElement : MonoBehaviour
    {
        private bool _selected;
        private bool _pressed;
        public Action ActivationAction { get; set; }

        public bool Selected
        {
            get => _selected;
            set
            {
                if(_selected)
                    OnSelect();
                else
                    OnDeselect();
                
                _selected = value;
            }
        }
        
        public bool Pressed
        {
            get => _pressed;
            set
            {
                if(_pressed)
                    OnPress();
                else
                    OnDepress();
                
                _pressed = value;
            }
        }
        
        protected virtual void OnSelect(){}
        protected virtual void OnDeselect(){}
        
        protected virtual void OnPress(){}
        protected virtual void OnDepress(){}
    }
}