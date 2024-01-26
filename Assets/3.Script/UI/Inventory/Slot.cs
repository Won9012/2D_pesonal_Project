using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text countText;

    Player_Stat player;
    InventorySlotClickHandler inventorySlotClickHandler;

    private Item _item;
  
    private int count = 0;
    private float ResetTime = 0f;

    private void Awake()
    {
        ResetTime = Time.time;
    }
    private void Start()
    {
        player = FindObjectOfType<Player_Stat>();
        inventorySlotClickHandler = FindObjectOfType<InventorySlotClickHandler>();
    }

    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1);
                UpdateCountText();
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
                //템없을때는 카운트택스트 공백
                countText.text = "";
            }
        }
    }

    public void UpdateCountText()
    {
        if(_item != null)
        {
            //아이템 갯수 표시 
            countText.text = _item.count.ToString(); 
        }
    }
}
