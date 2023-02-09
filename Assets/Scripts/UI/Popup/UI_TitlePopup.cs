using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_TitlePopup : UI_Popup
{
    enum Texts
    {
        StartButton,
    }

    public override void Init()
    {
        base.Init();
        Debug.Log("@>> UI_TitlePopup Init()..");
        BindText(typeof(Texts));

        Managers.Sound.Clear();
        Managers.Sound.Play("tangtang/bg/MainBG", Sound.Bgm);

        Text startButton = GetText((int)Texts.StartButton);

        startButton.gameObject.BindEvent(OnClickStart);
        startButton.GetComponent<Text>().DOFade(0, 0.8f).SetLoops(-1, LoopType.Yoyo) ;
    }

    void OnClickStart()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_MainmenuPopup>();
    }
}
