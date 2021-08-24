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
    // 화면 중앙에서 AR 전용 레이를 발사해서 레이가 충돌한 지점이 바닥이라면 그 위에 화살표를 표시하겠다.
    public GameObject indicator;
    public GameObject car;
    public Text debugLog;

    ARRaycastManager rayManager;
    GameObject activeIndicator;

    void Start()
    {
        rayManager = GetComponent<ARRaycastManager>();

        activeIndicator = Instantiate(indicator);
        activeIndicator.SetActive(false);

        car.SetActive(false);
        debugLog.text = $"가로: {Screen.width} x 세로: {Screen.height}";
    }

    void Update()
    {
        DetectGround();

        // UI 버튼을 눌렀을 때에는 업데이트를 그냥 종료한다.
        if (EventSystem.current.currentSelectedGameObject)
        {
            return;
        }

        // 인디케이터 표시되어 있는 상태에서 화면을 터치하면 자동차 모델링을 그 위치에 생성한다.
        if (activeIndicator.activeInHierarchy && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (Vector3.Distance(activeIndicator.transform.position, car.transform.position) > 0.5f)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    car.SetActive(true);
                    car.transform.SetPositionAndRotation(activeIndicator.transform.position, activeIndicator.transform.rotation);
                }
            }
        }
    }

    void DetectGround()
    {
        // 스크린의 중앙 지점의 픽셀 좌표를 구한다.
        Vector2 centerPos = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();

        // 스크린 중앙 픽셀에다 AR 레이를 발사해서 바닥형태에 부딪혔다면...
        if (rayManager.Raycast(centerPos, hitInfos, TrackableType.Planes))
        {
            // 그 지점에 인디케이터 오브젝트를 표시한다.
            activeIndicator.SetActive(true);
            activeIndicator.transform.position = hitInfos[0].pose.position;
            activeIndicator.transform.rotation = hitInfos[0].pose.rotation;

            //activeIndicator.transform.position += activeIndicator.transform.up * 0.05f;
            //activeIndicator.transform.Rotate(activeIndicator.transform.right, 90.0f);
        }
        else
        {
            activeIndicator.SetActive(false);
            car.SetActive(false);
        }
    }
}