using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject car;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100, 1<<LayerMask.NameToLayer("Car")))
            {
                //만일 터치 상태가 이동 상태 라면
                if(touch.phase == TouchPhase.Moved)
                {
                    Vector2 touchPos = touch.deltaPosition;

                    car.transform.Rotate(car.transform.up, touchPos.x);

                }

            }
            
        }

    }

}
