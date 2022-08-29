using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    [SerializeField] GameObject dialogueBackground;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] List<string> lines;
    [SerializeField] int lettersPerSecond = 10;
    [SerializeField] TextMeshProUGUI loadingText;
    [SerializeField] GameObject arrow;

    int currentLine = 0;

    void Start()
    {
        ShowArrow(false);
        loadingText.enabled = false;
        TypeNextLine();
    }

    public void TypeNextLine()
    {
        ShowArrow(false);

        if (currentLine >= lines.Count)
        {
            LoadNextScene();
            return;
        }

        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine()
    {
        dialogueText.text = "";
        foreach (var letter in lines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        currentLine++;
        ShowArrow(true);
    }

    private void ShowArrow(bool isOn)
    {
        arrow.SetActive(isOn);
    }

    private void LoadNextScene()
    {
        loadingText.enabled = true;
        dialogueBackground.SetActive(false);
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }
}
