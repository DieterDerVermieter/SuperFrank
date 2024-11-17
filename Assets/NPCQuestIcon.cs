using UnityEngine;
using UnityEngine.UI;

namespace SuperFrank
{
    public class NPCQuestIcon : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;

        private Camera _camera;

        [HideInInspector] public Quest Quest;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            // QuestStatus status = QuestManager.Instance.GetQuestStatus(Quest);
            // bool hasStartItems = QuestManager.Instance.HasItems(Quest.StartItems);
            bool hasCollectItems = QuestManager.Instance.HasItems(Quest.CollectItems);
            _iconImage.color = hasCollectItems ? Color.green : Color.yellow;
            transform.rotation = _camera.transform.rotation;
        }
    }
}
