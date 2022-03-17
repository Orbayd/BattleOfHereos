using UnityEngine;
using BattleOfHeroes.Showcase.Enums;
using BattleOfHeroes.Showcase.Helpers;
using System;
using DG.Tweening;

namespace BattleOfHeroes.Showcase.Core
{
    public class Hero : CreatureBase
    {
        public override CreatureType Type => CreatureType.Hero;

        private float _deltaTime;

        public void OnMouseDown()
        {
            _deltaTime = Time.time;
        }

        public void OnMouseUp()
        {
            if (Time.time - _deltaTime < 3f)
            {
                MessageBus.Publish<HeroAttackEvent>(new HeroAttackEvent(this, Type));
            }
            else
            {
                MessageBus.Publish<ShowToolTipEvent>(new ShowToolTipEvent(GetCreatureData() as HeroData, Camera.main.WorldToScreenPoint(transform.position)));
            }
        }


    }
}