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

    // ARCore의 네이티브 기능을 이용해서 얼굴의 특정한 세 지점의 위치 및 회전 값을 가져온다.
    void Start()
    {
        faceManager = GetComponent<ARFaceManager>();

        // 얼굴에 표시할 큐브들을 미리 생성해놓는다.
        for (int i = 0; i < 3; i++)
        {
            faceCubes[i] = Instantiate(detectCube);
            faceCubes[i].SetActive(false);
        }

        //faceManager.facesChanged += OnDetectThreePoints;
        faceManager.facesChanged += OnFaceDetectAll;
        coreSubsys = (ARCoreFaceSubsystem)faceManager.subsystem;

        // 각 버튼에 함수를 연결한다.
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


    // 얼굴을 추적하고 있다면 실행할 함수
    void OnDetectThreePoints(ARFacesChangedEventArgs args)
    {
        // 얼굴이 막 인식되었을 때
        if (args.added.Count > 0)
        {

        }
        // 얼굴의 정보(위치, 회전 등)가 변경되었을 때
        else if (args.updated.Count > 0)
        {
            NativeArray<ARCoreFaceRegionData> faceInfos = new NativeArray<ARCoreFaceRegionData>();

            // 얼굴의 기준 3군데의 정보를 배열에 담는다.
            coreSubsys.GetRegionPoses(args.updated[0].trackableId, Allocator.Persistent, ref faceInfos);

            // 미리 생성해둔 큐브를 각 지점에 위치시킨다.
            for (int i = 0; i < faceInfos.Length; i++)
            {
                faceCubes[i].transform.position = faceInfos[i].pose.position;
                faceCubes[i].transform.rotation = faceInfos[i].pose.rotation;
                faceCubes[i].SetActive(true);
            }
        }
        // 얼굴의 인식을 잃어버렸을 때
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

            //logText.text = "정점의 개수: " + verticeCount;
            //logText.text += "\r\n폴리곤의 개수: " + polygonCount;
            //logText.text += "\r\n노멀 벡터의 개수: " + normalCount;

            // UI 인덱스 번호의 값에 해당하는 정점에 큐브를 위치시킨다.
            int number = int.Parse(idxText.text);

            // 정점의 좌표를 얼굴 트랜스폼을 기준으로 한 월드 좌표로 환산한다.
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
        logText.text = "현재 선택된 얼굴 프리팹: " + faceManager.facePrefab.name;
    }
}