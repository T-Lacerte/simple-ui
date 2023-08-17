using System;
using Game.Controller;
using Game.View;
using Game.ViewModel;
using Unity.VisualScripting;
using UnityEngine;

namespace Game{
    public class GameMain : MonoBehaviour
    {
        [SerializeField]
        private MainMenuViewController mainMenuViewController;

        void Awake()
        {
        }

        void OnDestroy()
        {
        }
    }
}
