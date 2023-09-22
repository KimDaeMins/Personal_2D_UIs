using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemInfo : UI_Popup
{
    private Item _curItem;
    private UI_Item _curItemUI;

    enum Images
    {
        ItemImage,
        StatIcon
    }
    enum Texts
    {
        ItemText,
        ItemDescriptionText,
        ActionButtonText,
        ActionText,
        ValueText,
        OptionText,
    }
    enum Buttons
    {
        CancelButton,
        ActionButton,
        SellButton
    }

    protected override void Awake()
    {
        base.Awake();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
    }
    protected void Start()
    {
        GetButton((int)Buttons.ActionButton).gameObject.BindEvent(OnClickedActionButton);
        GetButton((int)Buttons.CancelButton).gameObject.BindEvent(OnClickedCancelButton);
        GetButton((int)Buttons.SellButton).gameObject.BindEvent(OnClickedSellButton);
    }


    public void OnClickedActionButton(PointerEventData data)
    {
        //�� ���⼭ ó���ϱ� ��¥ ������ ��... ����1�þ�..
        if (_curItem.IsEquip)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatsHandler>().RemoveStatModifier(_curItem._stat);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatsHandler>().AddStatModifier(_curItem._stat);
        }
        _curItem.IsEquip = !_curItem.IsEquip;
        _curItemUI.Refresh();
        _curItem = null;
        _curItemUI = null;

        
        Managers.Instance.TogglePopupUI(this);
    }
    public void OnClickedCancelButton(PointerEventData data)
    {
        _curItem = null;
        _curItemUI = null;
        Managers.Instance.TogglePopupUI(this);
    }

    public void OnClickedSellButton(PointerEventData data)
    {
        _curItemUI.SellItem();
        _curItem = null;
        _curItemUI = null;
        Managers.Instance.TogglePopupUI(this);
    }
    public override void Refresh()
    {
        if (_curItem == null)
            return;

        GetImage((int)Images.ItemImage).sprite = _curItem._data.icon;

        GetText((int)Texts.ItemText).text = _curItem._data.displayName;
        GetText((int)Texts.ItemDescriptionText).text = _curItem._data.description;
        if (_curItem.IsEquip)
        {
            GetText((int)Texts.ActionButtonText).text = "Remove";
            GetText((int)Texts.ActionText).text = "���� �Ͻðڽ��ϱ�??";
        }
        else
        {
            GetText((int)Texts.ActionButtonText).text = "Confirm";
            GetText((int)Texts.ActionText).text = "���� �Ͻðڽ��ϱ�??";
        }

        GetText((int)Texts.OptionText).text = "";
        GetText((int)Texts.ValueText).text = "";
        foreach (var p in _curItem._addedStats)
        {
            switch (p.Key)
            {
                case AddedStatType.Hp:
                    GetText((int)Texts.OptionText).text += "|ü��|";
                    break;
                case AddedStatType.Att:
                    GetText((int)Texts.OptionText).text += "|���ݷ�|";
                    break;
                case AddedStatType.Def:
                    GetText((int)Texts.OptionText).text += "|����|";
                    break;
                case AddedStatType.Critical:
                    GetText((int)Texts.OptionText).text += "|ũ��Ƽ��|";
                    break;
                default:
                    break;
            }
            GetText((int)Texts.ValueText).text += "|" + p.Value.ToString() + "|";
            //�̰ǰ����
            GetImage((int)Images.StatIcon).sprite = _curItem._data.addedStats[0].icon;
        }
    }

    public void Refresh(Item item, UI_Item itemUI)
    {
        _curItem = item;
        _curItemUI = itemUI;
        Refresh();
    }
}
