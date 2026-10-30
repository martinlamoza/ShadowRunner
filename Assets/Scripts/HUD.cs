using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour {
    public GameManager gm;
    public TextMeshProUGUI livesLabel;

    void Update(){
        livesLabel.text = $"Lives: {gm.lives}";
    }
}
