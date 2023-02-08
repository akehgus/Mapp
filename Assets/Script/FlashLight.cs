using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{

    Light flashLight;
    Inventory inventory;
    IEnumerator coru;
    IEnumerator coru2;

    bool isPowerOut;
    bool isTurnOff;
    float timer;

    [SerializeField]
    private int flashLightBatteryTime;
    [SerializeField]
    private int flashLightFlickerTime;

    // Start is called before the first frame update
    void Start()
    {
        flashLight = GetComponent<Light>();
        inventory = GameObject.Find("Inven").GetComponent<Inventory>();

        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F) && coru == null && !isPowerOut)
        {
            coru = delay();
            StartCoroutine(coru);
        }

        CheckPower();
        CheckReload();
        //Debug.Log(timer);
    }



    void PowerButton()
    {
        flashLight.enabled = !flashLight.enabled;

        if(!flashLight.enabled)
        {
            isTurnOff = true;
        } else
        {
            isTurnOff = false;
        }
    }

    void PowerOut()
    {
        isPowerOut = true;
        flashLight.enabled = false;
    }

    void CheckPower()
    {
        if(!isTurnOff)
            timer = timer + Time.deltaTime;

        if (timer > 10 && !isPowerOut && coru2 == null)
        {
            coru2 = flicker();
            StartCoroutine(coru2);
        }

        if (timer > flashLightBatteryTime)
        {
            PowerOut();
            timer = 0;
            isTurnOff = true;
        }
    }

    void CheckReload()
    {
        if(Input.GetKey(KeyCode.R) && inventory.battery > 0)
        {
            inventory.battery--;
            ReLoad();
        }
    }

    void ReLoad()
    {
        if (isPowerOut)
        {
            isPowerOut = false;
            timer = 0;
        }     
    }

    IEnumerator delay()
    {
        PowerButton();
        yield return new WaitForSeconds(0.5f);
        coru = null;

    }

    IEnumerator flicker()
    {
        while(!isTurnOff)
        {
            float randomFloat = Random.Range(0.1f, 1f);
            float randomFloat2 = Random.Range(0.1f, 1f);
            flashLight.enabled = !flashLight.enabled;
            yield return new WaitForSeconds(randomFloat);
            flashLight.enabled = !flashLight.enabled;
            yield return new WaitForSeconds(randomFloat2);
        }

        flashLight.enabled = false;

        coru2 = null;
    }
}
