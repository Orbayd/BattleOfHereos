using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BattleOfHeroes.Showcase.Enums;
using BattleOfHeroes.Showcase.Helpers;
using BattleOfHeroes.Showcase.UI;
using UnityEngine;

namespace BattleOfHeroes.Showcase.Managers
{
    public class UIManager : MonoBehaviour
    {
        private List<IView> _views = new List<IView>();

        [SerializeField]
        private Transform _viewHolder;

        [SerializeField]
        private UIConfig _uiConfig;

        [SerializeField]
        public ViewName _starter;

        [SerializeField]
        public bool NavigateOnStart = false;

        private Dictionary<ViewName, IView> _viewMap = new Dictionary<ViewName, IView>();

        [Header("ToolTip")]
        [SerializeField]
        private HeroTooltipView _tooltipView;

        public void Init()
        {
            if (!_views.Any())
            {
                _views = _viewHolder.GetComponentsInChildren<IView>(true).ToList();
            }
            foreach (var view in _views)
            {
                Bind(view);
            }

            AddEvents();

            if (NavigateOnStart)
                Navigate(_starter);
        }

        public void Terminate()
        {
            foreach (var view in _views)
            {
                   
            }

            RemoveEvents();
        }

        private void Bind(IView view)
        {
            if (view is HeroSelectionView v)
            {
                //var model = new HeroSelectionViewModel();
                v.Bind(_uiConfig.CreateViewModel(ViewName.HeroSelection) as HeroSelectionViewModel);
                _viewMap.Add(ViewName.HeroSelection, view);
            }
            else if (view is BattleResultView battleResultView)
            {
                var model = new BattleResultViewModel();
                battleResultView.Bind(model);
                _viewMap.Add(ViewName.BattleResult, view); 
            }
        }

        public void Navigate(ViewName name)
        {
            foreach (var view in _viewMap)
            {
                view.Value.Show(false);
            }
            _viewMap[name].Show(true);
        }

        private void OnShowTooltip(ShowToolTipEvent x)
        {
            _tooltipView.Bind(new HeroTooltipViewModel(x.Data));
            _tooltipView.transform.position = x.Position;
            _tooltipView.Show(true);
        }

        public void AddEvents()
        {
            MessageBus.Subscribe<ShowToolTipEvent>(x => OnShowTooltip(x));
        }

        public void RemoveEvents()
        {
            MessageBus.UnSubscribe<ShowToolTipEvent>();
        }


    }

}
