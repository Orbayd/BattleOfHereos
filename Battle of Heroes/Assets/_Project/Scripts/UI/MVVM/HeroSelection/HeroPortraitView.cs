using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BattleOfHeroes.Showcase.UI
{
    public class HeroPortraitView : ViewBase<HeroPortraitViewModel>, IPointerDownHandler , IPointerUpHandler
    {
        [SerializeField]
        private TMP_Text _txtName;

        [SerializeField]
        private Image _imgPortrait;

        [SerializeField]
        private Outline _outLine;

        private float _deltaTime;

        protected override void OnBind(HeroPortraitViewModel model)
        {
            _txtName.text = model.Data.Name;
            _outLine.enabled = false;

            model.PropertyChanged += OnNotifyPropertyChanged;
        }

        protected override void OnUnBind(HeroPortraitViewModel model)
        {
            model.PropertyChanged -= OnNotifyPropertyChanged;
        }

        private void OnNotifyPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals(nameof(DataContex.IsSelected)))
            {
                _outLine.enabled = DataContex.IsSelected;
            }
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            _deltaTime  = Time.time;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //Debug.Log("[INFO] Pointer Event" + _txtName.text);
            DataContex.OnItemSelected(Time.time - _deltaTime,this.GetComponent<RectTransform>().position);
        }
    }
}