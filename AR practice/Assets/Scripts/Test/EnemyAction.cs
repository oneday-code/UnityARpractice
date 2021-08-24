using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    DelegateTest manager;

    void Start()
    {
        //DelegateTest.instance.myDelegate += DestroyMySelf;

        manager = FindObjectOfType<DelegateTest>();
        manager.myDelegate += DestroyMySelf;
    }

    void DestroyMySelf()
    {
        print(gameObject.name);
        //DelegateTest.instance.myDelegate -= DestroyMySelf;
        manager.myDelegate -= DestroyMySelf;
        Destroy(gameObject);
    }

    void Update()
    {

    }
}