using UnityEngine;
using TMPro;
using System.Collections;

public class TeacherDialoige : MonoBehaviour
{
    [SerializeField] string[] stringTexts;
    [SerializeField] TextMeshProUGUI teacherDialogueText;
    int stringIndex = 0;
    [SerializeField] GameObject bookGO;
    [SerializeField] GameObject squareGO;
    [SerializeField] GameObject cubeGO;
    [SerializeField] GameObject painting1GO;
    [SerializeField] GameObject lastPaintingGO;
    [SerializeField] FindObjectManager fom;

    private void Start()
    {
        Time.timeScale = 2f;
        nextDialogue();
    }
    public void nextDialogue()
    {
        teacherDialogueText.text = stringTexts[stringIndex];
        switch (stringIndex)
        {
            case 0:
                StartCoroutine(EnableObjects(new GameObject[] { bookGO, squareGO }, new bool[] { true, true }, 4f));
                break;
            case 1:
                StartCoroutine(EnableObjects(new GameObject[] { squareGO, bookGO }, new bool[] { false, false }, 1f));
                break;
            case 8:
                StartCoroutine(EnableObjects(new GameObject[] { bookGO, cubeGO }, new bool[] { true, true }, 4f));
                break;
            case 9:
                StartCoroutine(EnableObjects(new GameObject[] { painting1GO, bookGO }, new bool[] { true, false }, 3f));
                break;
            case 10:
                StartCoroutine(EnableObjects(new GameObject[] { lastPaintingGO }, new bool[] { false }, 1f));
                break;
            case 13:
                fom.enabled = true;
                break;

        }
        stringIndex++;
        if (stringIndex <= 8 && stringIndex>1)
        {
            Invoke("nextDialogue", 4f);
        }
        else if(stringIndex>=11 && stringIndex <= 13)
        {
            Invoke("nextDialogue", 4f);
        }
    }

    IEnumerator EnableObjects(GameObject[] gos, bool[] bools, float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        for (int i = 0; i < gos.Length; i++)
        {
            gos[i].SetActive(bools[i]);
        }
    }
}
