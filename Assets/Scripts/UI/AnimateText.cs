using UnityEngine;
using System.Collections;
using TMPro;

public class AnimateText : MonoBehaviour {
    private TMP_Text m_TextComponent;
    private bool hasTextChanged;

    void Awake() {
        m_TextComponent = gameObject.GetComponent<TMP_Text>();
    }


    void Start() {
        StartCoroutine(RevealCharacters(m_TextComponent));
    }


    void OnEnable() {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
    }

    void OnDisable() {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
    }


    // Event received when the text object has changed.
    void ON_TEXT_CHANGED(Object obj) {
        hasTextChanged = true;
    }


    /// <summary>
    /// Method revealing the text one character at a time.
    /// </summary>
    /// <returns></returns>
    IEnumerator RevealCharacters(TMP_Text textComponent) {
        textComponent.ForceMeshUpdate();

        TMP_TextInfo textInfo = textComponent.textInfo;

        int totalVisibleCharacters = textInfo.characterCount; // Get # of Visible Character in text object
        int visibleCount = 0;

        while (true) {
            if (hasTextChanged) {
                totalVisibleCharacters = textInfo.characterCount; // Update visible character count.
                hasTextChanged = false;
            }

            if (visibleCount > totalVisibleCharacters) {
                yield return new WaitForSeconds(2f);
                visibleCount = 0;
            }

            // How many characters should TextMeshPro display?
            textComponent.maxVisibleCharacters = visibleCount;
            visibleCount += 1;

            yield return new WaitForSeconds(0.03f);
        }
    }

}