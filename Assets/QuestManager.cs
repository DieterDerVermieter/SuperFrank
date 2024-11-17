using SuperFrank;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Quest _startQuest;


    public static QuestManager Instance;


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

        _startQuest.Data.IsActive = true;
    }
}