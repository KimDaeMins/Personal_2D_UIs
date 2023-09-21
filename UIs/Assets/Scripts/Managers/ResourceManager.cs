using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(GameObject original , Transform parent = null)
    {
        GameObject go = Object.Instantiate(original , parent);
        go.name = original.name;

        return go;
    }
    public GameObject Instantiate(GameObject original , Vector3 pos , Quaternion q , Transform parent = null)
    {
        GameObject go;
        if (parent == null)
            go = Object.Instantiate(original , pos , q);
        else
            go = Object.Instantiate(original , pos , q , parent);
        go.name = original.name;

        return go;
    }

    public void Destroy(GameObject go , float t = 0.0f)
    {
        Object.Destroy(go , t);
        go = null;
    }
    public void Destroy(MonoBehaviour mob , float t = 0.0f)
    {
        Destroy(mob.gameObject , t);
        mob = null;
    }
}
