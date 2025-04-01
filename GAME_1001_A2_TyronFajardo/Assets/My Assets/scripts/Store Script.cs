using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Data.SqlTypes;
using A2Object;

public class StoreScript : MonoBehaviour
{
    [SerializeField] TMP_Text money;
    [SerializeField] TMP_Text SSname;
    [SerializeField] TMP_Text description;
    [SerializeField] TMP_Text damage;
    [SerializeField] TMP_Text cost;
    [SerializeField] TMP_Text owned;
    [SerializeField] Button prev;
    [SerializeField] Button next;
    [SerializeField] Button buy;
    [SerializeField] HotBarScript hbs;

    private List<MyObject> stock;
    int current;

    void Start()
    {
        stock = new List<MyObject>();
        stock.Add(new MyObject(false, "Dagger", 5f, 4, "Small and sharp but gets the job done."));
        stock.Add(new MyObject(false, "LongSword", 50f, 8, "A nice true to his name sword"));
        stock.Add(new MyObject(false, "crossbow", 25f, 12, "Long reload but big punch"));
        stock.Add(new MyObject(false, "Pistol", 50f, 16, "small and reliable"));
        stock.Add(new MyObject(false, "Armor", 100f, 24, "gain +10 to defence"));
        current = stock.Count - 1;
        PrintCurrentItem();

        buy.onClick.AddListener(BuyItem);
        next.onClick.AddListener(NextItem);
        prev.onClick.AddListener(PrevItem);
    }

    void NextItem()
    {
        current = (current + 1) % stock.Count;
        PrintCurrentItem();
    }

    void PrevItem()
    {
        current = (current - 1 + stock.Count) % stock.Count;
        PrintCurrentItem();
    }

    void BuyItem()
    {
        MyObject currentItem = stock[current];
        float itemCost = currentItem.GetCost();
        if (hbs.GetMoney() >= itemCost)
        {
            hbs.DeductMoney(itemCost);
            currentItem.SetOwned(true);
            hbs.AddItemToInventory(currentItem);
            PrintCurrentItem();
        }
    }

    public void UpdateItemOwnership(MyObject item)
    {
        foreach (var stockItem in stock)
        {
            if (stockItem.GetName() == item.GetName())
            {
                stockItem.SetOwned(item.GetOwned());
                break;
            }
        }
        PrintCurrentItem();
    }

    void PrintCurrentItem()
    {
        money.text = "$" + hbs.GetMoney().ToString();
        cost.text = "$" + stock[current].GetCost().ToString();
        SSname.text = stock[current].GetName();
        damage.text = stock[current].GetPower().ToString();
        owned.text = (stock[current].GetOwned() ? "OWNED" : "");
        description.text = stock[current].GetDescription();
    }
}
