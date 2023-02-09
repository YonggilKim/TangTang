using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using DG.Tweening;

public class UI_MainmenuPopup : UI_Popup
{
    #region Enums
    enum GameObjects
    {
        Battle,
        Shop,
        Equipement,
        Trials,
        Evolve,
        TopContainer,
        DownContainer,
    }

    enum Buttons
    {
        ShopButton,
        EquipButton,
        BattleButton,
        TrialsButton,
        EvolveButton,
        MsgBoxButton,
        SettingButton,
        PlayButton,
    }

    enum Texts
    {
        ShopName,
        EquipName,
        BattleName,
        TrialsName,
        EvolveName,
    }

    enum Images
    {
        ShopImg,
        EquipImg,
        BattleImg,
        TrialsImg,
        EvolveImg,
    }
    #endregion
    MainMenuTab _currentTab = MainMenuTab.Evolve;

    public void OnEnable()
    {
        Debug.Log("");
    }
    public override void Init()
    {
        base.Init();

        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        GetButton((int)Buttons.ShopButton).gameObject.BindEvent(() => ShowTab(MainMenuTab.Shop));
        GetButton((int)Buttons.EquipButton).gameObject.BindEvent(() => ShowTab(MainMenuTab.Equipment));
        GetButton((int)Buttons.BattleButton).gameObject.BindEvent(() => ShowTab(MainMenuTab.Battle));
        GetButton((int)Buttons.TrialsButton).gameObject.BindEvent(() => ShowTab(MainMenuTab.Trials));
        GetButton((int)Buttons.EvolveButton).gameObject.BindEvent(() => ShowTab(MainMenuTab.Evolve));

        GetButton((int)Buttons.MsgBoxButton).gameObject.BindEvent(OnClickMsgBox);
        GetButton((int)Buttons.SettingButton).gameObject.BindEvent(() => { });
        GetButton((int)Buttons.PlayButton).gameObject.BindEvent(() => { OnClickPlay(); });

        ShowTab(MainMenuTab.Battle);
    }

    public void RefreshUI()
    {
        ShowTab(_currentTab);
    }
    //TODO : 좀더 좋은방법 찾기
    public void ShowTab(MainMenuTab tab)
    {
        if (_currentTab == tab)
            return;
        _currentTab = tab;

        GetObject((int)GameObjects.Battle).gameObject.SetActive(false);
        GetObject((int)GameObjects.Shop).gameObject.SetActive(false);
        GetObject((int)GameObjects.Equipement).gameObject.SetActive(false);
        GetObject((int)GameObjects.Trials).gameObject.SetActive(false);
        GetObject((int)GameObjects.Evolve).gameObject.SetActive(false);

        GetText((int)Texts.BattleName).gameObject.SetActive(false);
        GetText((int)Texts.ShopName).gameObject.SetActive(false);
        GetText((int)Texts.EquipName).gameObject.SetActive(false);
        GetText((int)Texts.TrialsName).gameObject.SetActive(false);
        GetText((int)Texts.EvolveName).gameObject.SetActive(false);

        ReverseImage(GetImage((int)Images.BattleImg));
        ReverseImage(GetImage((int)Images.ShopImg));
        ReverseImage(GetImage((int)Images.EquipImg));
        ReverseImage(GetImage((int)Images.TrialsImg));
        ReverseImage(GetImage((int)Images.EvolveImg));

        //선택된 아이콘
        // (-3,-7.1,0) -> (0,53,0),w:132 h:116 -> w:249 h:218 tweening
        // name ON

        //선택된 아이콘 상자
        // imageName Layer1 => Color Fill 3 
        GetButton((int)Buttons.BattleButton).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Art/UI/User Interface/Layer 1");
        GetButton((int)Buttons.ShopButton).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Art/UI/User Interface/Layer 1");
        GetButton((int)Buttons.EquipButton).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Art/UI/User Interface/Layer 1");
        GetButton((int)Buttons.TrialsButton).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Art/UI/User Interface/Layer 1");
        GetButton((int)Buttons.EvolveButton).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Art/UI/User Interface/Layer 1");

        switch (_currentTab)
        {
            case MainMenuTab.Battle:
                GetObject((int)GameObjects.Battle).gameObject.SetActive(true);
                GetButton((int)Buttons.BattleButton).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Art/UI/User Interface/Color Fill 3");
                GetText((int)Texts.BattleName).gameObject.SetActive(true);
                FocuseImage(GetImage((int)Images.BattleImg));
                break;
            case MainMenuTab.Trials:
                GetObject((int)GameObjects.Trials).gameObject.SetActive(true);
                GetButton((int)Buttons.TrialsButton).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Art/UI/User Interface/Color Fill 3");
                GetText((int)Texts.TrialsName).gameObject.SetActive(true);
                FocuseImage(GetImage((int)Images.TrialsImg));
                break;
            case MainMenuTab.Equipment:
                GetObject((int)GameObjects.Equipement).gameObject.SetActive(true);
                GetButton((int)Buttons.EquipButton).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Art/UI/User Interface/Color Fill 3");
                GetText((int)Texts.EquipName).gameObject.SetActive(true);
                FocuseImage(GetImage((int)Images.EquipImg));
                break;
            case MainMenuTab.Evolve:
                GetObject((int)GameObjects.Evolve).gameObject.SetActive(true);
                GetButton((int)Buttons.EvolveButton).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Art/UI/User Interface/Color Fill 3");
                GetText((int)Texts.EvolveName).gameObject.SetActive(true);
                FocuseImage(GetImage((int)Images.EvolveImg));
                break;
            case MainMenuTab.Shop:
                GetObject((int)GameObjects.Shop).gameObject.SetActive(true);
                GetButton((int)Buttons.ShopButton).gameObject.GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Art/UI/User Interface/Color Fill 3");
                GetText((int)Texts.ShopName).gameObject.SetActive(true);
                FocuseImage(GetImage((int)Images.ShopImg));
                break;
        }


    }

    void FocuseImage(Image imgae, float delay = 0.3f)
    {
        imgae.rectTransform.DOAnchorPos(new Vector2(0, 53), delay);
        imgae.rectTransform.DOSizeDelta(new Vector2(249, 218), delay);
    }

    void ReverseImage(Image imgae, float delay = 0.3f)
    {
        imgae.rectTransform.DOAnchorPos(new Vector2(-3f, -7.1f), delay);
        imgae.rectTransform.DOSizeDelta(new Vector2(132, 116), delay);
    }
    #region OnClickEvent
    void OnClickMsgBox()
    {
        //TODO
        Debug.Log("Click Msg Box");
    }

    void OnClickSettingButton()
    {
        //TODO
    }

    void OnClickPlay()
    {
        //TODO
        Managers.Game.GameStart();
        // Game UI Open
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_PlayPopup>();
    }
    #endregion
}
