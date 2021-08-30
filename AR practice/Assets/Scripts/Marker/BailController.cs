using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float throwSpeed = 0.005f;
    public Vector3 ballOffset;
    public float captureRate = 0.5f;
    public Text resultText;

    Rigidbody rb;
    Vector3 startPos;
    Vector3 endPos;
    MeshRenderer mr;

    bool isFly = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 물리 작용을 하지 않도록 한다.
        rb.isKinematic = true;

        mr = GetComponent<MeshRenderer>();

        resultText.text = "";
    }

    void FollowCamera()
    {
        if (!isFly)
        {
            // 공의 위치를 카메라로부터 일정 거리에 놓는다.
            transform.position = Camera.main.transform.position + ballOffset;
        }
    }

    void Update()
    {
        FollowCamera();

        // 터치가 시작되었다면...
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);



            // 터치가 시작되었다면 그 때의 터치 위치(스크린 좌표)를 저장한다.
            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // 터치가 종료될 때에 터치 위치를 저장한다.
                endPos = touch.position;

                float distance = endPos.y - startPos.y;

                // 공이 날아가는 방향을 구한다.
                Vector3 dir = Camera.main.transform.forward + Camera.main.transform.up;
                dir.Normalize();

                // 공의 물리 능력을 활성화시키고 그 방향과 힘으로 발사한다.
                rb.isKinematic = false;
                rb.AddForce(dir * distance * throwSpeed, ForceMode.VelocityChange);
                isFly = true;

                // 발사한 뒤 3초 뒤에 공을 다시 원래 위치로 초기화한다.
                Invoke("ResetBall", 3.0f);
            }
        }
    }

    void ResetBall()
    {
        isFly = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        mr.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 공이 발사되지 않을 상태라면 그냥 종료한다.
        if (!isFly)
        {
            return;
        }

        // 부딪힌 대상의 태그가 "CaptureObject"라면...
        if (collision.transform.tag == "CaptureObject")
        {
            float drawResult = Random.Range(0, 1.0f);

            if (drawResult <= captureRate)
            {


                resultText.text = $"{collision.gameObject.name}을 포획했습니다!";
            }
            else
            {


                resultText.text = $"{collision.gameObject.name}이 도망쳤습니다!";
            }

            collision.gameObject.SetActive(false);
            mr.enabled = false;
        }
    }
}