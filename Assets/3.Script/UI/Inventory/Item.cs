using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite itemImage;
    public int count; //������ ���� 
    public bool isMoney;
    public int moneyValue;
    public ItemType itemType;

    //�� 30�� ~ 500�� �� �����ϱ� ���� �ʵ�
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
