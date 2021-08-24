using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


[RequireComponent(typeof(ARTrackedImageManager))]
public class MultipleTrackingImages : MonoBehaviour
{
    ARTrackedImageManager imageManager;



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
            //for (int i = 0; i < args.added.Count; i++)
            foreach (ARTrackedImage trackedImage in args.added)
            {
                if (trackedImage.transform.childCount == 0)
                {
                    // 인식한 이미지의 라이브러리 상의 이름을 가져온다.
                    string imageName = trackedImage.referenceImage.name;

                    // 인식된 이미지 이름과 동일한 이름의 프리팹을 찾는다.
                    GameObject prefab = Resources.Load<GameObject>(imageName);

                    // 찾은 프리팹을 인식한 이미지 위치에 생성한다.
                    GameObject go = Instantiate(prefab);
                    go.transform.position = trackedImage.transform.position;
                    go.transform.rotation = trackedImage.transform.rotation;
                    go.transform.localScale = trackedImage.transform.localScale;
                    go.transform.parent = trackedImage.transform;
                }
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