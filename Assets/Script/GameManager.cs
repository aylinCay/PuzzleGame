using System;
using UnityEngine.Serialization;

namespace PuzzleGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    public class GameManager:Singleton<GameManager>
    {
       
       
        public GameObject levelPanel;
        public GameObject menupanel;
        public GameObject gameUI;
        public GameObject successPanel;
        public GameObject failPanel;
        
        
        float       _gameTime;
        private int _level = 1;
        public int matchChecker;
         public int matchedCorecount;
        public bool _isWorking;
        public Text _levelText;
        public Text _secText;
         private Slot _slot = new Slot();

         // Start is called before the first frame update
        
         
         void Start()
        {
            Application.targetFrameRate = 60;
            if(Instance != this) Destroy(this.gameObject);
            
            if (PlayerPrefs.HasKey("PuzzleLevel")) _level = PlayerPrefs.GetInt("PuzzleLevel");

            UiIndicator(0);
        }
         
        public void SlotInitiator(Slot slot)
        {
            if (!_isWorking)
            {
                _slot = slot;
                _isWorking = true;
                
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q)) Grid.Instance.isBuild = false;
            
            if (Grid.Instance.isBuild)
            {
                if (_slot.getCore.getColor != CoreColor.Base)
                {
                    if (Vector3.Distance(_slot.getCore.getTransform.position, _slot.slotPos) > .15f)
                    {
                        _slot.getCore.Move(_slot.slotPos);
                    }
                    else if (_slot.getTransform.position != _slot.slotPos)
                    {
                        _slot.getCore.getTransform.position = _slot.slotPos;
                        _isWorking = false;
                        _slot = new Slot();
                    }
                }

                if (matchChecker == matchedCorecount)
                {
                    UiIndicator(2);
                    _level++;
                    SaveLevel();
                    Grid.Instance.isBuild = false;
                }
                else
                {
                    if (_gameTime > 0f)
                    {
                        _gameTime -= Time.deltaTime;
                    }
                    else
                    {
                        _gameTime = 0;
                        UiIndicator(3);
                        Grid.Instance.isBuild = false;
                    }

                    _secText.text = _gameTime.ToString("000") + "sec.";
                }
            }
        }
        
        public void PlayButton()
        {
            UiIndicator(1);
            BuildLevelTime(_level);
            matchChecker = 0;
            matchedCorecount = 0;
            _levelText.text = "Level " + _level.ToString();
            Grid.Instance.CreateMap((3 + _level));
            Debug.Log("Çalıştı");
        }
        
        void SaveLevel() => PlayerPrefs.SetInt("PuzzleLevel", _level);
        void BuildLevelTime(int lvl) => _gameTime = (3 + _level) * 20f;

        void UiIndicator(int panelId)
        {
            if (panelId == 0) menupanel.SetActive(true);
                else menupanel.SetActive(false);

            if (panelId == 1) levelPanel.SetActive(true);
            else levelPanel.SetActive(false);

            if(panelId == 1){gameUI.SetActive(true);}
            else gameUI.SetActive(false);
            
            if(panelId == 2){successPanel.SetActive(true);}
            else successPanel.SetActive(false);

            if (panelId == 3){failPanel.SetActive(true);}
            else failPanel.SetActive(false);
            
        }
        
    }

}