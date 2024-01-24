using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : MonoBehaviour
{
    private new Rigidbody rigidbody;

    [SerializeField] float moveSpeed;

    private bool isWalking;
    private bool isDashing;
    private Vector3 inputVector;
    private Vector3 dashVector;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        // 플레이어 입력
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (inputVector != Vector3.zero)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        // 플레이어 대시
        if (isDashing)
        {
            rigidbody.velocity = dashVector * moveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            // 플레이어 회전
            if (inputVector != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(inputVector);
            }

            // 플레이어 움직임
            rigidbody.velocity = inputVector * moveSpeed * Time.fixedDeltaTime;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isWalking && !isDashing)
            {
                StartCoroutine(Dash());
            }
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;

        dashVector = inputVector;
        float tempSpeed = moveSpeed;
        moveSpeed = 3000;

        yield return new WaitForSeconds(0.2f);

        moveSpeed = tempSpeed;

        isDashing = false;
    }
}
