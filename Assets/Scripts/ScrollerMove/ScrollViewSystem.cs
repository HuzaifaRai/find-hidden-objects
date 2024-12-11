using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSystem : MonoBehaviour
{
    private ScrollRect _scrollRect;
    [SerializeField] private ButtonScroll _topButton;
    [SerializeField] private ButtonScroll _bottomButton;

    [SerializeField] private float ScrollSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_topButton != null)
        {
            if (_topButton.isDown)
            {
                ScrollTop();
            }
        }
        if (_bottomButton != null)
        {
            if (_bottomButton.isDown)
            {
                ScrollBottom();
            }
        }
    }

    private void ScrollTop()
    {
        if (_scrollRect != null)
        {
            if (_scrollRect.verticalNormalizedPosition <= 1f)
            {
                _scrollRect.verticalNormalizedPosition += ScrollSpeed;
            }
        }
    }
    private void ScrollBottom()
    {
        if (_scrollRect != null)
        {
            if (_scrollRect.verticalNormalizedPosition >= 0f)
            {
                _scrollRect.verticalNormalizedPosition -= ScrollSpeed;
            }
        }
    }
}
