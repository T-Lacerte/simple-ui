using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Game.Model;
using UnityEngine;

namespace Game.ViewModel
{
    public class TitleScreenViewModel : BaseViewModel
    {
        public ObservableVariable<bool> TextVisibility { get; } = new();

        public void ToggleTextVisibility()
        {
            TextVisibility.Value = !TextVisibility.Value;
        }
    }
}