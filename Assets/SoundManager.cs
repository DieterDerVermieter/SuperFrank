using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperFrank
{
    
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _bgMusic;
        [SerializeField] private AudioSource _sfx;
        [SerializeField] private AudioClip _buttonClip;
        
        public static SoundManager Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void PlayBgMusic(AudioClip clip)
        {
            _bgMusic.clip = clip;
            _bgMusic.Play();
        }
        
        public void PlaySfx(AudioClip clip)
        {
            _sfx.PlayOneShot(clip);
        }
        
        public void PlayButtonSound()
        {
            PlaySfx(_buttonClip);
        }
        
    }
}
