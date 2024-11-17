using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperFrank
{
    public class FInalQuestTrigger : MonoBehaviour
    {
        [SerializeField] private Quest _finalQuest;
        [SerializeField] private int _index = 2;


        private bool _isActive;


        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("YOu cheater!!!");
                _finalQuest.Data.ItemCounter = 10000;
            }

            if (!_isActive && _finalQuest.Data.IsActive)
            {
                _isActive = true;
            }
            else if(_isActive && !_finalQuest.Data.IsActive)
            {
                SceneManager.LoadScene(_index);
            }
#endif
        }
    }
}
