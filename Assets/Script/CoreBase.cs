using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame
{
    public class CoreBase : MonoBehaviour,ICore
    {
        [SerializeField] private CoreColor _color;
        public CoreColor getColor => _color;
        private Transform _transform;

        public Transform getTransform
        {
            get => (_transform == null ? _transform = transform : _transform);
            set => _transform = value;
        }
    
        

        public void Move(Vector3 pos)
        {
           
        }

        public CoreBase()
        {
            
        }
    }
}