

using System;
using PuzzleGame;
using UnityEngine;

namespace PuzzleGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.PlayerLoop;

    public class Slot : MonoBehaviour, ISlot
    {
        const float UP_POS = -10f;
        const float DOWN_POS = -100f;
        float _moveSlotSpeed = default(float);
        int _slotX = default(int);
        int _slotY = default(int);
        int _moveDirection = default(int);

        Transform _transform;
        Transform _coreSlotPos;
        Vector3 _pos = default(Vector3);

        ICore _core = new CoreBase();

        public int slotX
        {
            get => _slotX;
            set => _slotX = value;
        }

        public int slotY
        {
            get => _slotY;
            set => _slotY = value;
        }

        public CoreColor targetColor { get; set; }

        public Transform getTransform => (_transform == null ? _transform = transform : _transform);

        public Vector3 slotPos
        {
            get
            {
                if (_coreSlotPos == null)
                {
                    _coreSlotPos = getTransform.GetChild(0);

                }

                _pos = _coreSlotPos.position;
                return _pos;
            }
        }

        public ICore getCore => _core;

        public ICore setCore
        {
            set
            {
                if (targetColor != CoreColor.Base)
                {
                    if (targetColor == _core.getColor)
                    {
                        if (value.getColor != targetColor)
                        {
                            GameManager.Instance.matchedCorecount--;
                        }

                    }
                    else if (value.getColor == targetColor)
                    {
                        GameManager.Instance.matchedCorecount++;
                    }
                }

                _core = value;
                MoveDirection(1);
            }
        }

        private void Start()
        {
            _moveSlotSpeed = Random.Range(.25f, .1f);
        }

        private void Update()
        {
            if (_moveDirection > 0) TakeInToGrid();
            else if (_moveDirection < 0) TakeOutFromGrid();
        }

        public void SetCoreDirection(Directions dir)
        {
            var targetX = -1;
            var targetY = -1;
            if (getCore.getColor == CoreColor.Blue || getCore.getColor == CoreColor.Yellow)
            {

                switch (dir)
                {
                    case Directions.Right:
                        if (slotX != Grid.Instance.size - 1)
                        {
                            targetX = slotX + 1;
                            targetY = slotY;
                        }

                        break;

                    case Directions.Left:
                        if (slotX != 0)
                        {
                            targetX = slotX - 1;
                            targetY = slotY;
                        }

                        break;

                    case Directions.Up:
                        if (slotY != Grid.Instance.size - 1)
                        {
                            targetX = slotX;
                            targetY = slotY + 1;
                        }

                        break;

                    case Directions.Down:
                        if (slotY != 0)
                        {
                            targetX = slotX;
                            targetY = slotY - 1;
                        }

                        break;

                    default:
                        break;
                }

                if (targetX != -1 && targetY != -1)
                {
                    if (Grid.Instance.slots[targetX, targetY].getCore.getColor == CoreColor.Base &&
                        getCore.getColor != CoreColor.Base)
                    {
                        Grid.Instance.slots[targetX, targetY].setCore = getCore;
                        GameManager.Instance.SlotInitiator(Grid.Instance.slots[targetX, targetY]);
                        setCore = new CoreBase();
                    }
                }
            }
        }

        public void TakeInToGrid()
        {
            if (getTransform.localPosition.y < UP_POS - .1f)
            {
                getTransform.localPosition = Vector3.Lerp(
                    getTransform.localPosition,
                    new Vector3(
                        getTransform.localPosition.x,
                        UP_POS,
                        getTransform.localPosition.z
                    ),
                    _moveSlotSpeed
                );

                if (_core.getColor != CoreColor.Base)
                    _core.getTransform.localPosition = slotPos;
            }
            else if (getTransform.localPosition.y >= UP_POS - .1f)
            {
                getTransform.localPosition = new Vector3(
                    getTransform.localPosition.x,
                    UP_POS,
                    getTransform.localPosition.z
                );
                MoveDirection(0);
            }

        }

        public void TakeOutFromGrid()
        {
            if (getTransform.localPosition.y > DOWN_POS)
            {
                getTransform.localPosition = Vector3.Lerp(
                    getTransform.localPosition,
                    new Vector3(
                        getTransform.localPosition.x,
                        DOWN_POS,
                        getTransform.localPosition.z
                    ),
                    _moveSlotSpeed * .5f
                );

                if (_core.getColor != CoreColor.Base)
                    getCore.getTransform.position = slotPos;

            }
            else Destroy(this.gameObject);
        }
        public void MoveDirection(int dir = 0)
        {
            _moveDirection = dir;
        }
    }
}


public interface ISlot
    {
        CoreColor targetColor { get;}
        int slotX { get; set; }
        int slotY { get; set; }
        ICore getCore { get; }
        ICore setCore { set; }
        Vector3 slotPos { get;}
        void TakeInToGrid();
        void TakeOutFromGrid();
        void MoveDirection(int direction = 0);
    }

