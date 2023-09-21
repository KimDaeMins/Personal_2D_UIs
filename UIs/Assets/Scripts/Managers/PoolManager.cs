using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Queue<Poolable> _poolStack = new Queue<Poolable>();
        public void Init(GameObject original , int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{Original.name}_Root";

            for (int i = 0 ; i < count ; i++)
            {
                Push(Create());
            }
        }

        Poolable Create()
        {
            GameObject go = Managers.Instance.Load<GameObject>($"Prefabs/{Original.name}");
            go = Object.Instantiate(go);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.SetParent(Root);
            poolable.gameObject.SetActive(false);

            _poolStack.Enqueue(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Dequeue();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);
            poolable.transform.SetParent(parent);


            return poolable;
        }

        public Poolable Pop(Vector3 pos , Quaternion q , Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Dequeue();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);
            poolable.transform.SetParent(parent);
            poolable.transform.position = pos;
            poolable.transform.rotation = q;

            return poolable;
        }
    }
    #endregion


    Dictionary<string , Pool> _pool = new Dictionary<string , Pool>();

    Transform _root;
    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void CreatePool(GameObject original , int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original , count);
        pool.Root.parent = _root;

        _pool.Add(original.name , pool);
    }
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original , Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);

        return _pool[original.name].Pop(parent);
    }
    public Poolable Pop(GameObject original , Vector3 pos , Quaternion q , Transform parent)
    {
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);

        return _pool[original.name].Pop(pos , q , parent);
    }

    public T GetOriginal<T>(string path) where T : Object
    {
        string name = path;
        int index = name.LastIndexOf('/');

        if (index >= 0)
            name = name.Substring(index + 1);

        if (_pool.ContainsKey(name) == false)
            return null;

        return _pool[name].Original as T;
    }

    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }
}
