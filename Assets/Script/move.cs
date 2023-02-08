using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject player;
    public float rotateSpeed = 1.0f;
    public float speed;
    public float saveSpeed;
    public float rayDistance;

    Ray ray;
    bool iscollision;
    bool isCollide;

    Rigidbody rb;

    public Status stat;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        saveSpeed = speed;
        stat = GetComponent<Status>();

        
    }

    // Update is called once per frame
    void Update()
    {
        ray.direction = -transform.up;
        ray.origin = transform.position;
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 5f);  // ����� ����

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            if (!isCollide)
            {
                isCollide = true;
                stat.Damage(1);

            }
            else
            {
                Vector3 vec = new Vector3(
                Input.GetAxis("Horizontal"),
                0, Input.GetAxisRaw("Vertical"));

                rb.velocity = (transform.forward * speed * Input.GetAxis("Vertical")) +
                transform.right * speed * Input.GetAxis("Horizontal");
            }
            
        } else
        {
            isCollide = false;
            Vector3 vec = new Vector3(0, 10, 0);
            Debug.Log(isCollide);
        }
        

        
        //rb.velocity = transform.right * speed * Input.GetAxis("Horizontal");
        //rb.velocity(2f);

        //transform.Translate(vec * speed);
        //Debug.Log(vec);

        Rotate();
        Speedup();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Rotate()
    {
        //if (Input.GetMouseButton(1))
        //{
        Vector3 rot = transform.rotation.eulerAngles; // ���� ī�޶��� ������ Vector3�� ��ȯ
        rot.y += Input.GetAxis("Mouse X") * rotateSpeed; // ���콺 X ��ġ * ȸ�� ���ǵ�
        Quaternion q = Quaternion.Euler(rot); // Quaternion���� ��ȯ
        q.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 2f); // �ڿ������� ȸ��
        //}
    }

    public void Speedup()
    {
        if(!iscollision)
        {
            if (Input.GetKey(KeyCode.LeftShift) && stat.isRunAble == true)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    speed = 15;
                    stat.steminaDrop(1);

                }
                else
                {
                    speed = saveSpeed;
                    stat.steminaUp(1);
                }
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                speed = 5;
                stat.steminaUp(1);
            }
            else
            {
                speed = saveSpeed;
                stat.steminaUp(1);
            }
        }
        


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!isCollide)
            isCollide = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("trap"))
            stat.Damage(1);

        if (!isCollide)
            isCollide = true;
    }

    private void OnTriggerStay(Collider other)
    {
    

        if(other.CompareTag("trap"))
        {
            iscollision = true;

            if (Input.GetKey(KeyCode.LeftShift) && stat.isRunAble == true)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    speed = 12;
                    stat.steminaDrop(1);

                }
                else
                {
                    speed = 7;
                    stat.steminaUp(1);
                }
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                speed = 2;
                stat.steminaUp(1);
            }
            else
            {
                speed = 7;
                stat.steminaUp(1);
            }
        }

        // ���Ϳ��� ��ġ ����
    }

    private void OnTriggerExit(Collider other)
    {
        iscollision = false;
        speed = saveSpeed;
    }

}
