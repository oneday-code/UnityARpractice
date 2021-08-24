using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PlayerAction : MonoBehaviour
{
    public Button myButton;
    //public UnityEvent<string> myEventHandler;
    //Action<string> myEventHandler;
    int num = 0;

    void Start()
    {
        // ��ư�� onClick ��������Ʈ�� �Լ� �����ϱ�
        //myButton.onClick.AddListener(OnButtonClicked);
        //myButton.onClick.RemoveListener(OnButtonClicked);
        //myButton.onClick.RemoveAllListeners();
        //myButton.onClick.AddListener(ButtonAction2);
        //myEventHandler = OnButtonClicked2;

        myButton.onClick.AddListener(OnButtonClicked1);

        StartCoroutine(ChangeButtonFunction(3));
    }

    IEnumerator ChangeButtonFunction(int count)
    {
        // ��ư�� count ����ŭ Ŭ���ϸ� ��ư�� onClick() ��������Ʈ�� ����� �Լ��� ��ü�Ѵ�.
        yield return new WaitUntil(CheckClickCount);

        myButton.onClick.RemoveListener(OnButtonClicked1);
        myButton.onClick.AddListener(OnButtonClicked2);
    }

    bool CheckClickCount()
    {
        return num >= 3;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //myEventHandler.Invoke("��ũ��Ʈ����");
            //myEventHandler("��ũ��Ʈ����");
        }
    }

    public void OnButtonClicked1()
    {
        print("��ư�� Ŭ���߽��ϴ�~~~");
        num++;
    }

    public void OnButtonClicked2()
    {
        print("����� ����� �ٲ�����ϴ�");
    }
    public void ButtonAction2()
    {
        print("���� �������~");
    }


}