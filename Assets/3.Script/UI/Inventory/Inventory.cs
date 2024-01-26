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
        moneyText.text = money.ToString(); // ���� �ؽ�Ʈ�� ǥ��
        // ���� �߰��Ǿ��� �� UI�� ������Ʈ�ϴ� �ڵ� �߰�
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
                return true; // ������ �̸��� ��ġ�ϴ� ��� true ��ȯ
            }
        }
        return false; // �������� ã�� ���� ��� false ��ȯ
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
            print("������ ���� �� �ֽ��ϴ�.");
        }
    }

    void UpdateMoneyText()
    {
        // �� UI �ؽ�Ʈ ������Ʈ ������ �߰��մϴ�.
        moneyText.text = money.ToString(); // ���� ���, ���� �ؽ�Ʈ�� ǥ���ϴ� �ڵ�
    }


}
