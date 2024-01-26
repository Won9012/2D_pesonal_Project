using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private bool isInventoryActive = false;

    public GameObject UI;

    private void Start()
    {
        UI.SetActive(false);
    }

    void Update()
    {

        // 'I' Ű�� ������ �κ��丮 ���
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnOffInventory();
        }
    }

    public void OnOffInventory()
    {
        isInventoryActive = !isInventoryActive;

        // �κ��丮 UI�� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
        Transform inventoryUI = transform.Find("Inventory");

        if (inventoryUI != null)
        {
            inventoryUI.gameObject.SetActive(isInventoryActive);
        }
    }
}