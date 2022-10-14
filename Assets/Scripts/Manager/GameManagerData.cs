using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Manager
{
    [CreateAssetMenu(menuName = "Game Manager Data", fileName = "Game Manager Data")]
    public class GameManagerData : ScriptableObject
    {
        [SerializeField] private int fpsRate;
        public int FpsRate { get { return fpsRate; } }
    }
}