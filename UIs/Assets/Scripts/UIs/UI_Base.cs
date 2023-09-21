using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//1. 아이콘은 앵커를 왼쪽위로둔다
//2. 아이콘의 피봇을 x= 0 y = 1로둔다 이건필수
//-------------------------------------------
//3. 뷰포트에 그리드패널을 두고 좌표를 미리 맞춰논다
//4. 가로갯수,아이콘크기  귀찮음이슈로 안넣었다 직접 넣어줘야한다
 
public abstract class UI_Base : MonoBehaviour
{
    Dictionary<Type , UnityEngine.Object[]> _objects = new Dictionary<Type , UnityEngine.Object[]>();

    public abstract void Init();

    public abstract void Refresh();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T) , objects);

        for (int i = 0 ; i < names.Length ; ++i)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject , names[i] , true);
            else
                objects[i] = Util.FindChild<T>(gameObject , names[i] , true);
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T) , out objects) == false)
            return null;

        return objects[idx] as T;
    }
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected TextMeshProUGUI GetText(int idx) { return Get<TextMeshProUGUI>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

    public static void BindEvent(GameObject go , Action<PointerEventData> action , Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            case Define.UIEvent.Pressed:
                evt.OnPressedHandler -= action;
                evt.OnPressedHandler += action;
                break;
            case Define.UIEvent.PointerUp:
                evt.OnPointerUpHandler -= action;
                evt.OnPointerUpHandler += action;
                break;
            case Define.UIEvent.PointerDown:
                evt.OnPointerDownHandler -= action;
                evt.OnPointerDownHandler += action;
                break;
        }
    }
}
