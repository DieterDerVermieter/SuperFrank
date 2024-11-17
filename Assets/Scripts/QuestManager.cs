using SuperFrank;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private Quest _startQuest;

    [SerializeField] private List<Quest> _allQuests;
    [SerializeField] private List<Quest> _activeQuests;


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

    private void Update()
    {
        _activeQuests.Clear();
        _activeQuests.AddRange(_allQuests.Where(q => q.Data.IsActive));
    }
}