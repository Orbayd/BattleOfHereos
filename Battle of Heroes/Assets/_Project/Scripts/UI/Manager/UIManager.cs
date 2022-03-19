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
    public class UIManager : IService
    {
        private List<GameObject> _views = new List<GameObject>();
        private Dictionary<ViewName, IView> _viewMap = new Dictionary<ViewName, IView>();

        private UserDbo _userDbo;

        private HeroTooltipView _tooltipView;

        private UIConfig _uIConfig;

        public UIManager(UIConfig uIConfig,UserDbo userDbo)
        { 
            _userDbo = userDbo;
            _uIConfig = uIConfig;
        }

        public void Init()
        {
            AddEvents();
        }

        public void Terminate()
        {
            RemoveEvents();
        }

        public void TerminatePresentation()
        {
            foreach (var view in _viewMap)
            {
                view.Value.UnBind();
            }
            _viewMap.Clear();

            foreach (var viewGo in _views)
            {
                MonoBehaviour.Destroy(viewGo);
            }

            _views.Clear();

            // if(!(_tooltipView?.gameObject is null))
            // {
            //     MonoBehaviour.Destroy(_tooltipView.gameObject);
            // }
            _tooltipView = null;
        }

        public void InitPresentation(string name)
        {
            var scene =_uIConfig.SceneViews.First(x=>x.SceneName == name);
            var canvas  =  MonoBehaviour.FindObjectOfType<Canvas>();
            foreach (var view in scene.Views)
            {
                var viewGo = GameObject.Instantiate(view.Prefab,canvas.transform);
                Bind(viewGo.GetComponent<IView>());
                _views.Add(viewGo);
            }

            if(_tooltipView is null)
            {
                var tooltip = _uIConfig.Commons.First(x=> x.Name == ViewName.Tooltip);
                var tooltipGo = GameObject.Instantiate(tooltip.Prefab,canvas.transform);
                _tooltipView = tooltipGo.GetComponent<HeroTooltipView>();
                _tooltipView.gameObject.SetActive(false);
            }
        }

        private void Bind(IView view)
        {
            if (view is HeroSelectionView heroSelectionView)
            {
                heroSelectionView.Bind(new HeroSelectionViewModel(_userDbo));
                _viewMap.Add(ViewName.HeroSelection, view);
            }
            else if (view is BattleResultView battleResultView)
            {

                battleResultView.Bind(new BattleResultViewModel());
                _viewMap.Add(ViewName.BattleResult, view); 
            }
            else if(view is StartView startView)
            {
                startView.Bind(new StartViewModel());
                _viewMap.Add(ViewName.Start,view);
            }
            else if(view is BattleView battleView)
            {
                battleView.Bind(new BattleViewModel(_userDbo));
                _viewMap.Add(ViewName.Battle,view);
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

        private void OnShowTooltip(ShowToolTipEvent e)
        {
            _tooltipView.Bind(new HeroTooltipViewModel(e.Data));
            _tooltipView.transform.position = e.Position;
            _tooltipView.Show(true);
        }

        public void AddEvents()
        {
            MessageBus.Subscribe<ShowToolTipEvent>(e => OnShowTooltip(e));
        }

        public void RemoveEvents()
        {
            MessageBus.UnSubscribe<ShowToolTipEvent>();
        }


    }

}
