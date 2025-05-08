using UnityEngine;

public class Dialoguemanager : MonoBehaviour
{
    public class DialogueLine
    {
        public string characterName;
        [TextArea(3, 10)] public string text;
        public Sprite characterPortrait;
        public AudioClip voiceClip;
        public List<DialogueOption> options;
        public UnityEvent onDialogueEvent;
    }

    [System.Serializable]
    public class DialogueOption
    {
        public string optionText;
        public int nextNodeIndex;
        public bool requiresCondition;
        public string conditionName;
        public UnityEvent onSelectEvent;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(DialogueTree tree)
    {
        if (tree == null || tree.dialogueNodes.Count == 0)
        {
            Debug.LogWarning("Tried to start empty dialogue tree");
            return;
        }

        currentTree = tree;
        currentNodeIndex = 0;
        dialoguePanel.SetActive(true);

    }
}
