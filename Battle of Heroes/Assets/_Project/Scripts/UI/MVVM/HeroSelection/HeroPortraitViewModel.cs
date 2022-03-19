using System;
using System.Collections;
using System.Collections.Generic;
using BattleOfHeroes.Showcase.Helpers;
using UnityEngine;

namespace BattleOfHeroes.Showcase.UI
{
    public class HeroPortraitViewModel : ViewModelBase
    {
        public HeroDbo Data {get; private set;}
        private bool _isSelected = false;
        public bool IsSelected { get {return _isSelected;} set{ _isSelected = value; NotifyPropertyChanged();}}
        public bool _isInteractible = false;
        public bool IsInteractible {get {return Data.IsAvailable;} set{_isInteractible = value; NotifyPropertyChanged();}}


        public HeroPortraitViewModel(HeroDbo data)
        {
            Data = data;
        }

        public void OnItemSelected(float deltaTime, Vector2 position)
        {
            if(deltaTime < 1.0f)
            {
                Debug.Log($"[INFO] Hero Selected{Data.HeroData.Name}");
                PublishSelectHero();
            }
            else
            {
                Debug.Log($"[INFO] Tooltip Shown {Data.HeroData.Name}");
                PublishShowTooltip(position);
            }
        }
        private void PublishSelectHero()
        {
            MessageBus.Publish(new HeroSelectedEvent(Data,this));
        }

        private void PublishShowTooltip(Vector2 positon)
        {
            MessageBus.Publish(new ShowToolTipEvent(Data,positon));
        }
    }
}
