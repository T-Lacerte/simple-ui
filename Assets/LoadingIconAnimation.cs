using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingIconAnimation : MonoBehaviour
{

    private bool fading;
    [SerializeField]
    private Image _image;
    [SerializeField][Range(0.01f, 2.0f)] private float _fadeSpeed;
    [SerializeField][Range(0.01f, 1.0f)] private float _minimumAlpha;
    // Start is called before the first frame update
    void Start()
    {
        fading = true;
    }

    // Update is called once per frame
    void Update()
    {
        var color = _image.color;
        var delta = _fadeSpeed * Time.deltaTime;
        if (fading)
        {
            color.a -= delta;
            if (color.a <= _minimumAlpha)
            {
                color.a = _minimumAlpha;
                fading = false;
            }
        }
        else
        {
            color.a += delta;
            if (color.a >= 1f)
            {
                color.a = 1f;
                fading = true;
            }
        }
        _image.color = color;
    }
}
