using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class BranchManager : MonoBehaviour
{
    public static BranchManager instance;
    [SerializeField] GameObject[] branchGOs;
    [SerializeField] SpriteRenderer[] fillSprites;
    public TextMeshProUGUI branchText;
    int currentBranchIndex = 0;
    [SerializeField] TeacherDialoige td;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void NextBranch()
    {
        branchGOs[currentBranchIndex].GetComponent<SpriteRenderer>().enabled = false;
        fillSprites[currentBranchIndex].enabled = true;
        currentBranchIndex++;
        if (currentBranchIndex >= branchGOs.Length)
        {
            Debug.Log("Complete");
            td.nextDialogue();

            // SceneManager.LoadScene("Coloring");
            //StartCoroutine(ShowDialogueAndLoadNextScene());
            return;
        }
        branchGOs[currentBranchIndex].SetActive(true);
    }

}
