using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Picker
{
    [CreateAssetMenu(menuName = "Picker Controller", fileName = "Picker Controller Data")]
    public class PickerControllerData : ScriptableObject
    {
        [SerializeField] private float verticalSpeed;
        public float VerticalSpeed { get { return verticalSpeed; } }

        [SerializeField] private float horizontalSpeed;
        public float HorizontalSpeed { get { return horizontalSpeed; } }
    }
}
