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

        // 'I' 키를 누르면 인벤토리 토글
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnOffInventory();
        }
    }

    public void OnOffInventory()
    {
        isInventoryActive = !isInventoryActive;

        // 인벤토리 UI를 활성화 또는 비활성화
        Transform inventoryUI = transform.Find("Inventory");

        if (inventoryUI != null)
        {
            inventoryUI.gameObject.SetActive(isInventoryActive);
        }
    }
}