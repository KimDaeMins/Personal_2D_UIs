using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject orgScrollItem;
    public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0 ; i < 1500 ; ++i)
        {
            Managers.Instance.Instantiate(orgScrollItem.name , parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
