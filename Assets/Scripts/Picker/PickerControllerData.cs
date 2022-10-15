using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Picker
{
    [CreateAssetMenu(menuName = "Picker Controller", fileName = "Picker Controller Data")]
    public class PickerControllerData : ScriptableObject
    {
        [SerializeField] private float speed;
        public float Speed { get { return speed; } }

        [SerializeField] private float horizontalCoef;
        public float HorizontalCoef { get { return horizontalCoef; } }

        private float minHorizontal = -8f;
        public float MinHorizontal { get { return minHorizontal; } }

        private float maxHorizontal = 8f;
        public float MaxHorizontal { get { return maxHorizontal; } }
    }
}
