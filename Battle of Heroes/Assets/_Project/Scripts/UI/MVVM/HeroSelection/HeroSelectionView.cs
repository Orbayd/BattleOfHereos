using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
        private TMP_Text txtInfo;

        [SerializeField]
        private  GridLayoutGroup _grid;

        [SerializeField]
        private HeroPortraitView _gridItemTemplate;

        protected override void OnBind(HeroSelectionViewModel model)
        {
            _btnPlay.onClick.AddListener(model.OnPlayButtonClicked);
            txtInfo.gameObject.SetActive(false);
            txtInfo.text = "Select three heroes to fight!";
            foreach (var item in model.Itemsource)
            {
                var view = CreateGridItem(_gridItemTemplate,_grid.transform);
                var viewModel = new HeroPortraitViewModel(item);
                view.Bind(viewModel);
            }

            model.PropertyChanged +=  OnNotifyPropertyChanged;

        }

        private void OnNotifyPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals(nameof(DataContex.ShowInfo)))
            {
               txtInfo.gameObject.SetActive(DataContex.ShowInfo);
            }
        }

        protected override void OnUnBind(HeroSelectionViewModel model)
        {
            model.PropertyChanged -=  OnNotifyPropertyChanged;
            _btnPlay.onClick.RemoveAllListeners();
        }

        public HeroPortraitView CreateGridItem(HeroPortraitView prefab, Transform parent)
        {
            return GameObject.Instantiate(prefab,parent).GetComponent<HeroPortraitView>();
        }

    }
}

