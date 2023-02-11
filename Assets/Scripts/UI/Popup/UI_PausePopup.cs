using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define;

public class UI_PausePopup : UI_Popup
{
    #region Enums
    enum GameObjects
    {
        ItemContainer,
    }

    enum Buttons
    {
        Resume
    }
    #endregion

    public void Awake()
    {
        Managers.UI.SetCanvas(gameObject, true);
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.Resume).gameObject.BindEvent(OnClickResume);
    }

    private void Start()
    {
        for (int i = 0; i < Managers.Game.CurrentSkills.Count; i++)
        {
            MakeSubItem(i, Managers.Game.CurrentSkills[i]);
        }
    }

    void MakeSubItem(int index, SkillType skillType)
    {
        GameObject container = GetObject((int)GameObjects.ItemContainer);

        foreach (Transform child in container.transform)
            Managers.Resource.Destroy(child.gameObject);

        List<SkillType> list = Managers.Game.CurrentSkills.ToList();

        foreach (SkillType skill in list)
        {
            UI_PauseSkillItem item = Managers.UI.MakeSubItem<UI_PauseSkillItem>(container.transform);
            item.GetComponent<UI_PauseSkillItem>().SetUI(skill);
        }
    }

    public void OnClickResume()
    {
        Managers.UI.ClosePopupUI();
    }
}
