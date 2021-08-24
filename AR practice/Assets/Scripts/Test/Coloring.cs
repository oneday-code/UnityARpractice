using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coloring : MonoBehaviour
{
    public Color32 selectColor;
    [Range(0, 1.0f)]
    public float brushSize = 0.1f;
    public List<int> selectNums = new List<int>();
    public Transform quadPrefab;

    Vector3[] myVertices;
    Color[] vertColors;

    Mesh mesh;

    void Start()
    {
        //mesh = GetComponent<MeshFilter>().mesh;
        mesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        myVertices = mesh.vertices;

        vertColors = new Color[mesh.vertices.Length];

        for (int i = 0; i < vertColors.Length; i++)
        {
            vertColors[i] = Color.white;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            FindVertice();
            ChangeMeshColor();
        }


    }

    void FindVertice()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            selectNums.Clear();

            Vector3 hitPoint = hitInfo.point;

            quadPrefab.position = hitPoint;
            quadPrefab.forward = hitInfo.normal;

            GameObject go = hitInfo.transform.gameObject;

            //hitPoint = go.transform.InverseTransformPoint(hitPoint);

            for (int i = 0; i < myVertices.Length; i++)
            {
                Vector3 vert = go.transform.TransformPoint(myVertices[i]);

                if (Vector3.Distance(vert, hitPoint) < brushSize)
                {
                    selectNums.Add(i);
                }
            }
            print(selectNums.Count);
        }
    }


    void ChangeMeshColor()
    {
        //for (int i = 0; i < vertColors.Length; i++)
        //{
        //    vertColors[i] = (Color)selectColor;
        //}

        if (selectNums.Count > 0)
        {
            //vertColors = mesh.colors;

            for (int i = 0; i < selectNums.Count; i++)
            {
                vertColors[selectNums[i]] = (Color)selectColor;
            }
            mesh.colors = vertColors;
        }

    }
}