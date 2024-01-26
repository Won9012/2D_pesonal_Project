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
        string damageString = damage.ToString(); //대미지를 문자열로
        char[] damageChars = damageString.ToCharArray(); // 문자열을 문자 배열로 ( "123" => "1" ,"2" , "3")
        foreach(char c in damageChars)
        {
            int digit = int.Parse(c.ToString()); // 문자를 정수로
            //이제 숫자에 맞는 프리팹을 선택해서 생성
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
            // 다음 프리팹을 표시할 위치를 조정 (예: Y축으로 약간 이동)
            currentPosition.x += 0.5f;
        }
    }
}  