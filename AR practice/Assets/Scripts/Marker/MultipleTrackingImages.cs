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
            for (int i = 0; i < args.added.Count; i++)
            {
                string name = args.added[0].referenceImage.name;
                StartCoroutine(DB_Manager.db.LoadDB(name, args.added[0].transform));
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

    IEnumerator StartDBSearch(string refImageName, List<ARTrackedImage> added)
    {

        //DB���� ī�޶�� �˻��� �̹����� ���� ����� �˻�
        //StartCoroutine(DB_Manager.db.LoadDB(refImageName));

        while(DB_Manager.db.searchComplete == false)
        {
            yield return null;
        }

        //Vector2 currentPos = new Vector2(LocationManager.locManager.latitude, LocationManager.locManager.longitude);
        //���� �������� �����ؾ��ϴ� �����
        //if (DB_Manager.db.isCreate)
        //{
        //    //for (int i = 0; i < args.added.Count; i++)
        //    foreach (ARTrackedImage trackedImage in added)
        //    {
        //        if (trackedImage.transform.childCount == 0)
        //        {
        //            // �ν��� �̹����� ���̺귯�� ���� �̸��� �����´�.
        //            string imageName = trackedImage.referenceImage.name;

        //            // �νĵ� �̹��� �̸��� ������ �̸��� �������� ã�´�.
        //            GameObject prefab = Resources.Load<GameObject>(imageName);

        //            // ã�� �������� �ν��� �̹��� ��ġ�� �����Ѵ�.
        //            GameObject go = Instantiate(prefab);
        //            go.transform.position = trackedImage.transform.position;
        //            go.transform.rotation = trackedImage.transform.rotation;
        //            go.transform.localScale = trackedImage.transform.localScale;
        //            go.transform.parent = trackedImage.transform;
        //        }
        //    }
    }
}
