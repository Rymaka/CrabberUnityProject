using UnityEngine;

public class SpiderController : MonoBehaviour
{
    public float _speed = 3f;
    [SerializeField] private float smoothness = 5f;
    [SerializeField] private int raysNb = 8;
    [SerializeField] private float raysEccentricity = 0.2f;
    [SerializeField] private float outerRaysOffset = 2f;
    [SerializeField] private float innerRaysOffset = 25f;

    private Vector3 velocity;
    private Vector3 lastVelocity;
    private Vector3 lastPosition;
    private Vector3 forward;
    private Vector3 upward;
    private Quaternion lastRot;
    private Vector3[] pn;

    private static Vector3[] GetClosestPoint(Vector3 point, Vector3 forward, Vector3 up, float halfRange,
        float eccentricity, float offset1, float offset2, int rayAmount)
    {
        var result = new Vector3[2] { point, up };
        var right = Vector3.Cross(up, forward);
        var normalAmount = 1f;
        var positionAmount = 1f;

        var dirs = new Vector3[rayAmount];
        var angularStep = 2f * Mathf.PI / rayAmount;
        var currentAngle = angularStep / 2f;
        for (var i = 0; i < rayAmount; ++i)
        {
            dirs[i] = -up + (right * Mathf.Cos(currentAngle) + forward * Mathf.Sin(currentAngle)) * eccentricity;
            currentAngle += angularStep;
        }

        foreach (var dir in dirs)
        {
            RaycastHit hit;
            var largener = Vector3.ProjectOnPlane(dir, up);
            var ray = new Ray(point - (dir + largener) * halfRange + largener.normalized * offset1 / 100f, dir);
            if (Physics.SphereCast(ray, 0.01f, out hit, 2f * halfRange))
            {
                result[0] += hit.point;
                result[1] += hit.normal;
                normalAmount += 1;
                positionAmount += 1;
            }

            ray = new Ray(point - (dir + largener) * halfRange + largener.normalized * offset2 / 100f, dir);
            if (Physics.SphereCast(ray, 0.01f, out hit, 2f * halfRange))
            {
                result[0] += hit.point;
                result[1] += hit.normal;
                normalAmount += 1;
                positionAmount += 1;
            }
        }

        result[0] /= positionAmount;
        result[1] /= normalAmount;
        return result;
    }

    private void Start()
    {
        velocity = new Vector3();
        forward = transform.forward;
        upward = transform.up;
        lastRot = transform.rotation;
    }

    private void FixedUpdate()
    {
        velocity = (smoothness * velocity + (transform.position - lastPosition)) / (1f + smoothness);
        if (velocity.magnitude < 0.00025f)
            velocity = lastVelocity;
        lastPosition = transform.position;
        lastVelocity = velocity;

        //TODO: Remake this controller to mobile gamepad style + PC (WASD) with unity new input system
        var valueY = Input.GetAxis("Vertical");
        if (valueY != 0)
            transform.position += transform.forward * valueY * _speed * Time.fixedDeltaTime;
        var valueX = Input.GetAxis("Horizontal");
        if (valueX != 0)
            transform.position += Vector3.Cross(transform.up, transform.forward) * valueX * _speed *
                                  Time.fixedDeltaTime;

        if (valueX != 0 || valueY != 0)
        {
            pn = GetClosestPoint(transform.position, transform.forward, transform.up, 0.5f, 0.1f, 30, -30, 4);
            upward = pn[1];
            var position = GetClosestPoint(transform.position, transform.forward, transform.up, 0.5f, raysEccentricity,
                innerRaysOffset, outerRaysOffset, raysNb);
            transform.position = Vector3.Lerp(lastPosition, position[0], 1f / (1f + smoothness));

            forward = velocity.normalized;
            var quaternion = Quaternion.LookRotation(forward, upward);
            transform.rotation = Quaternion.Lerp(lastRot, quaternion, 1f / (1f + smoothness));
        }

        lastRot = transform.rotation;
    }
}