using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static Define;

public class UI_SelectSkillPopup : UI_Popup
{
    #region Enums
    enum GameObjects
    {
        Container,
    }

    enum Buttons
    {
    }

    enum Texts
    {
    }

    enum Images
    {
        yellow_icon_1,
        yellow_icon_2,
        yellow_icon_3,
        yellow_icon_4,
        yellow_icon_5,
        yellow_icon_6,
    }
    #endregion

    private void Awake()
    {
        Managers.UI.SetCanvas(gameObject, true);
        BindImage(typeof(Images));
        BindObject(typeof(GameObjects));
    }

    void Start()
    {
        SetCanUpgradeSkill();

        for(int i =0; i< Managers.Game.CurrentSkills.Count; i++) 
        {
            SetCurrentSkill(i, Managers.Game.CurrentSkills[i]);
        }
    }

    void SetCanUpgradeSkill()
    {
        GameObject container = GetObject((int)GameObjects.Container);

        foreach (Transform child in container.transform)
            Managers.Resource.Destroy(child.gameObject);

        List<SkillType> list = Managers.Game.GetCanUpgradeSkills();

        foreach (SkillType skill in list)
        {
            UI_SkillItem item = Managers.UI.MakeSubItem<UI_SkillItem>(container.transform);
            item.GetComponent<UI_SkillItem>().SetUI(skill);
        }
    }

    void SetCurrentSkill(int index, SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.Commendation:
                GetImage(index).sprite = Managers.Resource.Load<Sprite>("Art/bullet/commendation/commendation");
                break;
            case SkillType.Tree:
                GetImage(index).sprite = Managers.Resource.Load<Sprite>("Art/bullet/tree/tree");
                break;
            case SkillType.Ball:
                GetImage(index).sprite = Managers.Resource.Load<Sprite>("Art/bullet/ball/ball");
                break;
            case SkillType.Drone:
                GetImage(index).sprite = Managers.Resource.Load<Sprite>("Art/bullet/drone/DroneA");
                break;
            case SkillType.Firebomb:
                GetImage(index).sprite = Managers.Resource.Load<Sprite>("Art/bullet/firebomb/firebomb");
                break;
            case SkillType.Spinner:
                GetImage(index).sprite = Managers.Resource.Load<Sprite>("Art/bullet/spinner/spinner");
                break;
            default:
                break;
        }
        GetImage(index).enabled = true;

    }
}
