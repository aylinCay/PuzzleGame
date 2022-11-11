using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame
{
   public class CoreBlue : MonoBehaviour, ICore
   {
      [SerializeField] private CoreColor _color = CoreColor.Blue;
      public CoreColor getColor => _color;
      private Transform _transform;
      public Transform getTransform
      {
         get => (_transform == null ? _transform = transform : _transform);
         set => _transform = value;
      }
      public void Move(Vector3 pos)
      {
         if(getTransform.position != pos) getTransform.position = Vector3.Lerp(getTransform.position,pos,10f * Time.deltaTime);
      }
   }
}
