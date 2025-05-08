using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentStage = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoToNextStage()
    {
        currentStage++;

        switch (currentStage)
        {
            case 1:
                SceneManager.LoadScene("Cube"); break;
            case 2:
                SceneManager.LoadScene("Coloring"); break;
            case 3:
                SceneManager.LoadScene("Maze"); break;
            case 4:
                SceneManager.LoadScene("Find"); break;
            default:
                Debug.Log("Game Finished."); break;
        }
    }
}
