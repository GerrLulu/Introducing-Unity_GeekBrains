using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] Slider sliderSoundVolume;

        private void Start()
        {
            sliderSoundVolume.onValueChanged.AddListener((float value) => { Debug.Log(value); });
        }
    }
}