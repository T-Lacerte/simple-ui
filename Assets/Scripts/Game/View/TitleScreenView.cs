using System;
using System.Collections;
using Game.Utils;
using Game.ViewModel;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.View
{
    public class TitleScreenView : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField]
        private TMP_Text text;
        
        [FormerlySerializedAs("delay")]
        [Header("Configuration")]
        [SerializeField]
        [Range(0.1f, 1.0f)]
        [Tooltip("Time between each change on the text's visibility")]
        [OnChangedCall(nameof(UpdateDelay))] float textBlinkDelay = 0.5f;

        private TitleScreenViewModel _viewModel;
        private WaitForSecondsRealtime _waitForSecondsRealtime;
        private Coroutine _runningCoroutine;

        private void Start()
        {
            _viewModel = new TitleScreenViewModel();
            _viewModel.TextVisibility.OnValueChanged += OnTextVisibilityChanged;
            _waitForSecondsRealtime = new WaitForSecondsRealtime(textBlinkDelay);
            
            Debug.Log("Starting title screen text blink coroutine");
            _runningCoroutine = StartCoroutine(TextBlinkCoroutine()); 
        }

        private void OnEnable()
        {
            if (_viewModel != null)
            {
                Debug.Log("Starting title screen text blink coroutine");
                _runningCoroutine = StartCoroutine(TextBlinkCoroutine()); 
            }
        }

        private void OnDisable()
        {
            Debug.Log("Ending title screen text blink coroutine");
            StopCoroutine(_runningCoroutine);
        }
        IEnumerator TextBlinkCoroutine()
        {
            Debug.Log("Title screen text blink coroutine started");
            while(true)
            {
                yield return _waitForSecondsRealtime;
                _viewModel.ToggleTextVisibility();
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private void OnDestroy()
        {
            _viewModel.TextVisibility.OnValueChanged -= OnTextVisibilityChanged;
        }

        private void OnTextVisibilityChanged(bool value)
        {
            text.enabled = value;
        }

        public void UpdateDelay()
        {
            _waitForSecondsRealtime = new WaitForSecondsRealtime(textBlinkDelay);
        }
        //TODO: handling controller / keyboard input
        //Maybe a good start (for UI buttons at least):
        //https://www.what-could-possibly-go-wrong.com/bringing-mvvm-to-unity-part-2-property-and-event-bindings/
    }
}