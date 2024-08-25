using UnityEngine;

public class LifeScript : MonoBehaviour {
    public AudioClip attackSound;
    public float Life {
        get => life; 
        set {
            life = value;
            if(GetComponent<MoveScript>()) {
                AudioSource.PlayClipAtPoint(attackSound, transform.position);
                ServiceManager.Instance.Get<OnLifeChanged>().Invoke(life/ maxLife);
            }
        }
    }
    public float maxLife = 3;
    private float life;

    void Start()
    {
        life = maxLife;
    }
    void CheckForDeath() {
        if(life <= 0) {
            ServiceManager.Instance.Get<OnDeath>().Invoke(gameObject);
            Destroy(gameObject);
        }
    }

    void Update() {
        CheckForDeath();
    }
}