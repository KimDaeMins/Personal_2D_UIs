using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    
    protected virtual void Awake()
    {
        Init();
    }
    public override void Init()
    {
        Managers.Instance.SetCanvas(this , true);
    }

    public virtual void ClosePopupUI()
    {
        Managers.Instance.ClosePopupUI(this);
    }

    public override void Refresh()
    {
    }
}
