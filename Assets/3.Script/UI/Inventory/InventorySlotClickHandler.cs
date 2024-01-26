using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotClickHandler : MonoBehaviour
{
    public Slot slot; // 슬롯에 대한 참조
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
            // 첫 번째 클릭 또는 더블클릭 시간 간격 초과
            clickCount = 1;
        }
        else if (clickCount == 1 && clickInterval <= doubleClickTime)
        {
            // 더블클릭 감지
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
                // 모든 포션을 사용했으므로 아이템 리스트에서 제거
                Inventory.Instance.items.Remove(slot.item);
                // 인벤토리 UI를 업데이트
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
