using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour {
    public SpriteRenderer visualSR;         // assign the SpriteRenderer on Visual
    public float flashTime = 0.25f;
    public Color flashColor = new Color(1f, 0.2f, 0.2f);  // red flash


    Color _origColor;

    void Awake(){
        if (visualSR) _origColor = visualSR.color;
    }

    public void Flash(){
        if (visualSR) StartCoroutine(FlashCo());
    }

    IEnumerator FlashCo(){
        if (!visualSR) yield break;
        // quick white flash
        visualSR.color = flashColor;
        yield return new WaitForSeconds(flashTime);
        visualSR.color = _origColor;
    }
}
