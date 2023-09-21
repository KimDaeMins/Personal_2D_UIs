using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������Ÿ�� �̷��Ŵ� ���߿� �߰��Ѵٸ� ����� �߰��� �� �ִ� �κ��̴ϱ� �ϴ� �н�
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
