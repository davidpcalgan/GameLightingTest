using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rigid;
    public Transform eyeCamera;

    public float walkSpeed;
    public float runSpeed;
    public float mouseSensitivity;

    public float JumpStrength;
    public float StrafeSpeed;
    public float ForwardSpeed;
    public float BackwardSpeed;

    public GameObject eyeCam;

    float momentum;

    bool inAir = false;
    bool runningStart = false;

    // Start is called before the first frame update
    void Start()
    {
        momentum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            GameTime.isPaused = !GameTime.isPaused;
        }

        RaycastHit hit;
        if (Physics.Raycast(eyeCam.transform.position, Vector3.down, out hit, 2.0f))
        {
            if (hit.distance < 1.805f)
            {
                inAir = false;
            }
            else if (Input.GetAxis("Jump") == 0)
            {
                inAir = true;
                if (Input.GetAxis("Run") > 0)
                {
                    runningStart = true;
                }
            }

            rigid.AddForce(Vector3.up * Input.GetAxis("Jump") * JumpStrength * (inAir ? 0 : 1));
        }
        else
        {
            inAir = true;
        }

        if (Input.GetAxis("Run") == 0)
        {
            runningStart = false;
        }

        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity * GameTime.deltaTime, 0)));
        float camX = eyeCamera.localEulerAngles.x;
        camX = camX > 180 ? camX - 360 : camX;
        eyeCamera.localEulerAngles = new Vector3(Mathf.Clamp(camX - Input.GetAxis("Mouse Y") * mouseSensitivity * GameTime.deltaTime, -85, 85), eyeCamera.localEulerAngles.y, eyeCamera.localEulerAngles.z);

        Vector3 moveVec = (transform.forward * Input.GetAxis("Vertical")) * (Input.GetAxis("Vertical") > 0 ? ForwardSpeed : BackwardSpeed) + (transform.right * Input.GetAxis("Horizontal") * StrafeSpeed);
        moveVec *= (Player.Instance.asthmaAttack ? 0 : 1) * (Input.GetAxis("Run") > 0 && (!inAir || runningStart) ? runSpeed : walkSpeed) * GameTime.deltaTime;
        rigid.MovePosition(transform.position + moveVec);

        if (moveVec != Vector3.zero && Input.GetAxis("Run") > 0)
            Player.Instance.DamageAsthma(30);
        else
            Player.Instance.RestAsthma();

        /*
        if (moveVec.magnitude > 0)
        {
            momentum += moveVec.magnitude * 0.25f;
            momentum = momentum > 1 ? 1 : momentum;
        }
        else
        {
            momentum -= GameTime.deltaTime * 10;
            momentum = momentum < 0 ? 0 : momentum;
        }

        eyeCamera.localPosition = new Vector3(momentum * Mathf.Sin(Time.time * 8) / 2, 1.7f + momentum * Mathf.Sin(Time.time * 16) / 4, 0);*/
    }
}
