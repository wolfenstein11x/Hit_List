using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletPoints : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] bulletPointsText;
    [SerializeField] List<string> bulletPoints;
    [SerializeField] int lettersPerSecond = 50;
    [SerializeField] GameObject arrow;

    int currentBulletPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TypeNextBulletPoint()
    {
        ShowArrow(false);

        if (currentBulletPoint >= bulletPointsText.Length)
        {
            LoadNextScene();
            return;
        }

        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine()
    {
        bulletPointsText[currentBulletPoint].text = "";
        foreach (var letter in bulletPoints[currentBulletPoint].ToCharArray())
        {
            bulletPointsText[currentBulletPoint].text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        currentBulletPoint++;
        ShowArrow(true);
    }

    private void ShowArrow(bool isOn)
    {
        arrow.SetActive(isOn);
    }

    private void LoadNextScene()
    {
        //loadingText.enabled = true;
        //dialogueBackground.SetActive(false);
        //FindObjectOfType<LevelLoader>().LoadNextLevel();
    }
}
