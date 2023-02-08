using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed = 1.0f;
    public float roatateLimit;
    float currentRoate;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        //if (Input.GetMouseButton(1))
        //{
        //Vector3 rot = transform.rotation.eulerAngles; // 현재 카메라의 각도를 Vector3로 반환
        //rot.x += -1 * Input.GetAxis("Mouse Y") * rotateSpeed; // 마우스 Y 위치 * 회전 스피드
        //Quaternion q = Quaternion.Euler(rot); // Quaternion으로 변환
        //q.z = 0;
        //transform.rotation = Quaternion.Slerp(transform.rotation, q, 2f); // 자연스럽게 회전
        //light.transform.rotation = Quaternion.Slerp(transform.rotation, q, 2f);
        //}

        float _xRoatation = Input.GetAxisRaw("Mouse Y");
        float _cameraRoationX = _xRoatation * rotateSpeed;
        currentRoate -= _cameraRoationX;
        currentRoate = Mathf.Clamp(currentRoate, -roatateLimit, roatateLimit);

        transform.localEulerAngles = new Vector3(currentRoate, 0f, 0f);

    }
}
