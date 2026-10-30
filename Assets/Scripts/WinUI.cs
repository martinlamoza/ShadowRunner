using UnityEngine;
using TMPro;

public class WinUI : MonoBehaviour {
    public GameManager gm;
    public GameObject endPanel;           
    public TextMeshProUGUI endTitle;      
    public TextMeshProUGUI endSubtitle;  
    public TextMeshProUGUI endReason;     

    bool shown;

    void Update(){
        if (!gm || shown || !gm.isGameOver) return;

        shown = true;
        endPanel.SetActive(true);

        if (gm.won){
            if (endTitle)   endTitle.text   = "YOU WIN!";
            if (endReason)  endReason.text  = "You reached the finish line.";
        } else {
            if (endTitle)   endTitle.text   = "GAME OVER";
            if (endReason)  endReason.text  = "You ran out of lives.";
        }
        if (endSubtitle) endSubtitle.text = "Press R to Restart";
    }
}
