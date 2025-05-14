using Player;
using UnityEngine;

namespace Utility
{
    public class FollowPlayer : MonoBehaviour
    {
        private Camera _mainCamera;
        [SerializeField]
        private float biasTowardsPlayer = 5;
        [SerializeField]
        private float biasTowardsMouse = 1;
        private MoveScript player;
        // Start is called before the first frame update
        void Start()
        {
            _mainCamera = Camera.main;
            player = FindObjectOfType<MoveScript>();
        }

        // Update is called once per frame
        void Update()
        {
            if(!player) return;

            var pos = transform.position;
            var mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            pos = Vector2.Lerp(pos, player.transform.position, Time.deltaTime * biasTowardsPlayer);
            pos = Vector2.Lerp(pos, mousePos, Time.deltaTime * biasTowardsMouse);
            pos.z = transform.position.z;
            transform.position = pos;
        }
    }
}
