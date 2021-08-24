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

        // ī�޶� �̹����� �ν����� ���� ������ �Լ��� ��������Ʈ�� �����Ѵ�.
        imageManager.trackedImagesChanged += OnImageTracked;

    }

    void Update()
    {

    }

    // �̹��� �ν� �� ó�� �Լ�
    void OnImageTracked(ARTrackedImagesChangedEventArgs args)
    {
        // �̹��� �ν��� ó�� �Ǿ��� ��
        if (args.added.Count > 0)
        {
            //for (int i = 0; i < args.added.Count; i++)
            foreach (ARTrackedImage trackedImage in args.added)
            {
                if (trackedImage.transform.childCount == 0)
                {
                    // �ν��� �̹����� ���̺귯�� ���� �̸��� �����´�.
                    string imageName = trackedImage.referenceImage.name;

                    // �νĵ� �̹��� �̸��� ������ �̸��� �������� ã�´�.
                    GameObject prefab = Resources.Load<GameObject>(imageName);

                    // ã�� �������� �ν��� �̹��� ��ġ�� �����Ѵ�.
                    GameObject go = Instantiate(prefab);
                    go.transform.position = trackedImage.transform.position;
                    go.transform.rotation = trackedImage.transform.rotation;
                    go.transform.localScale = trackedImage.transform.localScale;
                    go.transform.parent = trackedImage.transform;
                }
            }
        }
        // �νĵ� �̹����� ������ ���ŵǾ��� ��
        else if (args.updated.Count > 0)
        {
            for (int i = 0; i < args.updated.Count; i++)
            {
                if (args.updated[i].transform.childCount > 0)
                {
                    Transform childPrefab = args.updated[i].transform.GetChild(0);

                    // �ڽ� ������Ʈ�� ��ġ�� �����Ѵ�.
                    childPrefab.position = args.updated[i].transform.position;
                    childPrefab.rotation = args.updated[i].transform.rotation;
                    childPrefab.localScale = args.updated[i].transform.localScale;
                }
            }
        }
        // �ν��߾��� �̹����� ���� �ν��� �Ҿ��� ��
        else if (args.removed.Count > 0)
        {

        }
    }
}