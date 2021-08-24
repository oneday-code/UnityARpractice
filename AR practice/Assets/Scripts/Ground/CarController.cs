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
        // 자동차 모델링을 터치한 상태에서 좌우로 드래그해서 회전시키고 싶다.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100, 1 << LayerMask.NameToLayer("Car")))
            {
                // 만일, 터치 상태가 이동 상태라면...
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 touchPos = touch.deltaPosition;

                    car.transform.Rotate(car.transform.up, touchPos.x * -rotSpeed);
                }
            }
        }
    }
}