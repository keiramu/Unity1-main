using UnityEngine;
using TMPro;
using UnityEngine.Shaders;

public class AmmoTextControl : MonoBehaviour
{
    public TMP_Text ammoText;

    public void UpdateText(string gunName, int currentAmmo, int MaxAmmo)
    {
        string text = $"{gunName} {currentAmmo}/{MaxAmmo}";
        ammoText.text = text;
        
    }
}
