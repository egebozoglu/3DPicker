using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Particle
{
    [CreateAssetMenu(menuName = "Particle Controller", fileName = "Particle Controller Data")]
    public class ParticleControllerData : ScriptableObject
    {
        [SerializeField] private float force;
        public float Force { get { return force; } }

        [SerializeField] private float radius;
        public float Radius { get { return radius; } }
    }
}