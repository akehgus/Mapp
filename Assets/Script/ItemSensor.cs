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

        //Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 5f);  // 디버그 레이
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            infoWindowText.gameObject.SetActive(true);
            if (hit.transform.CompareTag("Item"))
            {
                
                infoWindowText.text = hit.transform.name + " pick up";

                if(Input.GetKeyDown(KeyCode.F))
                {
                    Destroy(hit.transform.gameObject);
                    Debug.Log("player에게 보낸 아이템: " + hit.transform.name);
                    //GameObject.Find("Player").GetComponent<PlayerController>().getItem(hit.transform.name);
                    inventory.GetItem(hit.transform.GetComponent<ItemPrefeb>().item); // 주운 아이템 인벤토리로 보내기
                } 
            }
            else if (hit.transform.CompareTag("Battery"))
            {
                infoWindowText.text = hit.transform.name + " pick up";

                if (Input.GetKeyDown(KeyCode.F))
                {
                    Destroy(hit.transform.gameObject);
                    Debug.Log("player에게 보낸 아이템: " + hit.transform.name);
                    //GameObject.Find("Player").GetComponent<PlayerController>().getItem(hit.transform.name);
                    inventory.GetStackItem("Battery"); // 주운 아이템 인벤토리로 보내기
                }
            }
            else if (hit.transform.CompareTag("Cash"))
            {
                infoWindowText.text = hit.transform.name + " pick up";

                if (Input.GetKeyDown(KeyCode.F))
                {
                    Destroy(hit.transform.gameObject);
                    Debug.Log("player에게 보낸 아이템: " + hit.transform.name);
                    //GameObject.Find("Player").GetComponent<PlayerController>().getItem(hit.transform.name);
                    inventory.GetStackItem("Cash"); // 주운 아이템 인벤토리로 보내기
                }
            }
            else if(!Interaction.isText)
            {
                infoWindowText.text = "";
                infoWindowText.gameObject.SetActive(false);
            }

            //Debug.Log("감지: " + hit.transform.name);
        }
        else
        {
            infoWindowText.text = "";
            infoWindowText.gameObject.SetActive(false);
        }
    }
}
