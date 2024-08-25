using System;
using System.Linq;
using UnityEngine;

public class OnAttacked : BaseEvent<GameObject> {}
public class AttackScript : MonoBehaviour {
    public GameObject attackParticle;
    private AIMoveScript move;
    private MinionScript minion;
    

    [Header("Stats")]
    public float attackDamage = 1;
    public float attackRange = 1;
    public float attackCooldown = 1;
    private float attackTimer;

    void CheckForAttack() {
        var hit = Physics2D.OverlapCircleAll(transform.position, attackRange)
                    .Where(x => x.TryGetComponent(out MinionScript m) && m.allegiance != minion.allegiance)
                    .FirstOrDefault();

        if(!hit) return;

        attackTimer += Time.deltaTime;
        if(attackTimer < attackCooldown) return;

        attackTimer = 0;
        hit.GetComponent<LifeScript>().Life -= attackDamage;
        ServiceManager.Instance.Get<OnAttacked>().Invoke(gameObject);
        Instantiate(attackParticle, hit.transform.position, Quaternion.identity);
    }
    
    void Start()
    {
        move = GetComponent<AIMoveScript>();
        minion = GetComponent<MinionScript>();
        attackTimer = attackCooldown;
    }


    void Update() {
        CheckForAttack();
    }
}