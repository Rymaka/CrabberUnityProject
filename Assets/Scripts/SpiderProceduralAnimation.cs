using System.Collections;
using UnityEngine;

public class SpiderProceduralAnimation : MonoBehaviour
{
    [SerializeField] private Transform[] legTargets;
    [SerializeField] private float stepSize = 0.15f;
    [SerializeField] private int smoothness = 8;
    [SerializeField] private float stepHeight = 0.15f;
    [SerializeField] private float sphereCastRadius = 0.125f;
    [SerializeField] private bool bodyOrientation = false;

    [SerializeField] private float raycastRange = 1.5f;
    private Vector3[] defaultLegPositions;
    private Vector3[] lastLegPositions;
    private Vector3 lastBodyUp;
    private bool[] legMoving;
    private int nbLegs;

    private Vector3 velocity;
    private Vector3 lastVelocity;
    private Vector3 lastBodyPos;

    private float velocityMultiplier = 15f;

    private Vector3[] MatchToSurfaceFromAbove(Vector3 point, float halfRange, Vector3 up)
    {
        var res = new Vector3[2];
        res[1] = Vector3.zero;
        RaycastHit hit;
        var ray = new Ray(point + halfRange * up / 2f, -up);

        if (Physics.SphereCast(ray, sphereCastRadius, out hit, 2f * halfRange))
        {
            res[0] = hit.point;
            res[1] = hit.normal;
        }
        else
        {
            res[0] = point;
        }

        return res;
    }

    private void Start()
    {
        lastBodyUp = transform.up;

        nbLegs = legTargets.Length;
        defaultLegPositions = new Vector3[nbLegs];
        lastLegPositions = new Vector3[nbLegs];
        legMoving = new bool[nbLegs];
        for (int i = 0; i < nbLegs; ++i)
        {
            defaultLegPositions[i] = legTargets[i].localPosition;
            lastLegPositions[i] = legTargets[i].position;
            legMoving[i] = false;
        }

        lastBodyPos = transform.position;
    }

    private IEnumerator PerformStep(int index, Vector3 targetPoint)
    {
        var startPos = lastLegPositions[index];
        for (int i = 1; i <= smoothness; ++i)
        {
            legTargets[index].position = Vector3.Lerp(startPos, targetPoint, i / (smoothness + 1f));
            legTargets[index].position +=
                transform.up * Mathf.Sin(i / (smoothness + 1f) * Mathf.PI) * stepHeight;
            yield return new WaitForFixedUpdate();
        }

        legTargets[index].position = targetPoint;
        lastLegPositions[index] = legTargets[index].position;
        legMoving[0] = false;
    }


    private void FixedUpdate()
    {
        velocity = transform.position - lastBodyPos;
        velocity = (velocity + smoothness * lastVelocity) / (smoothness + 1f);

        if (velocity.magnitude < 0.000025f)
        {
            velocity = lastVelocity;
        }
        else
        {
            lastVelocity = velocity;
        }
        
        Vector3[] desiredPositions = new Vector3[nbLegs];
        var indexToMove = -1;
        var maxDistance = stepSize;
        for (int i = 0; i < nbLegs; ++i)
        {
            desiredPositions[i] = transform.TransformPoint(defaultLegPositions[i]);

            var distance = Vector3
                .ProjectOnPlane(desiredPositions[i] + velocity * velocityMultiplier - lastLegPositions[i], transform.up)
                .magnitude;
            if (distance > maxDistance)
            {
                maxDistance = distance;
                indexToMove = i;
            }
        }

        for (int i = 0; i < nbLegs; ++i)
            if (i != indexToMove)
                legTargets[i].position = lastLegPositions[i];

        if (indexToMove != -1 && !legMoving[0])
        {
            var targetPoint = desiredPositions[indexToMove] +
                                  Mathf.Clamp(velocity.magnitude * velocityMultiplier, 0.0f, 1.5f) *
                                  (desiredPositions[indexToMove] - legTargets[indexToMove].position) +
                                  velocity * velocityMultiplier;

            Vector3[] positionAndNormalFwd = MatchToSurfaceFromAbove(targetPoint + velocity * velocityMultiplier,
                raycastRange, (transform.parent.up - velocity * 100).normalized);
            Vector3[] positionAndNormalBwd = MatchToSurfaceFromAbove(targetPoint + velocity * velocityMultiplier,
                raycastRange * (1f + velocity.magnitude), (transform.parent.up + velocity * 75).normalized);

            legMoving[0] = true;

            if (positionAndNormalFwd[1] == Vector3.zero)
            {
                StartCoroutine(PerformStep(indexToMove, positionAndNormalBwd[0]));
            }
            else
            {
                StartCoroutine(PerformStep(indexToMove, positionAndNormalFwd[0]));
            }
        }

        lastBodyPos = transform.position;
        if (nbLegs > 3 && bodyOrientation)
        {
            var v1 = legTargets[0].position - legTargets[1].position;
            var v2 = legTargets[2].position - legTargets[3].position;
            var normal = Vector3.Cross(v1, v2).normalized;
            var up = Vector3.Lerp(lastBodyUp, normal, 1f / (float)(smoothness + 1));
            transform.up = up;
            transform.rotation = Quaternion.LookRotation(transform.parent.forward, up);
            lastBodyUp = transform.up;
        }
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < nbLegs; ++i)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(legTargets[i].position, 0.05f);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.TransformPoint(defaultLegPositions[i]), stepSize);
        }
    }
}