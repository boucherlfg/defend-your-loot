using System;
using System.Linq;
using Pathfinding;
using UnityEngine;

public class AIMoveScript : MonoBehaviour {
    private AIPath aiPath;
    private MinionScript minion;
    private Vector2 startPosition;
    private Vector2? target;

    public IdleType idleType;
    public float range = 3;

    private Transform targetOverride;
    public Vector2? Target => target;

    // Start is called before the first frame update
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        minion = GetComponent<MinionScript>();
        ServiceManager.Instance.Get<OnAttacked>().Subscribe(HandleAttacked);
        startPosition = transform.position;
        target = startPosition;
    }

    void OnDestroy() {
        ServiceManager.Instance.Get<OnAttacked>().Unsubscribe(HandleAttacked);

    }

    private void HandleAttacked(GameObject source)
    {
        if(minion.allegiance == Allegiance.Ally) return;
        if(!source.GetComponent<ProjectileScript>()) return;
        targetOverride = FindObjectOfType<MoveScript>().transform;
    }

    void Update()
    {
        CheckForFollow();
        
        if(target != null && Vector2.Distance(target.Value, transform.position) < 0.3f) target = null;
    }
    
    private Vector2 Roam() {
        if(target != null) return target.Value;
        return FindObjectsOfType<Waypoint>().GetRandom().transform.position;
    }

    private Vector2 IdlePosition() => idleType switch { IdleType.StartPos => startPosition, IdleType.Roam => Roam(), _ => throw new System.NotImplementedException() };
    
    void CheckForFollow() {
        if(targetOverride) {
            target = targetOverride.position;
            aiPath.destination = target.Value;
            return;
        }
        var hit = Physics2D.OverlapCircleAll(transform.position, range)
                    .Where(x => x.TryGetComponent(out MinionScript m) && m.allegiance != minion.allegiance)
                    .OrderBy(x => Vector2.Distance(x.transform.position, transform.position))
                    .FirstOrDefault();

        if(hit) {
            target = hit.transform.position;
            aiPath.destination = target.Value;
        }
        else {
            target = IdlePosition();
            aiPath.destination = target.Value;
        }
    }
}