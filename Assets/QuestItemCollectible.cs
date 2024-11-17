using UnityEngine;

namespace SuperFrank
{
    public class QuestItemCollectible : Collectible
    {
        [SerializeField] private Quest _quest;

        public override void Collect()
        {
            base.Collect();
            _quest.Data.ItemCounter++;
        }
    }
}
