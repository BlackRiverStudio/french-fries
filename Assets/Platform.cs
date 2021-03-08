using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wakaba.Mobile;
public class Platform : MonoBehaviour
{
    private Transform platform;
    private void Start() => MobileInput.Initialise();
    private void Update() => MovePlatform();
    private void MovePlatform() => platform.rotation = GyroToUnity(Input.gyro.attitude);
    private static Quaternion GyroToUnity(Quaternion _q) => new Quaternion(_q.x, _q.y, -_q.z, -_q.w);
    /* private void Update() => GyroModifyCamera();
     * private void GyroModifyCamera() => transform.rotation = GyroToUnity(Input.gyro.attitude);
     * private static Quaternion GyroToUnity(Quaternion _q) => new Quaternion(_q.x, _q.y, -_q.z, -_q.w);
     */
}