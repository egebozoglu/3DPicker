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

        private float minHorizontal = -8f;
        public float MinHorizontal { get { return minHorizontal; } }

        private float maxHorizontal = 8f;
        public float MaxHorizontal { get { return maxHorizontal; } }
    }
}
