using UnityEngine;

namespace SuperFrank
{
    public class ItemCollectible : Collectible
    {
        [SerializeField] private QuestItem _item;

        public override void Collect()
        {
            base.Collect();
            QuestManager.Instance.Items.Add(_item);
        }
    }
}
