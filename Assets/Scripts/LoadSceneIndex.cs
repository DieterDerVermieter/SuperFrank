using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperFrank
{
    public class LoadSceneIndex : MonoBehaviour
    {
        [SerializeField] private float _duration = 2.0f;
        [SerializeField] private int _index = 1;


        private IEnumerator Start()
        {
            yield return new WaitForSeconds(_duration);
            SceneManager.LoadScene(_index);
        }
    }
}
