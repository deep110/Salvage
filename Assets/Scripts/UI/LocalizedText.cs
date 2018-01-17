using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour {

    public string key;

    void Start () {
    	if (LocalizationManager.Instance != null) {
	        Text text = GetComponent<Text>();
	        text.text = LocalizationManager.Instance.GetLocalizedValue(key);
    	}
    }

}