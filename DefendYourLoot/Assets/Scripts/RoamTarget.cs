using System.Collections.Generic;
using UnityEngine;

public class RoamTarget : MonoBehaviour {
    public List<Vector2> targets;
    void OnDrawGizmos() {
        targets.ForEach(t => Gizmos.DrawSphere(t, 0.2f));
    }
}