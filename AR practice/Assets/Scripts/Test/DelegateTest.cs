using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DelegateTest : MonoBehaviour
{
    public delegate void DeleTest();
    public event DeleTest myDelegate;

    public delegate int CalculateDele(int a, int b);
    CalculateDele myCalc;

    //delegate void EventCall(Collision collision);
    //EventCall myEventHandler;

    //public static DelegateTest instance;

    //private void Awake()
    //{
    //    if(instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }

    //}


    public Action myAction;

    delegate void MyNewDelegate(string msg);
    MyNewDelegate newDele;

    Action<string> myAction2;
    Action<string, int, int> myAction3;

    Func<int, int, int> myFunc;
    Func<int, float, string> myFunc2;

    void Start()
    {
        //myDelegate = PrintMyName;

        // 델리게이트에 함수를 할당하기
        myCalc = Add;
        myCalc += Multiply;
        myCalc += Test;

        // 콜백 연결 예시
        //myEventHandler = OnMyCollisionEnter;

        // Action 타입 델리게이트
        myAction = PrintMyName;
        newDele = PrintYourName;
        myAction2 = PrintYourName;
        myAction3 = RARARA;

        // Func 타입 델리게이트
        myFunc = Add;
        myFunc2 = FUNFUN;

        TestFunction(Add, 8);
        TestFunction(Multiply, 2);
    }

    void TestFunction(Func<int, int, int> myFunc, int num)
    {
        int result = myFunc(5, 10);
        result += num;
        print(result);
    }





    string FUNFUN(int a, float b)
    {

        return "";
    }

    void RARARA(string a, int b, int c)
    {

    }

    void PrintMyName()
    {
        print(gameObject.name);
    }

    void PrintYourName(string name)
    {
        print(name);
    }


    int Add(int num1, int num2)
    {
        int result = num1 + num2;
        //print("더한 값: " + result);
        return result;
    }

    int Multiply(int num1, int num2)
    {
        int result = num1 * num2;
        //print("곱한 값: " + result);
        return result;
    }

    int Test(int a, int b)
    {
        print("테스트야! " + a + b);
        return 0;
    }

    // 콜백될 함수
    //void OnMyCollisionEnter(Collision collision)
    //{

    //}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (myDelegate != null)
            {
                myDelegate();
            }

            // 델리게이트를 실행한다.
            //myCalc(5, 10);
        }

        // 콜백 실행 예시
        //if(Input.GetMouseButtonDown(1))
        //{
        //    Collision col = new Collision();
        //    myEventHandler(col);
        //}
    }
}