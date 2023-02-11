using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UI_PlayPopup : UI_Popup
{
    #region Enums
    enum GameObjects
    {
    }

    enum Buttons
    {
        PauseBtn
    }

    enum Texts
    {
        CoinValue,
        KillValue,
        Timer,
        ExpValue,
    }

    enum Images
    {
        FillingImg,
    }
    #endregion

    PlayerController _player;

    GameManager _game;
    public override void Init()
    {
        Debug.Log("Init");
        //TODO 이부분 로직 왜 안타는지 연구 
    }

    private void Awake()
    {
        base.Init();

        _game = Managers.Game;
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.PauseBtn).gameObject.BindEvent(OnClickPause);
        Managers.Game.OnPlayerDataUpdated = OnPlayerDataUpdated;
        Managers.Game.OnMonsterDataUpdated = OnOnMonsterDataUpdated;
        Managers.Game.OnPlayerLevelUp = OnPlayerLevelUp;
    }
    void Start()
    {
        _game.AddSkill(SkillType.Commendation);
    }

    void Update()
    {
        if (Managers.UI.PeekPopupUI<UI_PlayPopup>() != this)
        {
            Time.timeScale = 0;
            return;
        }
        else 
        {
            Time.timeScale = 1;
        }

        _game.SpawnInterval += Time.deltaTime;
        _game.PlayTime += Time.deltaTime;
        RefreshTime();
        if (_game.PlayTime > _game.MaxGameTime)
        {
            _game.PlayTime = _game.MaxGameTime;
            //TODO
            //Popup Result;
            return;
        }

        if (_game.SpawnInterval > _game.GetMonsterSpawnInterval())
        {
            _game.GenerateMonster();
            _game.SpawnInterval = 0;
        }
    }

    void OnPlayerDataUpdated()
    {
        GetImage((int)Images.FillingImg).fillAmount = (float)_game.PlayerExp / _game.PlayerTotalExp;
    }

    void OnOnMonsterDataUpdated()
    {
        GetText((int)Texts.KillValue).text = $"{_game.NumDeadMonsters}";
    }

    void RefreshTime()
    {
        GetText((int)Texts.Timer).text = $"{TimeSpan.FromSeconds(_game.PlayTime).ToString(@"mm\:ss")}";
    }

    void OnPlayerLevelUp()
    {
        //TODO
        if (_game.CanUpgradeSkill == true)
            Managers.UI.ShowPopupUI<UI_SelectSkillPopup>();

        GetImage((int)Images.FillingImg).fillAmount = (float)_game.PlayerExp / _game.PlayerTotalExp;
        GetText((int)Texts.ExpValue).text = $"{_game.PlayerLevel}";

    }
    PlayerController GetPlayer()
    {
        return _game.Player;
    }
    public void OnClickPause()
    {
        Managers.UI.ShowPopupUI<UI_PausePopup>();
    }
}
