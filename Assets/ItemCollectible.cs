using UnityEngine;

namespace SuperFrank
{
    public class ItemCollectible : Collectible
    {
        [SerializeField] private QuestItem _item;

        public override void Collect()
        {
            base.Collect();
            int count = QuestManager.Instance.GetItemCount(_item);
            QuestManager.Instance.SetItemCount(_item, count + 1);
        }
    }
}
