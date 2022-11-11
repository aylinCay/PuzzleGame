using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleGame
{
    public enum CoreColor
    {
        Base = 0,
        Blue = 1,
        Red = 2,
        Yellow = 3,
        Green = 4
    };

    public enum Directions
    {
        Null = 0,
        Right = 1,
        Left = 2,
        Up = 3,
        Down = 4
    }

    public class CoreStroge : MonoBehaviour
    {
        private int _blueCount, _greenCount, _redCount, _yellowCount, _baseCount, _size;
        [SerializeField] private List<Core> _cores = new List<Core>();
        public List<Core> stock = new List<Core>();
        [SerializeField] private List<GameObject> _objects = new List<GameObject>();
        public List<GameObject> objectUi = new List<GameObject>();
        public bool isSetUp;

        public ICore GetCoreObj(Vector3 pos)
        {
            objectUi.TrimExcess();
            var index = Random.Range(0, objectUi.Count);
            ICore result = new CoreBase();
            if (objectUi[index] != null)
            {
                result = Instantiate<GameObject>(objectUi[index], pos, Quaternion.identity).GetComponent<ICore>();
            }
            objectUi.RemoveAt(index);

            return result;
        }

        public Core GetCoreRepo()
        {
            var index = Random.Range(0, stock.Count);
            var result = stock[index];
            if (result.color != CoreColor.Red && result.color != CoreColor.Green)
            {
                objectUi.Add(result.get);
            }
            stock.RemoveAt(index);
            stock.TrimExcess();
            return result;
        }

        public void CoreStorageSetUp(int Size)
        {
            var size = (Size * Size);
            _size = size;
            _blueCount = (int)(size * .25f);
            _cores.Add(new Core());
            _cores[0].CoreSetUp(CoreColor.Blue,_blueCount);

            while (_cores[0].countOb > 0)
            {
                stock.Add(_cores[0]);

                _cores[0].countOb--; 
            }

            _greenCount = (int)(size * .0625f);
            _cores.Add(new Core());
            _cores[1].CoreSetUp(CoreColor.Green,_greenCount);

            while (_cores[1].countOb > 0)
            {
                stock.Add(_cores[1]);
                _cores[1].countOb--;
            }

            _redCount = (int)(size * .0625f);
            _cores.Add(new Core());
            _cores[2].CoreSetUp(CoreColor.Red,_redCount);

            while (_cores[2].countOb > 0)
            {
                stock.Add(_cores[2]);
                _cores[2].countOb--;
            }

            _yellowCount = (int)(size * .3125f);
            _cores.Add(new Core());
            _cores[3].CoreSetUp(CoreColor.Yellow,_yellowCount);

            while (_cores[3].countOb > 0)
            {
                stock.Add(_cores[3]);
                _cores[3].countOb--;
                
            }

            _baseCount = size - (_blueCount + _greenCount + _redCount + _yellowCount);
            _cores.Add(new Core());
            _cores[4].CoreSetUp(CoreColor.Base,_baseCount);

            while (_cores[4].countOb > 0)
            {
                stock.Add(_cores[4]);
                _cores[4].countOb--;

            }

            GameManager.Instance.matchChecker = _blueCount + _greenCount + _redCount + _yellowCount;
            isSetUp = true;

        }
    }

}
