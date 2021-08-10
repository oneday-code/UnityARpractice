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
        debugLog.text = $"���� : {Screen.width} x ���� : {Screen.height}";

    }

    // Update is called once per frame
    void Update()
    {

        DetectGround();
        
        if(EventSystem.current.currentSelectedGameObject)
        {
            return;
        }

        //�ε������� ǥ�õǾ� �ִ� ���¿��� ȭ���� ��ġ�ϸ� �ڵ��� �𵨸��� �� ��ġ�� ����
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
        //��ũ���� �߾� ������ �ȼ� ��ǥ�� ����
        Vector2 centerPos = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();
        
        //��ũ�� �߾� �ȼ��� AR ���̸� �߻��� �ٴ� ���¿� �ε����ٸ�
        if (rayManager.Raycast(centerPos, hitInfos, TrackableType.Planes))
        {
            //�� ������ �ε������� ������Ʈ�� ǥ��
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
