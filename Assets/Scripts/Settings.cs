using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SuperFrank
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private Toggle _masterToggle, _sfxToggle, _musicToggle;
        [SerializeField] private Button _button;
        [SerializeField] private AudioMixer _audioMixer;
        private void Awake()
        {
            _masterToggle.isOn = _audioMixer.GetFloat("masterVol", out var masterVol) && masterVol > 0f;
            _sfxToggle.isOn = _audioMixer.GetFloat("effectVol", out var effectVol) && effectVol > 0f;
            _musicToggle.isOn = _audioMixer.GetFloat("musicVol", out var musicVol) && musicVol > 0f;
            
            _button.onClick.AddListener(OnButtonClick);
            _masterToggle.onValueChanged.AddListener(OnMasterToggleValueChanged);
            _musicToggle.onValueChanged.AddListener(OnMusicToggleValueChanged);
            _sfxToggle.onValueChanged.AddListener(OnSfxToggleValueChanged);
        }

        private void OnMusicToggleValueChanged(bool toggleState)
        {
            if (_audioMixer != null)
            {
                SoundManager.Instance.PlayButtonSound();
                _audioMixer.SetFloat("musicVol", toggleState ? -80f : -6f);
            }
        }

        private void OnSfxToggleValueChanged(bool toggleState)
        {
            if (_audioMixer != null)
            {
                SoundManager.Instance.PlayButtonSound();
                _audioMixer.SetFloat("effectVol", toggleState ? -80f : -6f);
            }
        }
        
        private void OnMasterToggleValueChanged(bool toggleState)
        {
            if (_audioMixer != null)
            {
                SoundManager.Instance.PlayButtonSound();
                _audioMixer.SetFloat("masterVol", toggleState ? -80f : -6f);
            }
        }

        private void OnButtonClick()
        {
            SoundManager.Instance.PlayButtonSound();
            gameObject.SetActive(false);
        }
        
        
    }
}
