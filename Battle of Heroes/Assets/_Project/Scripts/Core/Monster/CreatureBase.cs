using BattleOfHeroes.Showcase.Enums;
using BattleOfHeroes.Showcase.Helpers;
using UnityEngine;
namespace BattleOfHeroes.Showcase.Core
{
    public abstract class CreatureBase : MonoBehaviour, ICreature
    {
        public virtual CreatureType Type { get; protected set; }
        private CreatureAnimHandler _animHandler { get; set; }
        private HeroDbo _dbo;
        private HealthBarUI _healthbar;
        private float _health;
        private float _maxHealth;
        private float _attackPower;

        public void Init(CreatureAnimHandler animHandler, HeroDbo dbo)
        {
            _animHandler = animHandler;
            _dbo = dbo;
            _attackPower = _dbo.HeroData.AttackPower + ((_dbo.Level - 1) * _dbo.HeroData.PowerUpPerLevel) * _dbo.HeroData.AttackPower;
            _maxHealth = _dbo.HeroData.Health + ((_dbo.Level - 1) * _dbo.HeroData.PowerUpPerLevel) * _dbo.HeroData.Health;
            _health = _maxHealth;
            //GetComponent<SpriteRenderer>().sprite = dbo.HeroData.Sprite;
        }
        public void Attack(ICreature target, Vector2 pos)
        {
            _animHandler.AttackAnim(target, pos);
        }

        public virtual void OnAttackStarted(ICreature target)
        {
            _healthbar.gameObject.SetActive(false);
        }

        public virtual void OnAttackLanded(ICreature target)
        {
            target.TakeDamage(_attackPower);
        }

        public virtual void OnAttackEnded(ICreature target)
        {
            _healthbar.gameObject.SetActive(true);
        }

        public void Die()
        {
            gameObject.SetActive(false);
            Destroy(_healthbar.gameObject);
        }

        public void TakeDamage(float dmg)
        {
            _animHandler.TakeDamageAnim(dmg);
        }

        public virtual void OnDamageTaken(float dmg)
        {
            _health -= dmg;
            bool isAlive = true;
            if (_health <= 0)
            {
                Debug.Log($"[Info]{_dbo.HeroData.Name} Is Dead");
                isAlive = false;
                Die();
            }
            else
            {
                _healthbar.SetDamage(dmg);
                SetHealthBar();
            }
            MessageBus.Publish<DamageTaken>(new DamageTaken(this, Type, isAlive));
        }

        public void InitBillboard(HealthBarUI healthBar)
        {
            _healthbar = healthBar;
            _healthbar.gameObject.SetActive(true);
            _healthbar.SetTarget(transform);
            SetHealthBar();
        }

        public void SetHealthBar()
        {
            _healthbar.SetHealth(_health / _maxHealth);
            _healthbar.SetText($"{_health} / {_maxHealth}");
        }

        public HeroDbo GetCreatureData()
        {
            return _dbo;
        }

    }
}