using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletPoint : MonoBehaviour
{
    [SerializeField] int lettersPerSecond = 50;
    [SerializeField] TextMeshProUGUI bulletPointTextBox;
    [SerializeField] string bulletPoint;

    void Start()
    {
        
    }

    IEnumerator TypeBulletPoint()
    {
        bulletPointTextBox.text = "";
        foreach (var letter in bulletPoint.ToCharArray())
        {
            bulletPointTextBox.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(TypeBulletPoint());
    }
}
