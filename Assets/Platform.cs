using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wakaba.Mobile;
public class Platform : MonoBehaviour
{
    private Transform platform;
    private void Start() => MobileInput.Initialise();
    private void Update() => platform.rotation = Input.gyro.attitude;
}