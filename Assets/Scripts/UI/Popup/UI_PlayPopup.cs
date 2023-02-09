using Data;
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
    }

    enum Texts
    {
        CoinValue,
        KillValue,
        Timer,
        ExpValue,
        EvolveName,
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

    void OnPlayerDataUpdated()
    {
        
    }
    private void Awake()
    {
        base.Init();

        _game = Managers.Game;
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        Managers.Game.OnPlayerDataUpdated = OnPlayerDataUpdated;
    }
    void Start()
    {
        _game.AddSkill(SkillType.Spinner);
        _game.AddSkill(SkillType.Commendation);
    }

    void Update()
    {
        _game.SpawnInterval += Time.deltaTime;
        _game.PlayTime += Time.deltaTime;

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

    PlayerController GetPlayer()
    {
        return _game.Player;
    }
}
