using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class DmgText : MonoBehaviour
{
    public GameObject[] DmgPrefab;
    public float displayDuration = 1f;

    private float elapsedTime = 0;
    private GameObject damageTextObject;

    public void ShowDamageText(int damage)
    {
        string damageString = damage.ToString(); //������� ���ڿ���
        char[] damageChars = damageString.ToCharArray(); // ���ڿ��� ���� �迭�� ( "123" => "1" ,"2" , "3")
        foreach(char c in damageChars)
        {
            int digit = int.Parse(c.ToString()); // ���ڸ� ������
            //���� ���ڿ� �´� �������� �����ؼ� ����
            int prefabIndex = Mathf.Clamp(digit, 0, DmgPrefab.Length - 1);
            GameObject selectedPrefab = DmgPrefab[prefabIndex]; // 
        }

    }

    IEnumerator DisplayDamageText_co()
    {
        damageTextObject.SetActive(true);
        while(elapsedTime < displayDuration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        damageTextObject.SetActive(false);
        Destroy(damageTextObject);

        elapsedTime = 0;
    }


}
*/
public class DmgText : MonoBehaviour
{
    public GameObject[] DmgPrefab;
    public float displayDuration = 0.5f;


    public void ShowDamageText(int damage, Vector3 position)
    {
        string damageString = damage.ToString();
        char[] damageChars = damageString.ToCharArray();

        Vector3 currentPosition = position;

        foreach (char c in damageChars)
        {
            int digit = int.Parse(c.ToString());
            int prefabIndex = Mathf.Clamp(digit, 0, DmgPrefab.Length - 1);
            GameObject selectedPrefab = DmgPrefab[prefabIndex];

            GameObject damageTextObject = Instantiate(selectedPrefab, currentPosition, Quaternion.identity);
            Destroy(damageTextObject, 0.3f);
            // ���� �������� ǥ���� ��ġ�� ���� (��: Y������ �ణ �̵�)
            currentPosition.x += 0.5f;
        }
    }
}  