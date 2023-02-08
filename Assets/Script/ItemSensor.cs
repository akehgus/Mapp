using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemSensor : MonoBehaviour
{
    // Start is called before the first frame update

    Ray ray;
    float rayDistance;
    public TextMeshProUGUI infoWindowText;
    public Inventory inventory;

    void Start()
    {
        rayDistance = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Sensor();
    }

    void Sensor()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        //Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 5f);  // ����� ����
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            infoWindowText.gameObject.SetActive(true);
            if (hit.transform.CompareTag("Item"))
            {
                
                infoWindowText.text = hit.transform.name + " pick up";

                if(Input.GetKeyDown(KeyCode.F))
                {
                    Destroy(hit.transform.gameObject);
                    Debug.Log("player���� ���� ������: " + hit.transform.name);
                    //GameObject.Find("Player").GetComponent<PlayerController>().getItem(hit.transform.name);
                    inventory.GetItem(hit.transform.GetComponent<ItemPrefeb>().item); // �ֿ� ������ �κ��丮�� ������
                } 
            }
            else if (hit.transform.CompareTag("Battery"))
            {
                infoWindowText.text = hit.transform.name + " pick up";

                if (Input.GetKeyDown(KeyCode.F))
                {
                    Destroy(hit.transform.gameObject);
                    Debug.Log("player���� ���� ������: " + hit.transform.name);
                    //GameObject.Find("Player").GetComponent<PlayerController>().getItem(hit.transform.name);
                    inventory.GetStackItem("Battery"); // �ֿ� ������ �κ��丮�� ������
                }
            }
            else if (hit.transform.CompareTag("Cash"))
            {
                infoWindowText.text = hit.transform.name + " pick up";

                if (Input.GetKeyDown(KeyCode.F))
                {
                    Destroy(hit.transform.gameObject);
                    Debug.Log("player���� ���� ������: " + hit.transform.name);
                    //GameObject.Find("Player").GetComponent<PlayerController>().getItem(hit.transform.name);
                    inventory.GetStackItem("Cash"); // �ֿ� ������ �κ��丮�� ������
                }
            }
            else if(!Interaction.isText)
            {
                infoWindowText.text = "";
                infoWindowText.gameObject.SetActive(false);
            }

            //Debug.Log("����: " + hit.transform.name);
        }
        else
        {
            infoWindowText.text = "";
            infoWindowText.gameObject.SetActive(false);
        }
    }
}
