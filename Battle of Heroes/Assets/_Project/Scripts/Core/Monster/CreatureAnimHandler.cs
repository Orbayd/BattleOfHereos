using BattleOfHeroes.Showcase.Enums;
using BattleOfHeroes.Showcase.Helpers;
using DG.Tweening;
using UnityEngine;
namespace BattleOfHeroes.Showcase.Core
{
    public class CreatureAnimHandler
    {
        private CreatureBase _creature;
  
        public CreatureAnimHandler(CreatureBase creature)
        {
            _creature = creature;
        }

        public void AttackAnim(ICreature target, Vector2 pos)
        {   
            _creature.OnAttackStarted(target);
            Vector2 inital = _creature.transform.position;
            _creature.transform.DOMove(pos, 1).OnComplete(() => { _creature.OnAttackLanded(target); //Ping
            _creature.transform.DOMove(inital, 1).OnComplete(()=>{_creature.OnAttackEnded(target);});});               //Pong
        }

        public void TakeDamageAnim(float dmg)
        {          
            _creature.transform.DOShakePosition(1.0f, strength: new Vector3(0, 0.1f, 0), vibrato: 5, randomness: 1, snapping: false, fadeOut: true)
                      .OnComplete(() =>
                      {
                          _creature.OnDamageTaken(dmg);
                      });
        }
    }
}