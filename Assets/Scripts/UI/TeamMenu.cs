using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public enum TeamMenuMode
    {
        View, // view the stats of the team
        Select, // select a creature to use an object, switch...
        BattleSwitch, // use a new creature for battle
        BattleItem // use an item on a creature (uses a turn)
    }
    
    public class TeamMenu : UpDownMenu
    {
        // Need to store info about the selected creature
        [NonSerialized] public new List<TeamMenuMonTracker> choices = new();
        private List<GameObject> _choicesObjects = new();

        private GameObject _baseMonTrackerObject;

        private int _firstDisplayedIndex;

        [NonSerialized] protected new int Step = 110; // TODO magic vars

        [NonSerialized] public TeamMenuMode Mode = TeamMenuMode.View;

        private new void Awake()
        {
            base.Awake();
            _baseMonTrackerObject = GameObject.Find("TeamMenuItems/MonTracker");
            _baseMonTrackerObject.gameObject.SetActive(false);
        }
        
        public void Redraw()
        {
            if (choices.Count > 0) choices.Clear();

            // Destroy existing tracker objects
            foreach (var choice in _choicesObjects)
            {
                Destroy(choice);
            }
            _choicesObjects.Clear();
            
            var items = transform.Find("TeamMenuItems");
            
            // Initialize trackers
            for (var i = _firstDisplayedIndex;
                 i < _firstDisplayedIndex + 6 && i < GameInformation.Squad.Monsters.Count;
                 i++)
            {
                var newTrackerObject = Instantiate(_baseMonTrackerObject, items);
                newTrackerObject.SetActive(true);
                _choicesObjects.Add(newTrackerObject);
                
                var newTracker = newTrackerObject.GetComponent<TeamMenuMonTracker>();
                newTracker.TrackedCreature = GameInformation.Squad.Monsters[i];
                choices.Add(newTracker);
            }
            
            // Refresh display AFTER initializing!
            foreach (var tracker in choices)
            {
                tracker.Redraw();
            }

        }

        public int GetMonPosition()
        // Return the index of the mon to use in Select functions
        {
            return _firstDisplayedIndex + ArrowPosition;
        }

        public Creature GetMonUnderCursor()
        {
            return GameInformation.Squad.Monsters[GetMonPosition()];
        }

        public void UpdateMonUnderCursor(Creature newMon)
        // Replace an existing mon in the collection with new data (healing etc.)
        {
            GameInformation.Squad.Monsters[GetMonPosition()] = newMon;
        }
        
        public new void Navigate(int direction) // TODO very ugly
        {
            if (direction == -1 & ArrowPosition <= 0)
            {
                if (_firstDisplayedIndex == 0) return;
                _firstDisplayedIndex--;
                Redraw();
                return;
            }

            if (direction == 1 & ArrowPosition >= choices.Count) return;

            if (direction == 1 & ArrowPosition >= 5)
                // at this point there are more than 6 creatures in the inventory
            {
                if (_firstDisplayedIndex > choices.Count - 6) return;
                _firstDisplayedIndex++;
                Redraw();
                return;
            }
            
            ArrowPosition += direction;
            ArrowRt.anchoredPosition = new Vector2(InitialArrowPosition.x,
                InitialArrowPosition.y - Step * ArrowPosition);
        }
    }
}
