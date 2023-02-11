using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class UI_SkillItem : UI_Base
{
    #region Enums
    enum GameObjects
    {
    }

    enum Buttons
    {
        UI_SkillItem
    }

    enum Texts
    {
        DescText
    }

    enum Images
    {
        SkillIcon,
    }
    #endregion

    private SkillType _skillType;

    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        GetButton((int)Buttons.UI_SkillItem).gameObject.BindEvent(OnClicked);
        RefreshUI();

    }

    void RefreshUI()
    {
        //icon
        switch (_skillType)
        {
            case SkillType.Commendation:
                GetImage((int)Images.SkillIcon).sprite = Managers.Resource.Load<Sprite>("Art/bullet/commendation/commendation");
                GetText((int)Texts.DescText).text = "Commendation";
                break;
            case SkillType.Tree:
                GetImage((int)Images.SkillIcon).sprite = Managers.Resource.Load<Sprite>("Art/bullet/tree/tree");
                GetText((int)Texts.DescText).text = "Tree";
                break;
            case SkillType.Ball:
                GetImage((int)Images.SkillIcon).sprite = Managers.Resource.Load<Sprite>("Art/bullet/ball/ball");
                GetText((int)Texts.DescText).text = "Ball";
                break;
            case SkillType.Drone:
                GetImage((int)Images.SkillIcon).sprite = Managers.Resource.Load<Sprite>("Art/bullet/drone/DroneA");
                GetText((int)Texts.DescText).text = "Drone";
                break;
            case SkillType.Firebomb:
                GetImage((int)Images.SkillIcon).sprite = Managers.Resource.Load<Sprite>("Art/bullet/firebomb/firebomb");
                GetText((int)Texts.DescText).text = "FireBomb";
                break;
            case SkillType.Spinner:
                GetImage((int)Images.SkillIcon).sprite = Managers.Resource.Load<Sprite>("Art/bullet/spinner/spinner");
                GetText((int)Texts.DescText).text = "Spinner";
                break;
            default:
                break;
        }
    }
    public void SetUI(SkillType type)
    {
        _skillType = type;
    }

    public void OnClicked()
    {
        Managers.Game.AddSkill(_skillType);
        Managers.UI.ClosePopupUI();
    }

}
