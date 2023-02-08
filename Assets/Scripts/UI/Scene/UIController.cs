using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using static Define;

public class UIController : UI_Popup
{
    enum GameObjects
    {
        Intro,
        MainMenu,
        InGame
    }

    enum LobbyTexts
    {
        StartButton,
    }

    private GameObject _objIntro;
    private GameObject _objMainmenu;
    private GameObject _objInGame;
    private GameObject Audio;


    #region LifeCycle
    void Start()
    {
        //Init();
    }

    public override void Init()
    {
        base.Init();
        //Managers.UI.SetCanvas(GetObject((int)LobbyUIObjects.Intro), true);
        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(LobbyTexts));

        _objIntro = GetObject((int)GameObjects.Intro);
        _objMainmenu = GetObject((int)GameObjects.MainMenu);
        _objInGame = GetObject((int)GameObjects.InGame);

        Managers.Sound.Play("tangtang/bg/MainBG", Sound.Bgm);

        Text startButton = GetText((int)LobbyTexts.StartButton);

        startButton.gameObject.BindEvent(OnStartButtonClicked);
        startButton.GetComponent<Text>().DOFade(0, 0.8f).SetLoops(-1, LoopType.Yoyo);
    }
    #endregion

    private void OnStartButtonClicked()
    {
        Debug.Log("Clicked");
    }

}
