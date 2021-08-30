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

        // ������ �Լ� ���� ���
        //deleTest = () =>
        //{
        //    TestFunction(3);
        //    TestFunction(7);
        //};

        //// ���ٽ����� ������ ���
        //deleTest += () => print("�������� ����Ǿ���");

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
        print("�Ű� ���� :" + num);
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