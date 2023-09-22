using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Main : UI_Scene
{
    CharacterStatsHandler _handler;
    enum Texts
    {
        NameText,
        LevelText,
        JobText,
        ExpText,
        DescriptionText,
        GoldText
    }
    enum Buttons
    {
        StatusButton,
        InventoryButton,
        ShopButton
    }
    enum Images
    {
        ExpBarImage,
    }
    protected override void Awake()
    {
        base.Awake();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        _handler = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatsHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {

        GetButton((int)Buttons.StatusButton).gameObject.BindEvent(OnClickedStatus);
        GetButton((int)Buttons.InventoryButton).gameObject.BindEvent(OnClickedInventory);
        GetButton((int)Buttons.ShopButton).gameObject.BindEvent(OnClickedShop);
        Refresh();
    }

    public override void Refresh()
    {
        //아 빨리끝내고 유아이매니저나 뜯어고치고싶다~ 라는 마음이 들어가서 이렇게 만들어졌네
        base.Refresh();
        GetText((int)Texts.NameText).text = _handler.CurrentStats._name;
        GetText((int)Texts.LevelText).text = _handler.CurrentStats._level.ToString();
        GetText((int)Texts.JobText).text = _handler.CurrentStats._job;
        GetText((int)Texts.ExpText).text = _handler.CurrentStats._exp.ToString() + "/" + _handler.CurrentStats._expMax.ToString();
        GetText((int)Texts.DescriptionText).text = _handler._description;
        GetText((int)Texts.GoldText).text = string.Format("{0:#,###}", _handler.CurrentStats._gold);

        GetImage((int)Images.ExpBarImage).fillAmount = ( _handler.CurrentStats._exp / _handler.CurrentStats._expMax );
    }


    public void OnClickedStatus(PointerEventData data)
    {
        UI_Popup pu = Managers.Instance.TogglePopupUI<UI_Popup>("UI_CharacterStat");
        pu.Refresh();
    }
    public void OnClickedInventory(PointerEventData data)
    {
        UI_Popup pu = Managers.Instance.TogglePopupUI<UI_Popup>("UI_Inventory");
        pu.Refresh();
    }
    public void OnClickedShop(PointerEventData data)
    {
        //Managers.Instance.TogglePopupUI<UI_Popup>("UI_StatusPopUp");
        Debug.Log("미구현");
    }
}
