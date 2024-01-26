using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    public int money = 0;
    public Text moneyText;
    public static Inventory Instance { get; private set; }

    [SerializeField] private Transform slotParent;
    [SerializeField] private Slot[] slots;
    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        AddSlot();
    }

    private void Start()
    {
        moneyText.text = $"0";
    }

    private void Update()
    {
        UpdateMoneyText();
    }

    public void AddMoney(int moneyValue)
    {
        money += moneyValue;
        moneyText.text = money.ToString(); // 돈을 텍스트로 표시
        // 돈이 추가되었을 때 UI에 업데이트하는 코드 추가
        UpdateMoneyText();
    }

    public void AddSlot()
    {
        int i = 0;
        for (; i < items.Count; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    public bool HasItem(string itemName)
    {
        foreach (Item item in items)
        {
            if (item.ItemName == itemName)
            {
                return true; // 아이템 이름이 일치하는 경우 true 반환
            }
        }
        return false; // 아이템을 찾지 못한 경우 false 반환
    }

    public void AddItem(Item _item)
    {
        if (items.Count < slots.Length)
        {
            bool itemExists = false;
            foreach(Item item in items)
            {
                if(item.ItemName == _item.ItemName)
                {
                    itemExists = true;
                    item.count++;
                    if (item.isMoney)
                    {
                        UpdateMoneyText();
                    }
                    break;
                }
            }

            if (!itemExists)
            {
                _item.count = 1;
                items.Add(_item);
                if (_item.isMoney)
                {
                    UpdateMoneyText();
                }
            }
            AddSlot();
        }
        else
        {
            print("슬롯이 가득 차 있습니다.");
        }
    }

    void UpdateMoneyText()
    {
        // 돈 UI 텍스트 업데이트 로직을 추가합니다.
        moneyText.text = money.ToString(); // 예를 들어, 돈을 텍스트에 표시하는 코드
    }


}
