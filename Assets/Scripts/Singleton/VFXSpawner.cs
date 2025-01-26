using UnityEngine;

namespace TKM
{
    public class VFXSpawner : Singleton<VFXSpawner>
    {
        [SerializeField] GameObject _deadParticleSystem;

        public void PlayDeadVFX(Vector2 pos)
        {
            Instantiate(_deadParticleSystem, pos, Quaternion.identity);
        }
    }
}
