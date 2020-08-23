using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
}
