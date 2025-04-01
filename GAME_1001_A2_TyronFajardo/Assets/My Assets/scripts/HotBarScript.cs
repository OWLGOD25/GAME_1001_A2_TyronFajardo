using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using A2Object;
using System.Data.SqlTypes;
using TMPro;


public class HotBarScript : MonoBehaviour
{
    [SerializeField] TMP_Text slot;
    [SerializeField] TMP_Text HBname;
    [SerializeField] TMP_Text description;
    [SerializeField] TMP_Text damage;
    [SerializeField] Button next;
    [SerializeField] Button prev;
    [SerializeField] Button remove;
    [SerializeField] StoreScript ss;

    private List<MyObject> inventory;
    private float money;
    int current;

    void Start()
    {
        money = 1000;
        inventory = new List<MyObject>
        {null, null, null, null, null, null, null, null, null};
        current = 0;
        PrintCurrentItem();

        next.onClick.AddListener(NextItem);
        prev.onClick.AddListener(PrevItem);
        remove.onClick.AddListener(Remove);
    }

    void Update()
    {

    }

    public void Remove()
    {
        if (inventory[current] != null)
        {
            MyObject itemToRemove = inventory[current];
            inventory[current] = null;
            itemToRemove.SetOwned(false);
            ss.UpdateItemOwnership(itemToRemove);
            AddMoney(itemToRemove.GetCost());
            PrintCurrentItem();
        }
    }

    public void PrintCurrentItem()
    {
        int nonNullCount = 0;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] != null)
            {
                nonNullCount++;
            }
        }

        slot.text = (current + 1).ToString();
        HBname.text = inventory[current]?.GetName() ?? "";
        damage.text = inventory[current]?.GetPower().ToString() ?? "";
        description.text = inventory[current]?.GetDescription() ?? "";
    }

    public float GetMoney()
    {
        return money;
    }

    public void DeductMoney(float amount)
    {
        money -= amount;
    }

    public void AddMoney(float amount)
    {
        money += amount;
    }

    public void AddItemToInventory(MyObject item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                break;
            }
        }
        PrintCurrentItem();
    }

    void NextItem()
    {
        do
        {
            current = (current + 1) % inventory.Count;
        } while (inventory[current] == null && current != 0);
        PrintCurrentItem();
    }

    void PrevItem()
    {
        do
        {
            current = (current - 1 + inventory.Count) % inventory.Count;
        } while (inventory[current] == null && current != inventory.Count - 1);
        PrintCurrentItem();
    }
}
