using UnityEngine;

public class ImpEnemy : MonoBehaviour {
    public float speed = 4f;
    public GameManager gm;
    public float killXOffset = 30f;

    void Update(){
        if (gm == null || gm.isGameOver) return;
        float bob = Mathf.Sin(Time.time * 3f) * 0.1f;
        transform.position += (Vector3.left * speed + Vector3.up * bob) * Time.deltaTime;
    }

    void Start(){
    
    if (Random.value < 0.4f) AudioHub.I?.PlayRandomGhost();


    }


    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log($"Imp trigger with: {other.name} tag={other.tag}");
        if (gm != null && !gm.isGameOver && other.CompareTag("Player")) {
            Debug.Log("HitByImp() called");
            gm.HitByImp();
            Destroy(gameObject);
        }
    }



}
