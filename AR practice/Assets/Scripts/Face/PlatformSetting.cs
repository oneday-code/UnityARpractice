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
        // �÷��� ���� �÷����� �����Ͷ�� ����ī�޶� Ȱ��ȭ�ϰ�, AR ������Ʈ�� ��Ȱ��ȭ�Ѵ�.
        editorCam.SetActive(true);

        for (int i = 0; i < arSessions.Length; i++)
        {
            arSessions[i].SetActive(false);
        }

#elif UNITY_ANDROID
        // �÷��� ���� �÷����� �ȵ���̵��� AR ������Ʈ�� Ȱ��ȭ�ϰ�, ���� ī�޶� ��Ȱ��ȭ�Ѵ�.
        editorCam.SetActive(false);

        for(int i = 0; i < arSessions.Length; i++)
        {
            arSessions[i].SetActive(true);
        }
#endif
    }
}