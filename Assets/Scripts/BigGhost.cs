using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]  // set to Kinematic
public class BigGhost : MonoBehaviour {
    public GameManager gm;

    [Header("Motion")]
    public float speed = 5.4f;      // constant rightward speed (set a bit below Player runSpeed)
    public bool lockYToGround = true;
    public float fixedY = -6f;      // your ground height if locking Y

    [Header("Kill")]
    public bool useTrigger = true;  // recommended: trigger collider kills on touch
    public float killDistance = 0.6f; // used only if useTrigger = false

    void Awake(){
        // physics setup for reliable trigger
        var rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = true;

        var col = GetComponent<Collider2D>();
        col.isTrigger = useTrigger; // turn on trigger mode if using trigger kill
    }

    void Update(){
        if (gm != null && gm.isGameOver) return;

        // constant, player-independent motion to the RIGHT
        transform.position += Vector3.right * (speed * Time.deltaTime);

        // keep Y steady if desired
        if (lockYToGround)
            transform.position = new Vector3(transform.position.x, fixedY, 0f);

        // Optional distance kill (if you prefer not to use triggers)
        if (!useTrigger && gm != null) {
            var player = gm.player;
            if (player && player.position.x - transform.position.x <= killDistance) {
                gm.GameOver();
                AudioHub.I?.PlaySfx(AudioHub.I.hitSfx, 1f);

            }
        }
    }

    // Trigger kill (recommended)
    void OnTriggerEnter2D(Collider2D other){
        if (!useTrigger || gm == null || gm.isGameOver) return;
        if (other.CompareTag("Player")){
            gm.GameOver();
            AudioHub.I?.PlaySfx(AudioHub.I.hitSfx, 1f);

        }
    }
}
