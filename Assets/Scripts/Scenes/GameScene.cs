﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        //Managers.UI.ShowPopupUI<UI_TitlePopup>();

    }

    public override void Clear()
    {
        
    }
}
