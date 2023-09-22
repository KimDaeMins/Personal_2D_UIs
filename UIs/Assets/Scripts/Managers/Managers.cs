using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance { get { Init(); return s_instance; } }

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();
    PoolManager _pool = new PoolManager();
    SceneManagerEX _scene = new SceneManagerEX();
    SoundManager _sound = new SoundManager();

    void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@GameManager");
            if (go == null)
            {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
            s_instance._scene.Init();
            s_instance._sound.Init();
        }
    }
    public static void Clear()
    {
        s_instance._scene.Clear();
        s_instance._sound.Clear();
        s_instance._ui.Clear();
    }

    #region Resource & Pool
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            T go = _pool.GetOriginal<T>(path);
            if (go != null)
                return go;
        }   
        return _resource.Load<T>(path);
    }
    public GameObject GetOriginal(string path)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Failed to load original : {path}");
            return null;
        }
        return original;
    }
    public GameObject Instantiate(string path , Vector3 pos)
    {
        return Instantiate(path , pos , Quaternion.identity, null);
    }
    public GameObject Instantiate(string path , Vector3 pos , Vector3 rotation)
    {
        return Instantiate(path , pos , Quaternion.Euler(rotation) , null);
    }
    public GameObject Instantiate(string path , Transform parent = null)
    {
        GameObject original = GetOriginal(path);

        if (original.TryGetComponent<Poolable>(out Poolable p))
            return _pool.Pop(original , parent).gameObject;

        return _resource.Instantiate(original , parent);
    }
    public GameObject Instantiate(string path , Vector3 pos , Quaternion q , Transform parent = null)
    {
        GameObject original = GetOriginal(path);

        if (original.TryGetComponent<Poolable>(out Poolable p))
            return _pool.Pop(original , pos , q , parent).gameObject;

        return _resource.Instantiate(original , pos , q , parent);
    }

    public void Destroy(GameObject go , float t = 0.0f)
    {
        if (go == null)
            return;

        if (go.TryGetComponent<Poolable>(out Poolable p))
        {
            _pool.Push(p);
            return;
        }

        _resource.Destroy(go , t);
    }
    public void Destroy(MonoBehaviour mob , float t = 0.0f)
    {
        _resource.Destroy(mob, t);
    }
    #endregion

    #region Scene

    public Define.Scene GetSceneType()
    {
        return _scene.CurrentSceneType;
    }
    public BaseScene GetScene()
    {
        return _scene.CurrentScene;
    }
    public void ChangeScene(Define.Scene type)
    {
        _scene.ChangeScene(type);
    }
    #endregion

    #region Sound
    public AudioSource GetBgm()
    {
        return _sound.GetCurrent();
    }
    public void Play(string path , Define.Sound type = Define.Sound.Effect , float pitch = 1.0f)
    {
        _sound.Play(path, type, pitch);
    }

    public void Play(AudioClip audioClip , Define.Sound type = Define.Sound.Effect , float pitch = 1.0f)
    {
        _sound.Play(audioClip , type , pitch);
    }
    #endregion

    #region UI
    public void OpenPopupRefresh()
    {
        _ui.OpenPopupRefresh();
    }
    public UI_Scene GetSceneUI()
    {
        return _ui.SceneUI;
    }
    public void SetCanvas(GameObject go , bool sort = true)
    {
        _ui.SetCanvas(go , sort);
    }
    public void SetCanvas(UI_Popup popup , bool sort = true)
    {
        _ui.SetCanvas(popup , sort);
    }
    public T MakeWorldSpaceUI<T>(Transform parent = null , string name = null) where T : UI_Base
    {
        return _ui.MakeWorldSpaceUI<T>(parent , name);
    }
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        return _ui.ShowSceneUI<T>(name);
    }

    public T TogglePopupUI<T>(string name = null) where T : UI_Popup
    {
        return _ui.TogglePopupUI<T>(name);
    }
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        return _ui.ShowPopupUI<T>(name);
    }
    public void TogglePopupUI<T>(T popup) where T : UI_Popup
    {
        _ui.TogglePopupUI<T>(popup);
    }
    public T FindPopup<T>(string name = null) where T : UI_Popup
    {
        return _ui.FindPopup<T>(name);
    }
    public T GetLastPopupUI<T>() where T : UI_Popup
    {
        return _ui.GetLastPopupUI<T>();
    }
    public void HidePopupUI(UI_Popup popup = null)
    {
        _ui.HidePopupUI(popup);
    }
    public void ClosePopupUI(UI_Popup popup = null)
    {
        _ui.ClosePopupUI(popup);
    }
    public void CloseAllPopupUI()
    {
        _ui.CloseAllPopupUI();
    }
    #endregion
}
