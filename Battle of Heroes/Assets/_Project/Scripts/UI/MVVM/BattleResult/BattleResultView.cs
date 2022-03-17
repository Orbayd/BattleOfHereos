using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleOfHeroes.Showcase.UI
{
    public class BattleResultView : ViewBase<BattleResultViewModel>
    {
        [SerializeField]
        private Button _btnReturn;

        [SerializeField]
        private TMP_Text _txtInfo;
        protected override void OnBind(BattleResultViewModel model)
        {
            model.PropertyChanged += OnNotifyPropertyChanged;
            _btnReturn.onClick.AddListener(DataContex.OnReturnButtonClicked);
        }

        protected override void OnUnBind(BattleResultViewModel model)
        {
            model.PropertyChanged -= OnNotifyPropertyChanged;
            _btnReturn.onClick.RemoveAllListeners();
        }

        private void OnNotifyPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals(nameof(DataContex.IsBattleLost)))
            {
                _txtInfo.text = DataContex.IsBattleLost ? "You Lost!" : "You Win!";
            }
        }
    }
}