using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectPetrmon
{
    public class FightButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private MoveInfoPanel _moveInfoPanel;

        private Move _move;
        private Petrmon _targetPetrmon;
        private Button _fightButton;
        private TextMeshProUGUI _buttonText;
        private Action _moveExecuteEvent;

        private void Awake()
        {
            _buttonText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _fightButton = GetComponent<Button>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _moveInfoPanel.UpdateMoveInfoPanel(_move);
            _moveInfoPanel.gameObject.SetActive(true);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            _moveInfoPanel.gameObject.SetActive(false);
        }

        public void UpdateFightButton(Move move, Petrmon targetPetrmon, Action updateOpponentPetrPanel)
        {
            _move = move;
            _targetPetrmon = targetPetrmon;
            _moveExecuteEvent = updateOpponentPetrPanel;

            UpdateDisplay();
            ButtonSetup();
        }

        private void UpdateDisplay()
        {
            _buttonText.text = _move.MoveName;
        }

        private void ButtonSetup()
        {
            _fightButton.onClick.RemoveAllListeners();
            _fightButton.onClick.AddListener(() =>
            {
                _move.Execute(_targetPetrmon);
                _moveExecuteEvent?.Invoke();
                _moveInfoPanel.UpdateMoveInfoPanel(_move);
            });
        }
    }
}
