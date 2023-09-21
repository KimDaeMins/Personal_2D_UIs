using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX : MonoBehaviour
{
    private Define.Scene _curSceneType = Define.Scene.Unknown;
    public Define.Scene CurrentSceneType
    {
        get
        {
            if (_curSceneType != Define.Scene.Unknown)
                return _curSceneType;
            return CurrentScene.SceneType;
        }
        set { _curSceneType = value; }
    }

    private GameObject _rootScene;
    public BaseScene CurrentScene { get { return _rootScene.GetComponent<BaseScene>(); } }

    public void Init()
    {
        _rootScene = GameObject.Find("@Scene");
    }

    public void ChangeScene(Define.Scene type)
    {
        Managers.Clear();

        _curSceneType = type;
        SceneManager.LoadScene(GetSceneName(type));
    }
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene) , type);
        return name;
    }
    public void Clear()
    {
        CurrentScene.Clear();
    }

    //오류있을거같은데이거
    //public string GetSceneName(Define.Scene type)
    //{
    //    string name = System.Enum.GetName(typeof(Define.Scene) , type);
    //    char[] letters = name.ToLower().ToCharArray();
    //    letters[0] = char.ToUpper(letters[0]);
    //    return new string(letters);
    //}
}
