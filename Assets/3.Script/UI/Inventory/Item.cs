using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite itemImage;
    public int count; //아이템 갯수 
    public bool isMoney;
    public int moneyValue;
    public ItemType itemType;

    //돈 30원 ~ 500원 을 관리하기 위한 필드
    public enum ItemType
    {
        Coin,
        Gold,
        Money,
        BigMoney,
        MonItem,
        Potion
    }
}
