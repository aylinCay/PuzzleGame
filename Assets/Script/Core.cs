using UnityEngine;

namespace PuzzleGame
{
    public interface ICore
    {
       CoreColor getColor { get; }
      Transform getTransform { get; set; }
      void Move(Vector3 pos);
    }

    public class Core : MonoBehaviour
    {
        public CoreColor  color;        // Core Color data
        public GameObject uiObject;     // Core Ui reference
        public GameObject get;          // Core Object reference
        public int countOb;
        public int countUi;

        public void CoreSetUp(CoreColor clr = CoreColor.Base, int cn = -1)
        {                
            color   = clr;
            countOb = cn;
            countUi = cn;
            switch (clr)
            {

                case CoreColor.Blue  :
                    uiObject = Resources.Load<GameObject>("BlueCoreUI");
                    get      = Resources.Load<GameObject>("BlueCore");                   
                    break;

                case CoreColor.Green :
                    uiObject = Resources.Load<GameObject>("GreenCoreUI");
                    get      = Resources.Load<GameObject>("GreenCore");                   
                    break;

                case CoreColor.Red   :
                    uiObject = Resources.Load<GameObject>("RedCoreUI");
                    get      = Resources.Load<GameObject>("RedCore");                   
                    break;

                case CoreColor.Yellow:
                    uiObject = Resources.Load<GameObject>("YellowCoreUI");
                    get      = Resources.Load<GameObject>("YellowCore");                    
                    break;

                default              :
                    uiObject = Resources.Load<GameObject>("CoreBaseUI");                   
                    break;
            }

        }
    }
}
