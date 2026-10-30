using UnityEngine;

public class FinishLine : MonoBehaviour {
    public GameManager gm;

    void OnTriggerEnter2D(Collider2D other){
        if (gm && !gm.isGameOver && other.CompareTag("Player")){
            gm.Win();
        }
    }
}
