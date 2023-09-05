using System;
using UnityEngine;

namespace SimpleUI.EventChannels
{
    public class MainMenuOpenPreviousMenuEventChannel : MonoBehaviour
    {
        public event OpenPreviousMenuEvent OnOpenPreviousMenu;

        public void Publish()
        {
            OnOpenPreviousMenu?.Invoke();
        }
    }

    public delegate void OpenPreviousMenuEvent();
}