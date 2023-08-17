using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Model
{
    public class ObservableVariable<T>
    {
        private T _value;
        public event Action<T> OnValueChanged;
        public T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value)) return;
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }
    }
}