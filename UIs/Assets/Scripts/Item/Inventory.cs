using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemSlot
{
    public Item item;
}

public class Inventory : MonoBehaviour
{
    public ItemSlot[] _itemSlots;
    public int SlotLength { get { return _itemSlots.Length; } }
    //public UI_Item[] _UI_items;

    private static Inventory s_Instance;
    public static Inventory Instance { get => s_Instance; }

    private void Awake()
    {
        s_Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("InvenSize" , 120);
        _itemSlots = new ItemSlot[PlayerPrefs.GetInt("InvenSize")];
        for (int i = 0 ; i < _itemSlots.Length ; ++i)
        {
            _itemSlots[i] = new ItemSlot();
        }

        Item item = new Item();
        item.TestCreate();
        _itemSlots[0].item = item;
    }

    public int GetNowItemCount()
    {
        int count = 0;
        for(int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].item != null)
            {
                ++count;
            }
        }
        return count;
    }
    private void UpdateUI()
    {
        //�̺κ��� ���� ������ �κ��丮�� �����̰� �ƴϴϱ�!
        //for (int i = 0 ; i < _itemSlots.Length ; ++i)
        //{
        //    if (_itemSlots[i].item != null)
        //    {
        //        _UI_items[i].Set(_itemSlots[i]);
        //    }
        //    else
        //    {
        //        _UI_items[i].Clear();
        //    }
        //}
    }
    public void AddItem(ItemData itemData)
    {

        ItemSlot emptySlot = GetEmptySlot();
        if (emptySlot != null)
        {
            emptySlot.item = new Item();
            emptySlot.item._data = itemData;
            emptySlot.item.InitSetting();
            UpdateUI();
            return;
        }
    }

    private ItemSlot GetEmptySlot()
    {
        for (int i = 0 ; i < _itemSlots.Length ; ++i)
        {
            if (_itemSlots[i].item == null)
            {
                return _itemSlots[i];
            }
        }
        return null;
    }
}