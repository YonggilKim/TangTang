using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingFaces : MonoBehaviour
{
    float _valueX = -0.015f;
    float _valueY = -0.015f;
    RawImage _rawImage;
    //해골모양이 움직이게
    private void Start()
    {
        _rawImage= GetComponent<RawImage>();
    }
    void Update()
    {
        _rawImage.uvRect = new Rect(this._rawImage.uvRect.position + new Vector2(_valueX, _valueY) * Time.deltaTime, _rawImage.uvRect.size);
    }
}
