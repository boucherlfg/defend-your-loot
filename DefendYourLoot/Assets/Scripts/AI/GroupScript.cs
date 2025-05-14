using UnityEngine;

namespace AI
{
    public class GroupScript : MonoBehaviour
    {
        // Update is called once per frame
        private void Update()
        {
            if (transform.childCount > 0) return;
            Destroy(gameObject);
        }
    }
}
