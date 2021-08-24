
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

        // 프리팹이 비디오일 경우에는 비디오 파일을 플레이 시켜주고, 아니면 비디오 파일을 정지시킨다.
        if (num == 2)
        {
            currentVideo.Play();
        }
        else
        {
            currentVideo.Stop();
        }

        // 현재 인식 중이던 세션을 종료하고, 다시 새로운 인식 세션을 시작한다.
        mySession.Reset();

    }

}