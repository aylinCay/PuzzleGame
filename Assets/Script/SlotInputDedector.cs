using System;
using Unity.VisualScripting;
using UnityEngine;

namespace PuzzleGame
{
    public class SlotInputDedector : MonoBehaviour
    {
        private bool _isClick;
        private Vector2 _firstPos;
        private Vector2 _secondPos;
        private Vector2 _result;

        private Slot _slot;

        private void Start()
        {
            _slot = transform.GetComponentInParent<Slot>();
        }

        private void Update()
        {
            if (_isClick && !GameManager.Instance._isWorking)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    _secondPos = Input.mousePosition;
                    _result = _secondPos - _firstPos;

                    var direction = Directions.Null;

                    if (Math.Abs(_result.x) > Math.Abs(_result.y))
                    {
                        direction = _result.x > 0f ? Directions.Right : Directions.Left;
                    }
                    else
                    {
                        direction = _result.y > 0f ? Directions.Up : Directions.Down;
                    }
                    _slot.SetCoreDirection(direction);
                    _isClick = false;
                }
            }
        }

        private void OnMouseDown()
        {
            _firstPos = Input.mousePosition;
            _isClick = true;
        }
    }
    
}