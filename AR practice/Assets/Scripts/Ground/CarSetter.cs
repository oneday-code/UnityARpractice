using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARRaycastManager))]
public class CarSetter : MonoBehaviour
{
    public GameObject indicator;
    public GameObject car;
    public Text debugLog;

    ARRaycastManager rayManager;
    GameObject activeIndicator;

    // Start is called before the first frame update
    void Start()
    {
        rayManager = GetComponent<ARRaycastManager>();

        activeIndicator = Instantiate(indicator);
        activeIndicator.SetActive(false);

        //activeIndicator = Instantiate(car);
        //car.transform.SetPositionAndRotation(activeIndicator.transform.position, activeIndicator.transform.rotation);
        car.SetActive(false);
        debugLog.text = $"가로 : {Screen.width} x 세로 : {Screen.height}";

    }

    // Update is called once per frame
    void Update()
    {

        DetectGround();
        
        if(EventSystem.current.currentSelectedGameObject)
        {
            return;
        }

        //인디케이터 표시되어 있는 상태에서 화면을 터치하면 자동차 모델링을 그 위치에 생성
        if(activeIndicator.activeInHierarchy && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                GameObject go = Instantiate(car);
                go.transform.SetPositionAndRotation(activeIndicator.transform.position, activeIndicator.transform.rotation);
            }

        }

    }

    void DetectGround()
    {
        //스크린의 중앙 지점의 픽셀 좌표를 구함
        Vector2 centerPos = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();
        
        //스크린 중앙 픽셀에 AR 레이를 발사해 바닥 형태에 부딪혔다면
        if (rayManager.Raycast(centerPos, hitInfos, TrackableType.Planes))
        {
            //그 지점에 인디케이터 오브젝트를 표시
            activeIndicator.SetActive(true);
            activeIndicator.transform.position = hitInfos[0].pose.position;
            activeIndicator.transform.rotation = hitInfos[0].pose.rotation;

        }
        else
        {

            activeIndicator.SetActive(false);
            car.SetActive(false);
        }
    }

}
