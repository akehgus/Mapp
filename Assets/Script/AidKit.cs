using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKit : MonoBehaviour
{
    Inventory inventory;
    Status stat;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Inven").GetComponent<Inventory>();
        stat = GameObject.Find("Player").GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && inventory.getdownNumber().ToString() == transform.parent.name)
        {
            Debug.Log(name + " »ç¿ë");
            stat.Damage(-3);
            inventory.removeItemGm(inventory.getdownNumber());
            Destroy(gameObject);
        }
    }
}
