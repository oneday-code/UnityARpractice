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
        // 만일, 사용자의 위치 정보를 사용하도록 허가받지 못했다면, 허가를 요청한다.
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);

            // 허가가 있을 때까지 대기한다.
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        }

        // GPS 장치가 켜져있는지 확인한다.
        if (!Input.location.isEnabledByUser)
        {
            for (int i = 0; i < locateTexts.Length; i++)
            {
                locateTexts[i].text = "GPS off";
            }

            yield break;
        }

        // 위치 데이터 요청을 한다.
        Input.location.Start();

        // 위치 정보데이터가 수신될 때까지 대기한다.
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(1.0f);
            waitTime++;
        }

        // 위치 데이터 수신에 실패 또는 수신 종료되었다면 결과를 출력하고 끝낸다.
        if (Input.location.status == LocationServiceStatus.Failed || Input.location.status == LocationServiceStatus.Stopped)
        {
            for (int i = 0; i < locateTexts.Length; i++)
            {
                locateTexts[i].text = "위치 정보 수신 실패";
            }
        }

        // 만일 지연시간이 초과되었으면
        if (waitTime >= delayTime)
        {
            for (int i = 0; i < locateTexts.Length; i++)
            {
                locateTexts[i].text = "응답 대기시간을 초과하였습니다.";
            }
        }

        receiveGPS = true;

        while (receiveGPS)
        {
            // 수신한 데이터를 UI에 출력한다.
            locateTexts[0].text = "위도: " + Input.location.lastData.latitude;
            locateTexts[1].text = "경도: " + Input.location.lastData.longitude;
            //locateTexts[2].text = "고도: " + Input.location.lastData.altitude;

            // 위치 데이터를 변수에 저장한다.
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            altitude = Input.location.lastData.altitude;

            yield return new WaitForSeconds(updateTime);
        }
    }


}