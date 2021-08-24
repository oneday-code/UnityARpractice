using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicQuad : MonoBehaviour
{

    //public Shader myShaderFile;

    void Start()
    {
        // �ʼ� 3D ������Ʈ���� �߰��Ѵ�.
        MeshFilter myFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer myRenderer = gameObject.AddComponent<MeshRenderer>();

        // �޽� �ν��Ͻ��� �����Ѵ�.
        Mesh myMesh = new Mesh();

        // 1. ���� ��ǥ
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

        // 2. Ʈ���̾ޱ� ���� ����
        int[] triangleOrder = new int[6] { 0, 1, 2, 0, 2, 3 };

        myMesh.triangles = triangleOrder;
        //myMesh.SetTriangles(triangleOrder, 2);

        // 3. uv ���� ����
        Vector2[] myUVPos = new Vector2[4] {
                                             new Vector2(0, 0),
                                             new Vector2(0, 1),
                                             new Vector2(0.5f, 1),
                                             new Vector2(0.5f, 0),
                                             };

        myMesh.uv = myUVPos;

        // 4. Normal ���� ���, Bound ���� ���, Tangent ���� ���
        myMesh.RecalculateNormals();
        myMesh.RecalculateBounds();
        myMesh.RecalculateTangents();

        // ������ �޽� �����͸� Mesh Filter�� �߰��Ѵ�.
        myFilter.mesh = myMesh;

        // ��Ƽ������ �����Ѵ�.
        Shader myShader = Shader.Find("Standard");
        Material myMat = new Material(myShader);
        //Material myMat = new Material(myShaderFile);

        // ������ ��Ƽ������ Mesh Renderer�� �߰��Ѵ�.
        myRenderer.material = myMat;
    }

}