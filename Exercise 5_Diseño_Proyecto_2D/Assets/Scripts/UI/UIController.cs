using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Fields
    [SerializeField] private Wings _wings;

    [SerializeField] private Slider _energySlider;
    [SerializeField] private TextMeshProUGUI _textSlider;
    #endregion

    #region Unity Callbacks
    void Update()
    {
        _energySlider.value = _wings.Energy;
        _textSlider.text = ((int)_wings.transform.position.y).ToString();
    }


    #endregion
}
