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
            SceneManager.LoadScene("Coloring");
            //StartCoroutine(ShowDialogueAndLoadNextScene());
            return;
        }
        branchGOs[currentBranchIndex].SetActive(true);
    }

    //IEnumerator ShowDialogueAndLoadNextScene()
    //{

    //    yield return new WaitForSeconds(2f); //wait dialogue lai

    //    string currentScene = SceneManager.GetActiveScene().name;
    //    Debug.Log(currentScene);

    //    if (currentScene == "Square")
    //    {
    //        SceneManager.LoadScene("Cube");
    //        Debug.Log("Change");
    //    }
    //    else if (currentScene == "Cube")
    //    {
    //        SceneManager.LoadScene("Coloring");
    //    }
    //}
}
