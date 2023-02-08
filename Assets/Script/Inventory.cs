using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int battery;
    public int cash;
    public Color c;
    public Canvas canvas;
    RectTransform targetRect;
    Color originColor;
    TextMeshProUGUI batteryInfo;
    TextMeshProUGUI cashInfo;

    GameObject[] itemGm; // 아이템 바에 있는 아이템
    Transform[] slots; // 슬롯 위치
    Image[] CanvasSlots;
    
    int downNumber; // 플레이어가 누른 버튼
    public GameObject SlotPos; // Slotpos 게임 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        battery = 0;
        cash = 0;
        batteryInfo = GameObject.Find("BatteryInfo").GetComponent<TextMeshProUGUI>();
        cashInfo = GameObject.Find("CashInfo").GetComponent<TextMeshProUGUI>();

        slots = new Transform[SlotPos.transform.childCount]; // 슬롯 개수만큼 배열 크기 설정
        CanvasSlots = new Image[transform.childCount];
        for (int i = 0; i < SlotPos.transform.childCount; i++) // 자식을 배열에 넣기
        {

            //targetRect = transform.GetChild(i).GetComponent<RectTransform>();
            //Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, targetRect.position);
            //Vector3 result = Vector3.zero;
            //RectTransformUtility.ScreenPointToWorldPointInRectangle(targetRect, screenPos, canvas.worldCamera, out result);
            //slots[i] = result;

            //RectTransform rect = transform.GetChild(i).GetComponent<RectTransform>();

            slots[i] = SlotPos.transform.GetChild(i);
            CanvasSlots[i] = transform.GetChild(i).GetComponent<Image>();
        }

        originColor = CanvasSlots[0].GetComponent<Image>().color;
        originColor.a = 0.6f;
        c.a = 0.6f;
        itemGm = new GameObject[slots.Length]; // 슬롯 수 만큼 아이템 주울 수 있게 개수 설정
        downNumber = 0; // 초기 값을 0로 설정 (처음엔 1번을 누르고 있음)
        ChangeColor(downNumber);
    }

    // Update is called once per frame
    void Update()
    {
        CheckButton();

        if (batteryInfo)
            batteryInfo.text = "battery: " + battery.ToString();
        else
            Debug.Log("없음");

        if (cashInfo)
            cashInfo.text = "cash: " + cash.ToString();
        else
            Debug.Log("없음");
    }

    public int getdownNumber() // 누른 숫자 버튼 값 얻기
    {
        return downNumber;
    }

    void CheckButton() // 숫자 버튼 중 무엇을 눌렀는지 체크
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            downNumber = 0;
            ChangeColor(downNumber);
            Debug.Log(downNumber);
        } else if (Input.GetKey(KeyCode.Alpha2))
        {
            downNumber = 1;
            ChangeColor(downNumber);
            Debug.Log(downNumber);
        } else if (Input.GetKey(KeyCode.Alpha3))
        {
            downNumber = 2;
            ChangeColor(downNumber);
            Debug.Log(downNumber);
        }
    }

    public void GetItem(GameObject item) // 아이템을 주으면 슬롯에 아이템 넣기
    {
        /*for (int i = 0; i <= itemGm.Length; i++)
        {
            if (itemGm[i] == null) // 슬롯이 비어 있다면
            {
                itemGm[i] = item; // 슬롯에 아이템 넣기
                Instantiate(item, slots[i]);
                break;
            }
        }*/

        if(itemGm[downNumber] == null) // 슬롯이 비어 있다면
        {
            item.transform.position = new Vector3(0, 0, 0);
            itemGm[downNumber] = item; // 슬롯에 아이템 넣기
            Instantiate(item, slots[downNumber]);
            transform.GetChild(downNumber).GetChild(0).GetComponent<Image>().sprite = item.GetComponent<ItemPrefeb>().spriteImage;
            ChangeAlpha(transform.GetChild(downNumber).GetChild(0).gameObject, 1);
            //gm.transform.position = slots[downNumber];
        } else // 슬롯에 아이템이 있다면
        {
            GameObject dump = Instantiate(slots[downNumber].GetChild(0).GetComponent<ItemPrefeb>().item, GameObject.Find("Dump").transform);// 원래 있던 아이템과 교환
            dump.transform.position = GameObject.Find("Player").transform.position;
            Destroy(slots[downNumber].GetChild(0).gameObject);

            item.transform.position = new Vector3(0, 0, 0);
            itemGm[downNumber] = item; // 슬롯에 아이템 넣기
            
            Instantiate(item, slots[downNumber]);
            transform.GetChild(downNumber).GetChild(0).GetComponent<Image>().sprite = item.GetComponent<ItemPrefeb>().spriteImage;
        }
    }

    public void GetStackItem(string str) // 배터리, 캐쉬용
    {
        if (str == "Battery")
        {
            battery += 1;
        }
        else
        {
            cash += 1;
        }
    }

    void ChangeColor(int dn)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            CanvasSlots[i].color = originColor;
        }
        CanvasSlots[dn].color = c;
    }
    void ChangeAlpha(GameObject gm, float alpha)
    {
        Color color = gm.GetComponent<Image>().color;
        color.a = alpha;
        gm.GetComponent<Image>().color = color;
    }

    public void removeItemGm(int dn)
    {
        itemGm[dn] = null;
        ChangeAlpha(transform.GetChild(dn).GetChild(0).gameObject, 0);
    }

}
