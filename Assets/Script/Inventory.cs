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

    GameObject[] itemGm; // ������ �ٿ� �ִ� ������
    Transform[] slots; // ���� ��ġ
    Image[] CanvasSlots;
    
    int downNumber; // �÷��̾ ���� ��ư
    public GameObject SlotPos; // Slotpos ���� ������Ʈ

    // Start is called before the first frame update
    void Start()
    {
        battery = 0;
        cash = 0;
        batteryInfo = GameObject.Find("BatteryInfo").GetComponent<TextMeshProUGUI>();
        cashInfo = GameObject.Find("CashInfo").GetComponent<TextMeshProUGUI>();

        slots = new Transform[SlotPos.transform.childCount]; // ���� ������ŭ �迭 ũ�� ����
        CanvasSlots = new Image[transform.childCount];
        for (int i = 0; i < SlotPos.transform.childCount; i++) // �ڽ��� �迭�� �ֱ�
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
        itemGm = new GameObject[slots.Length]; // ���� �� ��ŭ ������ �ֿ� �� �ְ� ���� ����
        downNumber = 0; // �ʱ� ���� 0�� ���� (ó���� 1���� ������ ����)
        ChangeColor(downNumber);
    }

    // Update is called once per frame
    void Update()
    {
        CheckButton();

        if (batteryInfo)
            batteryInfo.text = "battery: " + battery.ToString();
        else
            Debug.Log("����");

        if (cashInfo)
            cashInfo.text = "cash: " + cash.ToString();
        else
            Debug.Log("����");
    }

    public int getdownNumber() // ���� ���� ��ư �� ���
    {
        return downNumber;
    }

    void CheckButton() // ���� ��ư �� ������ �������� üũ
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

    public void GetItem(GameObject item) // �������� ������ ���Կ� ������ �ֱ�
    {
        /*for (int i = 0; i <= itemGm.Length; i++)
        {
            if (itemGm[i] == null) // ������ ��� �ִٸ�
            {
                itemGm[i] = item; // ���Կ� ������ �ֱ�
                Instantiate(item, slots[i]);
                break;
            }
        }*/

        if(itemGm[downNumber] == null) // ������ ��� �ִٸ�
        {
            item.transform.position = new Vector3(0, 0, 0);
            itemGm[downNumber] = item; // ���Կ� ������ �ֱ�
            Instantiate(item, slots[downNumber]);
            transform.GetChild(downNumber).GetChild(0).GetComponent<Image>().sprite = item.GetComponent<ItemPrefeb>().spriteImage;
            ChangeAlpha(transform.GetChild(downNumber).GetChild(0).gameObject, 1);
            //gm.transform.position = slots[downNumber];
        } else // ���Կ� �������� �ִٸ�
        {
            GameObject dump = Instantiate(slots[downNumber].GetChild(0).GetComponent<ItemPrefeb>().item, GameObject.Find("Dump").transform);// ���� �ִ� �����۰� ��ȯ
            dump.transform.position = GameObject.Find("Player").transform.position;
            Destroy(slots[downNumber].GetChild(0).gameObject);

            item.transform.position = new Vector3(0, 0, 0);
            itemGm[downNumber] = item; // ���Կ� ������ �ֱ�
            
            Instantiate(item, slots[downNumber]);
            transform.GetChild(downNumber).GetChild(0).GetComponent<Image>().sprite = item.GetComponent<ItemPrefeb>().spriteImage;
        }
    }

    public void GetStackItem(string str) // ���͸�, ĳ����
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
