using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UI_PauseSkillItem : UI_Base
{
    #region Enums
    enum Images
    {
        SkillIcon,
    }
    #endregion
    private SkillType _skillType;
    
    public void SetUI(SkillType type)
    {
        _skillType = type;
    }

    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, true);
        BindImage(typeof(Images));

        RefreshUI();
    }

    void RefreshUI()
    {
        //icon
        switch (_skillType)
        {
            case SkillType.Commendation:
                GetImage((int)Images.SkillIcon).sprite = Managers.Resource.Load<Sprite>("Art/bullet/commendation/commendation");
                break;
            case SkillType.Tree:
                GetImage((int)Images.SkillIcon).sprite = Managers.Resource.Load<Sprite>("Art/bullet/tree/tree");
                break;
            case SkillType.Ball:
                GetImage((int)Images.SkillIcon).sprite = Managers.Resource.Load<Sprite>("Art/bullet/ball/ball");
                break;
            case SkillType.Drone:
                GetImage((int)Images.SkillIcon).sprite = Managers.Resource.Load<Sprite>("Art/bullet/drone/DroneA");
                break;
            case SkillType.Firebomb:
                GetImage((int)Images.SkillIcon).sprite = Managers.Resource.Load<Sprite>("Art/bullet/firebomb/firebomb");
                break;
            case SkillType.Spinner:
                GetImage((int)Images.SkillIcon).sprite = Managers.Resource.Load<Sprite>("Art/bullet/spinner/spinner");
                break;
            default:
                break;
        }
    }


}
