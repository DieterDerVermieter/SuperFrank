using UnityEngine;

namespace SuperFrank
{
    public class DestroySystemWhenFinished : MonoBehaviour
    {
        private ParticleSystem _system;


        private void Awake()
        {
            _system = GetComponent<ParticleSystem>();
            if (_system == null) enabled = false;
        }

        private void Update()
        {
            if (!_system.IsAlive())
                Destroy(gameObject);
        }
    }
}
