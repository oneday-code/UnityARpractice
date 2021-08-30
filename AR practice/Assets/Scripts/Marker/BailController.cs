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
        // ���� �ۿ��� ���� �ʵ��� �Ѵ�.
        rb.isKinematic = true;

        mr = GetComponent<MeshRenderer>();

        resultText.text = "";
    }

    void FollowCamera()
    {
        if (!isFly)
        {
            // ���� ��ġ�� ī�޶�κ��� ���� �Ÿ��� ���´�.
            transform.position = Camera.main.transform.position + ballOffset;
        }
    }

    void Update()
    {
        FollowCamera();

        // ��ġ�� ���۵Ǿ��ٸ�...
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);



            // ��ġ�� ���۵Ǿ��ٸ� �� ���� ��ġ ��ġ(��ũ�� ��ǥ)�� �����Ѵ�.
            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // ��ġ�� ����� ���� ��ġ ��ġ�� �����Ѵ�.
                endPos = touch.position;

                float distance = endPos.y - startPos.y;

                // ���� ���ư��� ������ ���Ѵ�.
                Vector3 dir = Camera.main.transform.forward + Camera.main.transform.up;
                dir.Normalize();

                // ���� ���� �ɷ��� Ȱ��ȭ��Ű�� �� ����� ������ �߻��Ѵ�.
                rb.isKinematic = false;
                rb.AddForce(dir * distance * throwSpeed, ForceMode.VelocityChange);
                isFly = true;

                // �߻��� �� 3�� �ڿ� ���� �ٽ� ���� ��ġ�� �ʱ�ȭ�Ѵ�.
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
        // ���� �߻���� ���� ���¶�� �׳� �����Ѵ�.
        if (!isFly)
        {
            return;
        }

        // �ε��� ����� �±װ� "CaptureObject"���...
        if (collision.transform.tag == "CaptureObject")
        {
            float drawResult = Random.Range(0, 1.0f);

            if (drawResult <= captureRate)
            {


                resultText.text = $"{collision.gameObject.name}�� ��ȹ�߽��ϴ�!";
            }
            else
            {


                resultText.text = $"{collision.gameObject.name}�� �����ƽ��ϴ�!";
            }

            collision.gameObject.SetActive(false);
            mr.enabled = false;
        }
    }
}