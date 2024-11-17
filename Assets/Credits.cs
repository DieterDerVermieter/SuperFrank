using UnityEngine;
using UnityEngine.UI;

namespace SuperFrank
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SoundManager.Instance.PlayButtonSound();
            gameObject.SetActive(false);
        }
    }
}
