using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class LocationManager : MonoBehaviour
{
    public static LocationManager locManager;
    public Text[] locateTexts = new Text[3];
    public float delayTime = 5;
    public float updateTime = 10;

    public float latitude = 0;
    public float longitude = 0;
    public float altitude = 0;
    public bool receiveGPS = false;

    float waitTime = 0;

    private void Awake()
    {
        if (locManager == null)
        {
            locManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        StartCoroutine(GPS_On());
    }


    IEnumerator GPS_On()
    {
        // ����, ������� ��ġ ������ ����ϵ��� �㰡���� ���ߴٸ�, �㰡�� ��û�Ѵ�.
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);

            // �㰡�� ���� ������ ����Ѵ�.
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        }

        // GPS ��ġ�� �����ִ��� Ȯ���Ѵ�.
        if (!Input.location.isEnabledByUser)
        {
            for (int i = 0; i < locateTexts.Length; i++)
            {
                locateTexts[i].text = "GPS off";
            }

            yield break;
        }

        // ��ġ ������ ��û�� �Ѵ�.
        Input.location.Start();

        // ��ġ ���������Ͱ� ���ŵ� ������ ����Ѵ�.
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(1.0f);
            waitTime++;
        }

        // ��ġ ������ ���ſ� ���� �Ǵ� ���� ����Ǿ��ٸ� ����� ����ϰ� ������.
        if (Input.location.status == LocationServiceStatus.Failed || Input.location.status == LocationServiceStatus.Stopped)
        {
            for (int i = 0; i < locateTexts.Length; i++)
            {
                locateTexts[i].text = "��ġ ���� ���� ����";
            }
        }

        // ���� �����ð��� �ʰ��Ǿ�����
        if (waitTime >= delayTime)
        {
            for (int i = 0; i < locateTexts.Length; i++)
            {
                locateTexts[i].text = "���� ���ð��� �ʰ��Ͽ����ϴ�.";
            }
        }

        receiveGPS = true;

        while (receiveGPS)
        {
            // ������ �����͸� UI�� ����Ѵ�.
            locateTexts[0].text = "����: " + Input.location.lastData.latitude;
            locateTexts[1].text = "�浵: " + Input.location.lastData.longitude;
            //locateTexts[2].text = "��: " + Input.location.lastData.altitude;

            // ��ġ �����͸� ������ �����Ѵ�.
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            altitude = Input.location.lastData.altitude;

            yield return new WaitForSeconds(updateTime);
        }
    }


}