using UnityEngine;
using System.Collections;
using TMPro;

public class AnimateText : MonoBehaviour {
    private TMP_Text m_TextComponent;
    private TMP_TextInfo textInfo;

    private bool animateText;
    private int visibleCount;
    private int totalVisibleCharacters;

    private void Awake() {
        m_TextComponent = gameObject.GetComponent<TMP_Text>();
        textInfo = m_TextComponent.textInfo;
    }

    void OnEnable() {
        // TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
        animateText = true;
        m_TextComponent.ForceMeshUpdate();
        StartCoroutine(RevealCharacters());
    }

    void OnDisable() {
        // TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
        animateText = false;
    }

    public void SetText(string text) {
        m_TextComponent.text = text;
        totalVisibleCharacters = text.Length;
        visibleCount = 0;
    }


    /// <summary>
    /// Method revealing the text one character at a time.
    /// </summary>
    /// <returns></returns>
    IEnumerator RevealCharacters() {
        totalVisibleCharacters = textInfo.characterCount;
        visibleCount = 0;
        float waitTime = 0;

        while (animateText) {
            if (visibleCount > totalVisibleCharacters) {
                waitTime += 0.5f;
                yield return new WaitForSeconds(0.5f);
                if (waitTime >= 2f) {
                    waitTime = 0;
                    visibleCount = 0;
                }
            }

            // How many characters should TextMeshPro display?
            m_TextComponent.maxVisibleCharacters = visibleCount;
            visibleCount += 1;

            yield return new WaitForSeconds(0.03f);
        }
    }

}