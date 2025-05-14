using Core;
using Events;
using UnityEngine;

namespace Player
{
    public class LifeScript : MonoBehaviour 
    {
        public AudioClip attackSound;
        public float Life {
            get => life; 
            set {
                life = value;
                if (!TryGetComponent<MoveScript>(out _)) return;
            
                AudioSource.PlayClipAtPoint(attackSound, transform.position);
                ServiceManager.Instance.Get<OnLifeChanged>().Invoke(life/ maxLife);
            }
        }
        public float maxLife = 3;
        private float life;

        private void Start()
        {
            life = maxLife;
        }

        private void CheckForDeath()
        {
            if (!(life <= 0)) return;
            
            ServiceManager.Instance.Get<OnDeath>().Invoke(gameObject);
        }

        private void Update() {
            CheckForDeath();
        }
    }
}