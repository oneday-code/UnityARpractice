using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicQuad : MonoBehaviour
{

    //public Shader myShaderFile;

    void Start()
    {
        // 필수 3D 컴포넌트들을 추가한다.
        MeshFilter myFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer myRenderer = gameObject.AddComponent<MeshRenderer>();

        // 메시 인스턴스를 생성한다.
        Mesh myMesh = new Mesh();

        // 1. 정점 좌표
        //Vector3[] verticesPosition = new Vector3[4] { new Vector3(0, 0, 0),
        //                                              new Vector3(0, 1, 0),
        //                                              new Vector3(1, 1, 0),
        //                                              new Vector3(1, 0, 0) };
        Vector3[] verticesPosition = new Vector3[4] { new Vector3(-0.5f, -0.5f, 0),
                                                      new Vector3(-0.5f, 0.5f, 0),
                                                      new Vector3(0.5f, 0.5f, 0),
                                                      new Vector3(0.5f, -0.5f, 0) };
        //myMesh.vertices = verticesPosition;
        myMesh.SetVertices(verticesPosition);

        // 2. 트라이앵글 순서 설정
        int[] triangleOrder = new int[6] { 0, 1, 2, 0, 2, 3 };

        myMesh.triangles = triangleOrder;
        //myMesh.SetTriangles(triangleOrder, 2);

        // 3. uv 범위 설정
        Vector2[] myUVPos = new Vector2[4] {
                                             new Vector2(0, 0),
                                             new Vector2(0, 1),
                                             new Vector2(0.5f, 1),
                                             new Vector2(0.5f, 0),
                                             };

        myMesh.uv = myUVPos;

        // 4. Normal 벡터 계산, Bound 영역 계산, Tangent 영역 계산
        myMesh.RecalculateNormals();
        myMesh.RecalculateBounds();
        myMesh.RecalculateTangents();

        // 생성된 메시 데이터를 Mesh Filter에 추가한다.
        myFilter.mesh = myMesh;

        // 머티리얼을 생성한다.
        Shader myShader = Shader.Find("Standard");
        Material myMat = new Material(myShader);
        //Material myMat = new Material(myShaderFile);

        // 생성된 머티리얼을 Mesh Renderer에 추가한다.
        myRenderer.material = myMat;
    }

}