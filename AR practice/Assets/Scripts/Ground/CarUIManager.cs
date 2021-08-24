using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car_UIManager : MonoBehaviour
{
    public Color32[] carColors = new Color32[3];
    public MeshRenderer[] carMesh = new MeshRenderer[3];

    Material[] carMats;

    void Start()
    {
        carMats = new Material[carMesh.Length];

        for (int i = 0; i < carMesh.Length; i++)
        {
            carMats[i] = carMesh[i].material;
        }

        // 버튼의 기본 상태의 색상을 자동차의 색상으로 교체하기
        ColorBlock originColor = transform.GetChild(0).GetComponent<Button>().colors;
        originColor.normalColor = carMats[0].color;
        transform.GetChild(0).GetComponent<Button>().colors = originColor;

        carColors[0] = carMats[0].color;
    }

    public void ChangeColor(int num)
    {
        for (int i = 0; i < carMats.Length; i++)
        {
            carMats[i].color = carColors[num];
        }
    }
}