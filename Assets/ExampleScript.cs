// Create a cube with camera vector names on the faces.
// Allow the device to show named faces as it is oriented.

using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    protected void Update()
    {
        GyroModifyCamera();
    }

    /********************************************/

    // The Gyroscope is right-handed.  Unity is left handed.
    // Make the necessary change to the camera.
    void GyroModifyCamera()
    {
        transform.rotation = GyroToUnity(Input.gyro.attitude);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}