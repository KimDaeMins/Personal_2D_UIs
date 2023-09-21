using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollItem : UI_Base
{
    enum Texts
    {
        RankText
    }
    public override void Init()
    {
        
    }

    void Awake()
    {
        Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
    }
    public void SetRank(int rank)
    {
        GetText((int)Texts.RankText).text = rank.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Refresh()
    {
        throw new System.NotImplementedException();
    }
}
