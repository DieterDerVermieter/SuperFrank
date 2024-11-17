using UnityEngine;

namespace SuperFrank
{
    public class QuestRuntimeData
    {
        public bool IsActive;
        public int ItemCounter;
    }

    [CreateAssetMenu(menuName = "Quest")]
    public class Quest : ScriptableObject
    {
        [TextArea(2, 10)] public string ActiveText = "Please help me";
        [TextArea(2, 10)] public string DoneText = "Thank you";

        public Response[] ActiveResponses;
        public Response[] DoneResponses;

        public int NeededAmount;
        public Quest[] NextQuests;
        public Quest IncreaseQuest;

        public QuestRuntimeData Data = new();
    }
}