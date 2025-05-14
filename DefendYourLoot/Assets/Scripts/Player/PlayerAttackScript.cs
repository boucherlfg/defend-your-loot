using Core;
using Events;
using Managers;
using UnityEngine;
using Utility;

namespace Player
{
    public class PlayerAttackScript : MonoBehaviour
    {
        public float Mana
        {
            get => currentCharge;
            set
            {
                currentCharge = Mathf.Min(value, charge);
                onManaChanged.Invoke(currentCharge / charge);
            }
        }
        
        private Camera _mainCamera;
        public float cost = 3;
        public float projectileSpeed = 8;
        public float charge = 10;
        private float currentCharge;
        public GameObject projectile;
        private GameState gameState;
        
        private OnManaChanged onManaChanged;
        // Start is called before the first frame update
        private void Start()
        {
            _mainCamera = Camera.main;
            currentCharge = charge;
            
            gameState = ServiceManager.Instance.Get<GameState>();
            onManaChanged = ServiceManager.Instance.Get<OnManaChanged>();
        }

        // Update is called once per frame
        private void Update()
        {
            if(gameState.State != GameStateEnum.Level) return;

            currentCharge += Time.deltaTime;
            currentCharge = Mathf.Min(charge, currentCharge);
        
            onManaChanged.Invoke(currentCharge / charge);
            if(currentCharge < cost) return;
        
            if(!Input.GetMouseButtonDown(0)) return;
        
            currentCharge -= cost;

            var mouse = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 delta = mouse - transform.position;
            delta = delta.normalized;

            var instance = Instantiate(projectile, transform.position, Quaternion.identity);
            if (!instance.TryGetComponent(out Rigidbody2D rigidBody)) return;
            rigidBody.velocity = delta * projectileSpeed;

            if (!instance.TryGetComponent(out ProjectileScript proj)) return;
            proj.attackSource = gameObject;
        }
    }
}
