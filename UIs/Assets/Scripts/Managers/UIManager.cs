using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    int _order = 10;

    LinkedList<UI_Popup> _popupList = new LinkedList<UI_Popup>();
    public UI_Scene SceneUI { get; private set; }

    private GameObject _root;
    public GameObject Root
    {
        get
        {
            if (_root == null)
            {
                _root = GameObject.Find("@UI_Root");
                if (_root == null)
                    _root = new GameObject { name = "@UI_Root" };
            }
            return _root;
        }
    }
    private void SetSortOrder(GameObject go, bool sort)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }
    public void SetCanvas(GameObject go , bool sort = true)
    {
        UI_Popup popup = Util.FindRoot<UI_Popup>(go);

        if (popup == null)
        {
            SetSortOrder(go , sort);
            return;
        }

        SetCanvas(popup , sort);
    }
    public void SetCanvas(UI_Popup popup , bool sort = true)
    {
        SetSortOrder(popup.gameObject , sort);

        _popupList.Remove(popup);
        _popupList.AddLast(popup);
        popup.gameObject.SetActive(true);
    }
    public T MakeSubItem<T>(string name = null , Transform parent = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Instance.Instantiate($"UI/SubItem/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        return go.GetOrAddComponent<T>();
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null , string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Instance.Instantiate($"UI/WorldSpace/{name}", parent);
        //if (parent != null)
        //    go.transform.SetParent(parent);

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return go.GetOrAddComponent<T>();
    }
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Instance.Instantiate($"UI/Scene/{name}");

        T scene = Util.GetOrAddComponent<T>(go);
        SceneUI = scene;

        go.transform.SetParent(Root.transform);

        return scene;
    }
    public T TogglePopupUI<T>(string name = null) where T : UI_Popup
    {
        T popup = FindPopup<T>(name);
        if (popup == null)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = typeof(T).Name;
            }

            GameObject go = Managers.Instance.Instantiate($"UI/Popup/{name}");

            popup = Util.GetOrAddComponent<T>(go);
        }

        popup.transform.SetParent(Root.transform);

        return popup;
    }
    public void TogglePopupUI<T>(T popup) where T : UI_Popup
    {
        if(popup.gameObject.activeSelf)
        {
            HidePopupUI(popup);
        }
        else
        {
            SetCanvas(popup);
        }
    }
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {

        T popup = FindPopup<T>(name);
        if (popup == null)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = typeof(T).Name;
            }

            GameObject go = Managers.Instance.Instantiate($"UI/Popup/{name}");

            popup = Util.GetOrAddComponent<T>(go);
            _popupList.AddLast(popup);
        }
        else
        {
            SetCanvas(popup);
        }

        popup.transform.SetParent(Root.transform);

        return popup;
    }
    public T FindPopup<T>(string name = null) where T : UI_Popup
    {
        LinkedListNode<UI_Popup> currentNode = _popupList.Last;
        while(currentNode != null)
        {
            UI_Popup ui = currentNode.Value;
            if (ui == null)
                break;
            if ((string.IsNullOrEmpty(name) && ui.GetType() == typeof(T)) || ui.name == name)
            {
                if (ui.gameObject.activeSelf)
                    return ui as T;
            }
            currentNode = currentNode.Previous;
        }
        return null;
    }
    public T GetLastPopupUI<T>()where T : UI_Popup
    {
        if (_popupList.Count == 0)
            return null;

        return _popupList.Last.Value as T;
    }
    public void OpenPopupRefresh()
    {
        LinkedListNode<UI_Popup> currentNode = _popupList.Last;
        while (currentNode != null)
        {
            UI_Popup ui = currentNode.Value;
            if (ui == null)
                break;

            if (ui.gameObject.activeSelf)
            {
                ui.Refresh();
            }
            currentNode = currentNode.Previous;
        }
    }
    public void ClosePopupUI(UI_Popup popup = null)
    {
        if (_popupList.Count == 0)
            return;

        if (popup != null)
        {
            DestroyPopupUI(popup);
            return;
        }

        popup = _popupList.Last.Value;
        DestroyPopupUI(popup);
        _order--;
    }
    public void HidePopupUI(UI_Popup popup)
    {
        popup.gameObject.SetActive(false);
    }

    private void DestroyPopupUI(UI_Popup popup)
    {
        _popupList.Remove(popup);
        Managers.Instance.Destroy(popup.gameObject);
    }
    public void CloseAllPopupUI()
    {
        _popupList.Clear();
        _order = 10;
    }

    public void Clear()
    {
        CloseAllPopupUI();
        SceneUI = null;
        _root = null;
    }
}
