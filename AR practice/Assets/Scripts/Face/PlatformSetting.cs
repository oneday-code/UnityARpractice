using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSetting : MonoBehaviour
{
    public GameObject[] arSessions = new GameObject[2];
    public GameObject editorCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
#if UNITY_EDITOR
        //�÷��� ���� �÷����� �����Ͷ�� ����ī�޶� Ȱ��ȭ�ϰ� AR ������Ʈ�� ��Ȱ��ȭ ��
        editorCam.SetActive(true);

        for(int i = 0; i < arSessions.Length; i++)
        {
            arSessions[i].SetActive(false);
        }

#elif UNITY_ANDROID
        //�÷��� ���� �÷����� �ȵ���̵��� AR ������Ʈ�� Ȱ��ȭ�ϰ� ���� ī�޶� ��Ȱ��ȭ ��Ŵ
        editorCam.SetActive(false);

        for(int i = 0; i < arSessions.Length; i++)
        {
            arSessions[i].SetActive(true);

        }
#endif
    }

}
