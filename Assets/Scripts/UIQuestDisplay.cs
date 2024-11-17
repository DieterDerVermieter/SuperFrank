using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SuperFrank
{
    public class UIQuestDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Image _iconImage;

        private Quest _quest;

        public void SetQuest(Quest quest)
        {
            _quest = quest;
            _nameText.text = quest.name;
        }

        private void Update()
        {
            bool hasActiveQuest = false;
            if (_quest.Data.IsActive)
            {
                bool hasItems = _quest.Data.ItemCounter >= _quest.NeededAmount;
                _iconImage.color = hasItems ? Color.green : Color.yellow;
                hasActiveQuest = true;
            }
            _iconImage.gameObject.SetActive(hasActiveQuest);
        }
    }
}
