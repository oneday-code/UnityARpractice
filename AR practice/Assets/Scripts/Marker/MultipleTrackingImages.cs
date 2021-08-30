using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

[RequireComponent(typeof(ARTrackedImageManager))]
public class MultipleTrackingImages : MonoBehaviour
{
    public Text log;

    ARTrackedImageManager imageManager;

    Vector2 carPosition = new Vector2(37.4870232f, 126.7511413f);

    void Start()
    {
        imageManager = GetComponent<ARTrackedImageManager>();

        // 카메라가 이미지를 인식했을 때에 실행할 함수를 델리게이트에 연결한다.
        imageManager.trackedImagesChanged += OnImageTracked;

    }

    void Update()
    {

    }

    // 이미지 인식 시 처리 함수
    void OnImageTracked(ARTrackedImagesChangedEventArgs args)
    {
        // 이미지 인식이 처음 되었을 때
        if (args.added.Count > 0)
        {
            for (int i = 0; i < args.added.Count; i++)
            {
                string name = args.added[i].referenceImage.name;
                StartCoroutine(DB_Manager.db.LoadDB(name, args.added[i].transform));
            }
        }
        // 인식된 이미지의 정보가 갱신되었을 때
        else if (args.updated.Count > 0)
        {
            for (int i = 0; i < args.updated.Count; i++)
            {
                if (args.updated[i].transform.childCount > 0)
                {
                    Transform childPrefab = args.updated[i].transform.GetChild(0);

                    // 자식 오브젝트의 위치를 갱신한다.
                    childPrefab.position = args.updated[i].transform.position;
                    childPrefab.rotation = args.updated[i].transform.rotation;
                    childPrefab.localScale = args.updated[i].transform.localScale;
                }
            }
        }
        // 인식했었던 이미지에 대한 인식을 잃었을 때
        else if (args.removed.Count > 0)
        {

        }
    }

}