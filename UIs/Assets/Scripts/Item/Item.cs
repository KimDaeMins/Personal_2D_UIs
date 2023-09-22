using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemData _data;
    public Stats _stat;
    public bool IsEquip { get; set; }
    public Dictionary<AddedStatType , float> _addedStats = new Dictionary<AddedStatType , float>();
    
    public void TestCreate(int DataCount)
    {
        _data = Managers.Instance.Load<ItemData>("ScriptableObjects/Datas/Test_ItemData"+DataCount.ToString());
        InitSetting();
    }
    public void InitSetting()
    {
        _stat = new Stats();

        for(int i = 0 ; i < _data.addedStats.Length ; ++i)
        {
            float value = _data.addedStats[i].value;
            switch (_data.addedStats[i].type)
            {
                case AddedStatType.Hp:
                    _stat._hp = value;
                    DictionarySetting(AddedStatType.Hp , value);
                    break;
                case AddedStatType.Att:
                    _stat._att = value;
                    DictionarySetting(AddedStatType.Att , value);
                    break;
                case AddedStatType.Def:
                    _stat._def = value;
                    DictionarySetting(AddedStatType.Def , value);
                    break;
                case AddedStatType.Critical:
                    _stat._critical = value;
                    DictionarySetting(AddedStatType.Critical , value);
                    break;
            }
        }
    }

    private void DictionarySetting(AddedStatType type,float value)
    {
        _addedStats.TryGetValue(type , out float currentValue);
        currentValue += value;
        _addedStats[type] = currentValue;
    }
}
