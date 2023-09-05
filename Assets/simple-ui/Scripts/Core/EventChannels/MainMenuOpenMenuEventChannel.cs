using System;
using UnityEngine;

namespace SimpleUI.EventChannels
{
    public class MainMenuOpenMenuEventChannel : MonoBehaviour
    {
        public event OpenMenuEvent OnOpenMenu;

        public void Publish(string menuName)
        {
            OnOpenMenu?.Invoke(menuName);
        }
    }

    public delegate void OpenMenuEvent(string menuName);
}