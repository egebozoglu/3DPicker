using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Camera
{
    [CreateAssetMenu(menuName = "Camera Controller", fileName = "Camera Controller Data")]
    public class CameraControllerData : ScriptableObject
    {
        [SerializeField] private Vector3 offset;
        public Vector3 Offset { get { return offset; } }

        [SerializeField] private float lerpTime;
        public float LerpTime { get { return lerpTime; } }
    }
}
