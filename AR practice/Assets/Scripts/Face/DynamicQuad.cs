using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicQuad : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        MeshFilter myFilter = gameObject.AddComponent<MeshFilter>();
        //MeshRenderer myRenderer = gameObject.AddComponent<Mesh>();

        Mesh myMesh = new Mesh();

        Vector3[] verticesPosition = new Vector3[4] { new Vector3(0, 0, 0), 
                                                      new Vector3(0, 1, 0),
                                                      new Vector3(1, 1, 0), 
                                                      new Vector3(1, 0, 0) };
        //myMesh.vertices = verticesPosition;
        myMesh.SetVertices(verticesPosition);

        //2. 트라이앵글 순서 설정
        int[] triangleOrder = new int[6] { 0, 1, 2, 0, 2, 3 };
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
