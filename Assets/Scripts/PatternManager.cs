using UnityEngine;

public class PatternManager : MonoBehaviour
{
    [SerializeField] private GameObject questionPanel;
    [SerializeField] private GameObject[] prop;
    [SerializeField] private GameObject[] pattern;

    private int randomProp;
    private int randomPattern;

    private void Start()
    {
        if (questionPanel != null && !questionPanel.activeSelf)
        {
            questionPanel.SetActive(true);
        }

        randomProp = Random.Range(1, 3);
        randomPattern = Random.Range(1, 4);
        setPropActive();
        setPatternActive();

        Invoke("disableQuestionPanel", 3);
    }

    private void setPropActive()
    {
        prop[randomProp - 1].SetActive(true);
    }

    private void setPatternActive()
    {
        pattern[randomPattern - 1].SetActive(true);
    }

    private void disableQuestionPanel()
    {
        if (questionPanel != null)
        {
            questionPanel.SetActive(false);
        }
    }
}