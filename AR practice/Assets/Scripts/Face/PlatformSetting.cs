using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSetting : MonoBehaviour
{
    public GameObject[] arSessions = new GameObject[2];
    public GameObject editorCam;

    private void Awake()
    {
#if UNITY_EDITOR
        // 플레이 중인 플랫폼이 에디터라면 메인카메라를 활성화하고, AR 오브젝트를 비활성화한다.
        editorCam.SetActive(true);

        for (int i = 0; i < arSessions.Length; i++)
        {
            arSessions[i].SetActive(false);
        }

#elif UNITY_ANDROID
        // 플레이 중인 플랫폼이 안드로이드라면 AR 오브젝트를 활성화하고, 메인 카메라를 비활성화한다.
        editorCam.SetActive(false);

        for(int i = 0; i < arSessions.Length; i++)
        {
            arSessions[i].SetActive(true);
        }
#endif
    }
}