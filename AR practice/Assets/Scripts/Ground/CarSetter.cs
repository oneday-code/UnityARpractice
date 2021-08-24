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
    // ȭ�� �߾ӿ��� AR ���� ���̸� �߻��ؼ� ���̰� �浹�� ������ �ٴ��̶�� �� ���� ȭ��ǥ�� ǥ���ϰڴ�.
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
        debugLog.text = $"����: {Screen.width} x ����: {Screen.height}";
    }

    void Update()
    {
        DetectGround();

        // UI ��ư�� ������ ������ ������Ʈ�� �׳� �����Ѵ�.
        if (EventSystem.current.currentSelectedGameObject)
        {
            return;
        }

        // �ε������� ǥ�õǾ� �ִ� ���¿��� ȭ���� ��ġ�ϸ� �ڵ��� �𵨸��� �� ��ġ�� �����Ѵ�.
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
        // ��ũ���� �߾� ������ �ȼ� ��ǥ�� ���Ѵ�.
        Vector2 centerPos = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();

        // ��ũ�� �߾� �ȼ����� AR ���̸� �߻��ؼ� �ٴ����¿� �ε����ٸ�...
        if (rayManager.Raycast(centerPos, hitInfos, TrackableType.Planes))
        {
            // �� ������ �ε������� ������Ʈ�� ǥ���Ѵ�.
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