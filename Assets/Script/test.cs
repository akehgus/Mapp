using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{

    public Canvas canvas = null;
    public RectTransform targetRect = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, targetRect.position);
        Vector3 result = Vector3.zero;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(targetRect, screenPos, canvas.worldCamera, out result);
        transform.position = result;
    }
}
