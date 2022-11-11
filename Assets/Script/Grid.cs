using System;

namespace PuzzleGame
{
    using System.Collections;
    using System.Collections.Generic;
    using PuzzleGame;
    using UnityEngine;

    public class Grid : Singleton<Grid>
    {
        private bool _isBuild = false;
        private int _size = 4;

        GameObject _slotPrefab;
        RectTransform _uiPanelRect;
        private List<GameObject> uiObjects = new List<GameObject>();
        private List<GameObject> slotObjects = new List<GameObject>();

        private CoreStroge _stroge = new CoreStroge();
        private Slot[,] _slots;


        public bool isBuild
        {
            get => _isBuild;
            set
            {
                _isBuild = value;
                if (!_isBuild)
                    DestroyTheGrid();
            }
        }

        public int size => _size;
        public Slot[,] slots => _slots;

        private void Start()
        {
            if (Instance != this) Destroy(this.gameObject);
        }

        void BuildTheGrid()
        {
            var pos = (_size - 1) * .5f;
            var uiGridSize = _size * 30f;
            var uiPos = uiGridSize * .5f;

            int x, y = 0;

            for (y = 0; y < _size; y++)
            {
                for (x = 0; x < _size; x++)
                {
                    var origin = _stroge.GetCoreRepo();

                    var slotInstance = Instantiate(_slotPrefab, new Vector3(-pos + x, -100f, (-pos - (size * .5f)) + y),
                        Quaternion.identity);

                    slotInstance.name = "Slot Y " + y.ToString() + " X " + x.ToString();
                    slotInstance.transform.SetParent(transform);

                    _slots[x, y] = slotInstance.GetComponent<Slot>();
                    _slots[x, y].slotX = x;
                    _slots[x, y].slotY = y;
                    _slots[x, y].targetColor = origin.color;
                    slotObjects.Add(slotInstance);

                    var uiInstance = Instantiate(origin.uiObject, _uiPanelRect);
                    uiInstance.name = "Slot " + y.ToString() + x.ToString();
                    uiInstance.GetComponent<RectTransform>().localPosition =
                        new Vector3(-uiPos + 15f + (x * 30f), (-uiPos * 2f) + (y * 30f));
                    uiObjects.Add(uiInstance);
                }
            }

            foreach (var slotItem in _slots)
            {
                if (slotItem.targetColor == CoreColor.Red)
                {
                    var ob = Resources.Load<GameObject>("RedCore");
                    slotItem.setCore = Instantiate<GameObject>(ob, slotItem.slotPos, Quaternion.identity)
                        .GetComponent<ICore>();
                }
                else if (slotItem.targetColor == CoreColor.Green)
                {
                    var ob = Resources.Load<GameObject>("GreenCore");
                    slotItem.setCore = Instantiate<GameObject>(ob, slotItem.slotPos, Quaternion.identity)
                        .GetComponent<ICore>();
                }
                else
                {
                    slotItem.setCore = _stroge.GetCoreObj(slotItem.slotPos);
                }
            }

            _isBuild = true;
        }

        public void ClearTheLevel()
        {
            foreach (var uiOb in uiObjects)
            {
                Destroy(uiOb);
            }

            foreach (var slotObj in slotObjects)
            {
                Destroy(slotObj);
            }
        }

        public void CreateMap(int size = 2)
        {
            if (size > 10) size = 10;
            _size = size;
            _slots = new Slot[_size, size];

            _stroge = new CoreStroge();
            _stroge.CoreStorageSetUp(_size);

            _slotPrefab = Resources.Load<GameObject>("Slot");
            _uiPanelRect = GameObject.FindGameObjectWithTag("UIPanel").GetComponent<RectTransform>();

            var panelHeight = (_size + 3) * 30f;

            if (_uiPanelRect.rect.height < panelHeight)
                _uiPanelRect.sizeDelta = new Vector2(_uiPanelRect.sizeDelta.x, panelHeight);

            Camera.main.fieldOfView = 55f + (_size * 2.5f);
            ClearTheLevel();
            BuildTheGrid();
        }

        void DestroyTheGrid()
        {
            foreach (var slt in slots)
            {
                slt.MoveDirection(-1);
            }
        }
    }
}