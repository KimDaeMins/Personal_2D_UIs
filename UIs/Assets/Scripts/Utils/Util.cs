using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    public static string GetDefineName<T>(T type)
    {
        return System.Enum.GetName(typeof(T) , type);
    }
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }
    public static GameObject FindChild(GameObject go , string name = null , bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go , name , recursive);
        if (transform == null)
            return null;
        return transform.gameObject;
    }
    public static T FindChild<T>(GameObject go , string name = null , bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (!recursive)
        {
            for (int i = 0 ; i < go.transform.childCount ; ++i)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    if(transform.TryGetComponent(out T component))
                    {
                        return component;
                    }
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static GameObject FindRoot(GameObject go , string name = null)
    {
        Transform transform = FindRoot<Transform>(go , name);
        if (transform == null)
            return null;
        return transform.gameObject;
    }
    public static T FindRoot<T>(GameObject go , string name = null) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        Transform parentTransform = go.transform;

        while (parentTransform != null)
        {
            if (string.IsNullOrEmpty(name) || parentTransform.name == name)
            {
                if (parentTransform.TryGetComponent(out T component))
                {
                    return component;
                }
            }
            parentTransform = parentTransform.parent;
        }

        return null;
    }
    public static void InputRoot(string name , GameObject go)
    {
        GameObject root = GameObject.Find(name);
        if (root == null)
        {
            root = new GameObject { name = name };
        }

        go.transform.SetParent(root.transform);

        return;
    }
}
