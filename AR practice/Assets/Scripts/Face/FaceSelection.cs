
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Video;

public class FaceSelection : MonoBehaviour
{
    public List<GameObject> facePrefabs = new List<GameObject>();
    public ARFaceManager faceManager;
    public ARSession mySession;
    public VideoPlayer currentVideo;

    List<GameObject> faceObjects = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < facePrefabs.Count; i++)
        {
            GameObject go = Instantiate(facePrefabs[i]);
            faceObjects.Add(go);
        }
    }


    public void FaceChanged(int num)
    {

        GameObject go = faceObjects[num];
        faceManager.facePrefab = go;

        // �������� ������ ��쿡�� ���� ������ �÷��� �����ְ�, �ƴϸ� ���� ������ ������Ų��.
        if (num == 2)
        {
            currentVideo.Play();
        }
        else
        {
            currentVideo.Stop();
        }

        // ���� �ν� ���̴� ������ �����ϰ�, �ٽ� ���ο� �ν� ������ �����Ѵ�.
        mySession.Reset();

    }

}