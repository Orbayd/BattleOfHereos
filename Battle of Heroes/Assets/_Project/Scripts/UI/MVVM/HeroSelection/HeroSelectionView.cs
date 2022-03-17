using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BattleOfHeroes.Showcase.UI
{
    public class HeroSelectionView : ViewBase<HeroSelectionViewModel>
    {
        [SerializeField]
        private Button _btnPlay;

        [SerializeField]
        private  GridLayoutGroup _grid;

        [SerializeField]
        private HeroPortraitView _gridItemTemplate;

        protected override void OnBind(HeroSelectionViewModel model)
        {
            _btnPlay.onClick.AddListener(model.OnPlayButtonClicked);

            foreach (var item in model.Itemsource)
            {
                var view = CreateGridItem(_gridItemTemplate,_grid.transform);
                var viewModel = new HeroPortraitViewModel(item);
                view.Bind(viewModel);
            }

        }

        protected override void OnUnBind(HeroSelectionViewModel model)
        {
            _btnPlay.onClick.RemoveAllListeners();
        }

        public HeroPortraitView CreateGridItem(HeroPortraitView prefab, Transform parent)
        {
            return GameObject.Instantiate(prefab,parent).GetComponent<HeroPortraitView>();
        }

    }
}

