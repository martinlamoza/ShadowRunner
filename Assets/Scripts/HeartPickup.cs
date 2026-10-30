using UnityEngine;

public class HeartPickup : MonoBehaviour {
    public GameManager gm;
    public int amount = 1;     
    public int maxLives = 6;   

    void OnTriggerEnter2D(Collider2D other){
        if (!gm || gm.isGameOver) return;
        if (other.CompareTag("Player")){
            gm.lives = Mathf.Min(gm.lives + amount, maxLives);

            AudioHub.I?.PlaySfx(AudioHub.I.heartSfx, 0.8f);



            Destroy(gameObject);
        }
    }
}
