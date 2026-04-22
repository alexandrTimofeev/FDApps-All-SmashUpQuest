using UnityEngine;

public class GunController : MonoBehaviour
{
    public Vector2 accelRangeX = new Vector2(-1f, 1f);
    public Vector2 accelRangeY = new Vector2(-1f, 1f);

    public Vector2 minMaxRotateX = new Vector2(-40f, 15f);
    public Vector2 minMaxRotateY = new Vector2(-30f, 30f);

    public float smooth = 10f;

    private Vector2 currentAngles;

    void Update()
    {
        Vector3 accel = Input.acceleration;

        float tX = Mathf.InverseLerp(accelRangeX.x, accelRangeX.y, accel.x);
        float tY = Mathf.InverseLerp(accelRangeY.x, accelRangeY.y, accel.y);

        float yawTarget = Mathf.Lerp(minMaxRotateY.x, minMaxRotateY.y, tX);
        float pitchTarget = Mathf.Lerp(minMaxRotateX.x, minMaxRotateX.y, tY);

        currentAngles.x = Mathf.Lerp(currentAngles.x, pitchTarget, Time.deltaTime * smooth);
        currentAngles.y = Mathf.Lerp(currentAngles.y, yawTarget, Time.deltaTime * smooth);

        transform.localEulerAngles = new Vector3(currentAngles.x, currentAngles.y, 0f);
        transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);

        /*Debug.Log(
            $"accel: {accel} | " +
            $"tX: {tX:F2} tY: {tY:F2} | " +
            $"pitch: {currentAngles.x:F1} yaw: {currentAngles.y:F1}"
        );*/
    }
}