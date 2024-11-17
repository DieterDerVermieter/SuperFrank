using UnityEngine;

namespace SuperFrank
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private Quest _questTrigger;


        private GameObject[] _items;
        private GameObject[] _spawnedItems;

        private bool _isSpawned;


        private void Awake()
        {
            _items = new GameObject[transform.childCount];
            _spawnedItems = new GameObject[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject item = transform.GetChild(i).gameObject;
                item.SetActive(false);
                _items[i] = item;
            }
        }


        private void Update()
        {
            if (!_isSpawned && _questTrigger.Data.IsActive)
            {
                Spawn();
                _isSpawned = true;
            }
            else if(_isSpawned && !_questTrigger.Data.IsActive)
            {
                DespawnAll();
                _isSpawned = false;
            }
        }


        public void Spawn()
        {
            DespawnAll();
            for (int i = 0; i < _items.Length; i++)
            {
                _spawnedItems[i] = Instantiate(_items[i], transform);
                _spawnedItems[i].transform.localPosition = _items[i].transform.localPosition;
                _spawnedItems[i].transform.localRotation = _items[i].transform.localRotation;
                _spawnedItems[i].transform.localScale = _items[i].transform.localScale;
                _spawnedItems[i].SetActive(true);
            }
        }

        public void DespawnAll()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_spawnedItems[i] != null)
                {
                    Destroy(_spawnedItems[i]);
                    _spawnedItems[i] = null;
                }
            }
        }
    }
}
