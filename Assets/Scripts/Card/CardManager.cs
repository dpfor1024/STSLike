using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Assertions;

public class CardManager : MonoBehaviour
{
    public GameObject CardPrefab;
    public int Size;

    private readonly Stack<GameObject> _instances = new Stack<GameObject>();


    public void Awake()
    {
        Assert.IsNotNull(CardPrefab);
    }

    public void Init()
    {
        for (int i = 0; i < Size; i++) { 
            var obj=CreateInstance();
            obj.SetActive(false);
            _instances.Push(obj);
        }
    }


    private GameObject CreateInstance()
    {
        return Instantiate(CardPrefab,transform,true);
    }


    public GameObject GetInstance() {
    
        var obj =_instances.Count>0?_instances.Pop():CreateInstance();
        obj.SetActive(true);
        return obj;
    }
}
