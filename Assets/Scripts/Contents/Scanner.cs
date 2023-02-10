using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] _targets;
    public Transform NearestTarget { get; set; }
    public List<Transform> ListNearestTargets = new List<Transform>();

    void FixedUpdate()
    {
        _targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        NearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float dist = 100;

        foreach (RaycastHit2D target in _targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);
            if (curDiff < dist)
            {
                dist = curDiff;
                result = target.transform;
                ListNearestTargets.Add(result);
            }
        }
        return result;
    }

    public List<Transform> GetNearestTargets()
    {
        return Enumerable.Reverse(ListNearestTargets).ToList();
    }
}
