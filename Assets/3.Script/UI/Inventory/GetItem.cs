using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    public Item item; // ȹ���� ������
    public float minMoneyDrop = 30f;
    public float maxMoneyDrop = 500f;

    private Material itemMaterial;
    private SpriteRenderer itemRenderer;
    private float fadeOutDuration = 2.0f;
    private Transform playerTransform;
    private float startTime;

    private bool isPlayerInside = false;
    private bool isItemPickUp = false;

    private Rigidbody2D itemRigidbody;

    public float lerpSpeed = 1.0f; // ������ �̵� �ӵ�
    public float fadeOutSpeed = 2.0f; // ������ ����ȭ �ӵ�

    private void Start()
    {
        itemRigidbody = GetComponent<Rigidbody2D>();
        itemMaterial = GetComponent<SpriteRenderer>().material;
        itemRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("���̶��ڳ�?");
            isPlayerInside = true;
            playerTransform = collision.transform;
            startTime = Time.time;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
            playerTransform = null;
        }
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKey(KeyCode.Z) && !isItemPickUp)
        {
            itemRigidbody.mass = 0;

            float distance = Vector3.Distance(transform.position, playerTransform.position);
            float distanceTime = Time.time - startTime;
            float distace_Item = distanceTime * 0.05f;
            float fractionOfJourney = distace_Item / distance;

            if(fractionOfJourney <= 1f)
            {
                transform.position = Vector3.Lerp(transform.position, playerTransform.position + new Vector3(0.7f, -0.5f, 0), fractionOfJourney);
                float alpha = Mathf.Lerp(1.0f, 0.0f, distanceTime / fadeOutDuration);
                itemMaterial.color = new Color(itemMaterial.color.r, itemMaterial.color.g, itemMaterial.color.b, alpha);
                if(fractionOfJourney <= 0.3f)
                {
                    if (item.itemType == Item.ItemType.Coin ||
                        item.itemType == Item.ItemType.Gold ||
                        item.itemType == Item.ItemType.Money ||
                        item.itemType == Item.ItemType.BigMoney)
                    {
                        // �� �������� ���
                        Inventory.Instance.AddMoney(item.moneyValue);
                        SoundManager.instance.PlayGetItem();
                    }
                    else
                    {
                        // �� �������� �ƴ� ���
                        Inventory.Instance.AddItem(item);
                        SoundManager.instance.PlayGetItem();
                    }
                    isItemPickUp = true;
                    Destroy(gameObject);
                }
            }

        }
    }
}

/*
         Invent = GameObject.Find("Canvas");
        inventory = Invent.gameObject.GetComponentsInChildren<Inventory>()[0];
 */
