using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interaction : MonoBehaviour
{

    float l_rayDistance = 10f;

    public static bool isText;

    GameObject[] cd;
    GameObject player;
    IEnumerator coru;

    public Status stat;
    public Inventory inven;
    TextMeshProUGUI infoText;
    TextMeshProUGUI information;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        infoText = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
        information = GameObject.Find("information").GetComponent<TextMeshProUGUI>();
        infoText.gameObject.SetActive(false);
        information.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    private void OnTriggerEnter(Collider other)
    {
        Transform par = other.transform.parent;
        Debug.Log("123");
        if (other.CompareTag("TpPoint"))
        {
            infoText.gameObject.SetActive(true);
            isText = true;
            infoText.text = "E Clilk";
        }
        
        if (par.CompareTag("vending")) 
        {
            information.gameObject.SetActive(true);
            information.text = "needs 5 cash";
            infoText.gameObject.SetActive(true);
            isText = true;
            infoText.text = "E Clilk";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKey(KeyCode.E))
        {
            Transform par = other.transform.parent;
            if (par.CompareTag("Hurdle"))
            {
                if (coru == null)
                {
                    coru = delay(par, other.name);
                    StartCoroutine(coru);
                }
            }
            else if (par.CompareTag("vending")) 
            {
                if (coru == null)
                {
                    information.gameObject.SetActive(false);
                    coru = vendingDelay(par);
                    StartCoroutine(coru);
                }
            }

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        information.gameObject.SetActive(false);
        infoText.gameObject.SetActive(false);
        isText = false;
    }



    IEnumerator delay(Transform par, string name)
    {
        if (name == "TpPoint1")
        {
            player.transform.position = new Vector3(par.Find("TpPoint2").transform.position.x, player.transform.position.y, par.Find("TpPoint2").transform.position.z);
        }
        else
        {
            player.transform.position = new Vector3(par.Find("TpPoint1").transform.position.x, player.transform.position.y, par.Find("TpPoint1").transform.position.z);
        }

        stat.steminaDrop(25);
        yield return new WaitForSeconds(1f);

        coru = null;
        
    }

    IEnumerator vendingDelay(Transform par)
    {
        if (inven.cash >= 5)
        {
            inven.battery += 1;
            inven.cash -= 5;
        }
        else 
        {
            int msg;
            msg = 5 - inven.cash;
            infoText.gameObject.SetActive(true);
            infoText.text = "needs " +msg+ " cash more";
        }

        yield return new WaitForSeconds(1f);

        coru = null;

    }
}
