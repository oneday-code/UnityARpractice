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
        // 버튼의 onClick 델리게이트에 함수 연결하기
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
        // 버튼을 count 수만큼 클릭하면 버튼의 onClick() 델리게이트에 연결된 함수를 교체한다.
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
            //myEventHandler.Invoke("스크립트에서");
            //myEventHandler("스크립트에서");
        }
    }

    public void OnButtonClicked1()
    {
        print("버튼을 클릭했습니당~~~");
        num++;
    }

    public void OnButtonClicked2()
    {
        print("연결된 기능이 바뀌었습니다");
    }
    public void ButtonAction2()
    {
        print("나도 실행되죠~");
    }


}