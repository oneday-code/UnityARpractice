using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject car;
    public float rotSpeed = 0.1f;

    void Start()
    {

    }

    void Update()
    {
        // �ڵ��� �𵨸��� ��ġ�� ���¿��� �¿�� �巡���ؼ� ȸ����Ű�� �ʹ�.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100, 1 << LayerMask.NameToLayer("Car")))
            {
                // ����, ��ġ ���°� �̵� ���¶��...
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 touchPos = touch.deltaPosition;

                    car.transform.Rotate(car.transform.up, touchPos.x * -rotSpeed);
                }
            }
        }
    }
}