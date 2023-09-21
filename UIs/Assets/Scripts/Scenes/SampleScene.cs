using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScene : BaseScene
{
    void Start()
    {
    }
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.SampleScene;

        Managers.Instance.ShowSceneUI<UI_Main>();
    }
    public override void Clear()
    {

    }
}
