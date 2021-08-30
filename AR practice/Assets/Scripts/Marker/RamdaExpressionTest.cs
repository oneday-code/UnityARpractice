using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RamdaExpressionTest : MonoBehaviour
{
    delegate void myDele();
    myDele deleTest;

    Func<int, int, int> calculator;


    void Start()
    {
        //TestFunction();

        // 기존의 함수 연결 방식
        //deleTest = () =>
        //{
        //    TestFunction(3);
        //    TestFunction(7);
        //};

        //// 람다식으로 연결한 방식
        //deleTest += () => print("무명으로 연결되었다");

        //deleTest();


        calculator = Add;
        int result = calculator(5, 5);

        calculator = (num1, num2) => num1 * num2;
        result = calculator(5, 5);
        print(result);
    }

    void Update()
    {

    }

    void TestFunction(int num)
    {
        print("매개 변수 :" + num);
    }

    int Add(int a, int b)
    {
        return a + b;
    }

    int Multiply(int a, int b)
    {
        return a * b;
    }
}