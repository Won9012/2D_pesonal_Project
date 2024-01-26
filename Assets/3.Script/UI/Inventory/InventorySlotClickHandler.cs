using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotClickHandler : MonoBehaviour
{
    public Slot slot; // ���Կ� ���� ����
    Player_Stat player;
    private int clickCount = 0;
    private float doubleClickTime = 0.3f;
    private float lastClickTime = 0;

    private void Start()
    {
        player = FindObjectOfType<Player_Stat>();
    }

    public void OnSlotClicked()
    {
        float currentTime = Time.time;
        float clickInterval = currentTime - lastClickTime;
        if (clickCount == 0 || clickInterval > doubleClickTime)
        {
            // ù ��° Ŭ�� �Ǵ� ����Ŭ�� �ð� ���� �ʰ�
            clickCount = 1;
        }
        else if (clickCount == 1 && clickInterval <= doubleClickTime)
        {
            // ����Ŭ�� ����
            UseItem();
            clickCount = 0;
        }

        lastClickTime = currentTime;
    }

    private void UseItem()
    {
        if (slot.item != null && slot.item.itemType == Item.ItemType.Potion && slot.item.count > 0)
        {
            slot.item.count--;
            slot.UpdateCountText();
            if (slot.item.count == 0)
            {
                // ��� ������ ��������Ƿ� ������ ����Ʈ���� ����
                Inventory.Instance.items.Remove(slot.item);
                // �κ��丮 UI�� ������Ʈ
                Inventory.Instance.AddSlot();
            }
            if (slot.item.ItemName == "HpPotion")
            {
                player.Curhp += 50;
                if (player.Curhp > player.Maxhp)
                {
                    player.Curhp = player.Maxhp;
                }
            }
        }
    }
}
