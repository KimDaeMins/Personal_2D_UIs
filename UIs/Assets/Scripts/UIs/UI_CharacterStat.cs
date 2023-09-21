using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CharacterStat : UI_Popup
{
    private CharacterStatsHandler _handler;
    enum Texts
    {
       AttackValueText,
       DefValueText,
       HpValueText,
       CriticalValueText
    }
    enum Buttons
    {
        QuitButton
    }

    protected override void Awake()
    {
        base.Awake();
        //원래이렇게 안하는데.. 시힘귀...
        _handler = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatsHandler>();

        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
    }

    void Start()
    {
        GetButton((int)Buttons.QuitButton).gameObject.BindEvent(OnCllickedQuitButton);
    }

    public void OnCllickedQuitButton(PointerEventData data)
    {
        Managers.Instance.TogglePopupUI(this);
    }
    public override void Refresh()
    {
        base.Refresh();


        GetText((int)Texts.AttackValueText).text = _handler.CurrentStats._att.ToString();
        GetText((int)Texts.DefValueText).text = _handler.CurrentStats._def.ToString();
        GetText((int)Texts.HpValueText).text = _handler.CurrentStats._hp.ToString(); 
        GetText((int)Texts.CriticalValueText).text = _handler.CurrentStats._critical.ToString();

    }

}
