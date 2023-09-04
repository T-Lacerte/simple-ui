using UnityEngine;
using UnityEngine.InputSystem;

namespace simple_ui.Scripts.UI.Menu
{
    public abstract class MenuView : MonoBehaviour
    {
        public abstract void OnConfirmPerformed();
        public abstract void OnConfirmCanceled();
        public abstract void OnCancelPerformed();
        public abstract void OnCancelCanceled();
        public abstract void OnMovePerformed(Vector2 movement);
        public abstract void OnMoveCanceled();
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}