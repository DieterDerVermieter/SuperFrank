using UnityEngine;

namespace SuperFrank
{
    public struct QuestRuntimeData
    {
        public bool IsActive;
        public int ItemCounter;
    }

    [CreateAssetMenu(menuName = "Quest")]
    public class Quest : ScriptableObject
    {
        public string ActiveText = "Please help me";
        public string DoneText = "Thank you";

        public int NeededAmount;
        public Quest[] NextQuests;

        public QuestRuntimeData Data;
    }
}