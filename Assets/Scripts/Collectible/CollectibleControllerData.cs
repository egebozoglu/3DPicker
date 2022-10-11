using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.Collectible
{
    [CreateAssetMenu(menuName = "Collectible Controller", fileName = "Collectible Controller Data")]
    public class CollectibleControllerData : ScriptableObject
    {
        [SerializeField] private GameObject collectibleObject;
        public GameObject CollectibleObject { get { return collectibleObject; } }
    }
}