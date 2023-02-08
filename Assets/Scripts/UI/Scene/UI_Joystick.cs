using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UI_Joystick : UI_Scene
{
    //TODO
    //public GameObject ArrowDirecteur;
    //public GameObject Gun;
    private GameObject _handler;
    private GameObject _joystickBG;
    public Vector2 JoystickVec { get; set; }
    private Vector2 _joystickTouchPos;
    private Vector2 _joystickOriginalPos;
    private float _joystickRadius;

    enum GameObjects
    {
        JoystickBG,
        Handler,
    }

    public override void Init()
    {
        base.Init();
        Debug.Log("@>> UI_Joystick Init()..");
        BindObject(typeof(GameObjects));
        _handler = GetObject((int)GameObjects.Handler);
        _joystickBG = GetObject((int)GameObjects.JoystickBG);

        _joystickOriginalPos = _joystickBG.transform.position;
        _joystickRadius = _joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
        gameObject.BindEvent(PointerDown, Define.UIEvent.PointerDown);
        gameObject.BindEvent(PointerUp, Define.UIEvent.PointerUp);
        gameObject.BindEvent(Drag, Define.UIEvent.Drag);
    }


    void Update()
    {
        if (JoystickVec.x == 0 && JoystickVec.y == 0)
        {
            //ArrowDirecteur.SetActive(false);
        }
        else
        {
            //ArrowDirecteur.SetActive(true);
            //ArrowDirecteur.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-joystickVec.x, joystickVec.y) * 180 / Mathf.PI);
        }
        //if (Gun.activeSelf == true)
        //{
        //    Gun.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-joystickVec.x, joystickVec.y) * 180 / Mathf.PI);
        //}
    }

    public void PointerDown()
    {
        _handler.transform.position = Input.mousePosition;
        _joystickBG.transform.position = Input.mousePosition;
        _joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragePos = pointerEventData.position;
        JoystickVec = (dragePos - _joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragePos, _joystickTouchPos);
        if (joystickDist < _joystickRadius)
        {
            _handler.transform.position = _joystickTouchPos + JoystickVec * joystickDist;
        }
        else
        {
            _handler.transform.position = _joystickTouchPos + JoystickVec * _joystickRadius;
        }
    }

    public void PointerUp()
    {
        JoystickVec = Vector2.zero;
        _handler.transform.position = _joystickOriginalPos;
        _joystickBG.transform.position = _joystickOriginalPos;
    }
}
