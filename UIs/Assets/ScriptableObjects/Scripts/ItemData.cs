using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템타입 이런거는 나중에 추가한다면 충분히 추가할 수 있는 부분이니까 일단 패스
public enum AddedStatType
{
    Hp,
    Att,
    Def,
    Critical,
}
[System.Serializable]
public class AddedStat
{
    public AddedStatType type;
    public float value;
    public Sprite icon;
}

[CreateAssetMenu(fileName = "ItemData", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public Sprite icon;
    public int gold;

    [Header("Added")]
    public AddedStat[] addedStats;
}
