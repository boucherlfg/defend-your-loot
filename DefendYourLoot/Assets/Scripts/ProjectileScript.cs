using System;
using System.Linq;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {
    public GameObject attackParticle;

    [Header("Stats")]
    public float attackDamage = 1;
    public float attackRange = 1;

    void CheckForAttack() {

        var hit = Physics2D.OverlapCircleAll(transform.position, attackRange)
                    .Where(x => !x.GetComponent<MoveScript>() && (x.GetComponent<WallScript>() || x.TryGetComponent(out MinionScript minion) && minion.allegiance == Allegiance.Enemy))
                    .FirstOrDefault();
        
        if(!hit) return;
        if(hit.TryGetComponent(out LifeScript life)) {
            ServiceManager.Instance.Get<OnAttacked>().Invoke(gameObject);
            life.Life -= attackDamage;
            Instantiate(attackParticle, hit.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    
    void Update() {
        CheckForAttack();
    }
}