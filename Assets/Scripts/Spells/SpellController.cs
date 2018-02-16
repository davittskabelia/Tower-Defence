using UnityEngine;

namespace Assets.Scripts.Spells
{
    public abstract class SpellController : MonoBehaviour
    {
        public GameObject ParticleEffect;
        public float Power;

        public abstract void OnCast();

        protected void InstantiateCastEffect(Vector3 position)
        {
            Instantiate(ParticleEffect, position + Vector3.up, Quaternion.identity);
        }


    }
}