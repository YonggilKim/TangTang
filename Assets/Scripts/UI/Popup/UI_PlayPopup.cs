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
    public float _spawnInterval = 0;
    public float _playTime = 0;
    public float _maxGameTime = 1 * 60f;

    public override void Init()
    {
        base.Init();

        BindText(typeof(Texts));
        BindImage(typeof(Images));
    }

    void Start()
    {
        Managers.Game.AddSkill(SkillType.Spinner);
        Managers.Game.AddSkill(SkillType.Commendation);
    }

    void Update()
    {
        _spawnInterval += Time.deltaTime;
        _playTime += Time.deltaTime;

        if (_playTime > _maxGameTime)
        {
            _playTime = _maxGameTime;
            //TODO
            //Popup Result;
            return;
        }

        if (_spawnInterval > Managers.Game.GetMonsterSpawnInterval())
        {
            Managers.Game.GenerateMonster();
            _spawnInterval = 0;
        }

    }
}
