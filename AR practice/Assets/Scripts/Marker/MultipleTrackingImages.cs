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
            for (int i = 0; i < args.added.Count; i++)
            {
                string name = args.added[i].referenceImage.name;
                StartCoroutine(DB_Manager.db.LoadDB(name, args.added[i].transform));
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