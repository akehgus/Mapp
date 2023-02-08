using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Status : MonoBehaviour
{

    public int hp;
    public float stemina;
    public bool isRunAble;
    TextMeshProUGUI hpText;
    Slider steminaSlider;
    IEnumerator coru;
    IEnumerator coru2;


    // Start is called before the first frame update
    void Start()
    {
        hp = 4;
        stemina = 100;
        isRunAble = true;
        hpText = GameObject.Find("HpText").GetComponent<TextMeshProUGUI>();
        steminaSlider = GameObject.Find("Stemina").GetComponent<Slider>();

        steminaSlider.maxValue = stemina;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hpText)
            hpText.text = "hp: " + hp.ToString();
        else
            Debug.Log("¾øÀ½");

        steminaSlider.value = stemina;

        if(stemina == 1)
        {
            isRunAble = false;
        } else if(stemina == 100)
        {
            isRunAble = true;
        }

    }

    public void Damage(int d)
    {
        hp -= d;
        if (hp > 4)
            hp = 4;
    }

    public void steminaDrop(int v)
    {
        if (coru == null)
        {
            coru = DropS(v);
            StartCoroutine(coru);
        }
        
    }

    public void steminaUp(int v)
    {
        if (coru2 == null)
        {
            coru2 = DropS2(-v);
            StartCoroutine(coru2);
        }

    }

    public void SteminaHeal(int v)
    {
        stemina += v;
    }

    IEnumerator DropS(int v)
    {
        stemina -= v;
        if (stemina < 0)
            stemina = 0;
        yield return new WaitForSeconds(0.1f);
        coru = null;
    }

    IEnumerator DropS2(int v)
    {
        stemina -= v;
        if (stemina > 100)
            stemina = 100;
        yield return new WaitForSeconds(0.1f);
        coru2 = null;
    }
}
