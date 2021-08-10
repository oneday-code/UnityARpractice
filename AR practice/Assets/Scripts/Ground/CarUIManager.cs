using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarUIManager : MonoBehaviour
{
    public Color32[] carColors = new Color32[3];
    public MeshRenderer[] carMesh = new MeshRenderer[3];
    
    Material[] carMats;

    // Start is called before the first frame update
    void Start()
    {
        carMats = new Material[carMesh.Length];

        for(int i = 0; i < carMesh.Length; i++)
        {
            carMats[i] = carMesh[i].material;

        }

        //버튼의 기본 상태의 색상을 자동차의 색상으로 교체
        ColorBlock originColor = transform.GetChild(0).GetComponent<Button>().colors;
        originColor.normalColor = carMats[0].color;
        transform.GetChild(0).GetComponent<Button>().colors = originColor;

        carColors[0] = carMats[0].color;

    }

    // Update is called once per frame
    void ChangeColor(int num)
    {
        for (int i = 0; i < carMesh.Length; i++)
        {
            carMats[i].color = carColors[num];

        }

    }
}
