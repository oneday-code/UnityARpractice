using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARSubsystems;
using Unity.Collections;
using System;

public class FindDetection : MonoBehaviour
{
    public Text logText;
    public GameObject detectCube;
    public Button[] arrowButtons;
    public Text idxText;

    GameObject[] faceCubes = new GameObject[3];
    ARFaceManager faceManager;
    ARCoreFaceSubsystem coreSubsys;

    enum ArrowDirection
    {
        None,
        Left,
        Right
    }
    ArrowDirection arrowDir = ArrowDirection.None;

    // ARCore�� ����Ƽ�� ����� �̿��ؼ� ���� Ư���� �� ������ ��ġ �� ȸ�� ���� �����´�.
    void Start()
    {
        faceManager = GetComponent<ARFaceManager>();

        // �󱼿� ǥ���� ť����� �̸� �����س��´�.
        for (int i = 0; i < 3; i++)
        {
            faceCubes[i] = Instantiate(detectCube);
            faceCubes[i].SetActive(false);
        }

        //faceManager.facesChanged += OnDetectThreePoints;
        faceManager.facesChanged += OnFaceDetectAll;
        coreSubsys = (ARCoreFaceSubsystem)faceManager.subsystem;

        // �� ��ư�� �Լ��� �����Ѵ�.
        arrowButtons[0].onClick.AddListener(() => OnClickedArrowButton(ArrowDirection.Left));
        arrowButtons[1].onClick.AddListener(() => OnClickedArrowButton(ArrowDirection.Right));

        idxText.text = "0";
    }

    void OnClickedArrowButton(ArrowDirection dir)
    {
        int number = int.Parse(idxText.text);

        if (dir == ArrowDirection.Left)
        {
            number = Mathf.Max(0, number - 1);
        }
        else if (dir == ArrowDirection.Right)
        {
            number = Mathf.Min(468, number + 1);
        }

        idxText.text = number.ToString();
    }


    // ���� �����ϰ� �ִٸ� ������ �Լ�
    void OnDetectThreePoints(ARFacesChangedEventArgs args)
    {
        // ���� �� �νĵǾ��� ��
        if (args.added.Count > 0)
        {

        }
        // ���� ����(��ġ, ȸ�� ��)�� ����Ǿ��� ��
        else if (args.updated.Count > 0)
        {
            NativeArray<ARCoreFaceRegionData> faceInfos = new NativeArray<ARCoreFaceRegionData>();

            // ���� ���� 3������ ������ �迭�� ��´�.
            coreSubsys.GetRegionPoses(args.updated[0].trackableId, Allocator.Persistent, ref faceInfos);

            // �̸� �����ص� ť�긦 �� ������ ��ġ��Ų��.
            for (int i = 0; i < faceInfos.Length; i++)
            {
                faceCubes[i].transform.position = faceInfos[i].pose.position;
                faceCubes[i].transform.rotation = faceInfos[i].pose.rotation;
                faceCubes[i].SetActive(true);
            }
        }
        // ���� �ν��� �Ҿ������ ��
        else if (args.removed.Count > 0)
        {
            for (int i = 0; i < faceCubes.Length; i++)
            {
                faceCubes[i].SetActive(false);
            }
        }

    }

    void OnFaceDetectAll(ARFacesChangedEventArgs args)
    {
        if (args.added.Count > 0)
        {

        }
        else if (args.updated.Count > 0)
        {
            int verticeCount = args.updated[0].vertices.Length;
            int polygonCount = args.updated[0].indices.Length / 3;
            int normalCount = args.updated[0].normals.Length;

            //logText.text = "������ ����: " + verticeCount;
            //logText.text += "\r\n�������� ����: " + polygonCount;
            //logText.text += "\r\n��� ������ ����: " + normalCount;

            // UI �ε��� ��ȣ�� ���� �ش��ϴ� ������ ť�긦 ��ġ��Ų��.
            int number = int.Parse(idxText.text);

            // ������ ��ǥ�� �� Ʈ�������� �������� �� ���� ��ǥ�� ȯ���Ѵ�.
            Vector3 faceWorldPosition = args.updated[0].transform.TransformPoint(args.updated[0].vertices[number]);

            faceCubes[0].transform.position = faceWorldPosition;
            faceCubes[0].transform.forward = args.updated[0].normals[number];
            faceCubes[0].SetActive(true);
        }
        else if (args.removed.Count > 0)
        {
            faceCubes[0].SetActive(false);
        }
    }

    void Update()
    {
        logText.text = "���� ���õ� �� ������: " + faceManager.facePrefab.name;
    }
}