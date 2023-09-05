using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace simple_ui.Scripts.UI.Elements.SelectableElements
{
    public class UIButton : UIElement
    {
        private Button _button;
        private void Start()
        {
            _button = gameObject.GetComponent<Button>();
        }

        protected override void OnSelect()
        {
            _button.Select();
        }

        protected override void OnDeselect()
        {
        }

        protected override void OnPress()
        {
        }

        protected override void OnDepress()
        {
            ActivationAction?.Invoke();
        }
    }
}