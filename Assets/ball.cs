using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ball : MonoBehaviour {
    public float speed = 30;
    public Text p1points;
    public Text p2points;

    public Text mensagemsuser;


    private int countp1;
    private int countp2;

    private int countround;

    void Start() {
        countp1 =0 ;
        countp2 =0 ;
        countround=0;
        mensagemsuser.gameObject.SetActive(false);
        StartCoroutine (ExecuteAfterTime("Início"));
        // Velocidade inicial:
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                  float racketHeight) {
      // ascii art:
      // ||  1 <- at the top of the racket
      // ||  0 <- at the middle of the racket
      // || -1 <- at the bottom of the racket
      return (ballPos.y - racketPos.y) / racketHeight;
  }

    void OnCollisionEnter2D(Collision2D col) {
    // Note: 'col' holds the collision information. If the
    // Ball collided with a racket, then:
    //   col.gameObject is the racket
    //   col.transform.position is the racket's position
    //   col.collider is the racket's collider
    if (col.gameObject.CompareTag ("parededireita"))
        {
            countp1 = countp1 +1;
            countround = countround + 1;
            p1points.text = "Points P1: " +  countp1.ToString ();
            counter(countround);
        }
    if (col.gameObject.CompareTag ("paredeesquerda"))
        {
            countp2 = countp2 +1;
            countround = countround + 1;
            p2points.text = "Points P2: " +  countp2.ToString ();
            counter(countround);
        }    
    // Hit the left Racket?
    if (col.gameObject.name == "RacketLeft") {
        // Calculate hit Factor
        float y = hitFactor(transform.position,
                            col.transform.position,
                            col.collider.bounds.size.y);

        // Calculate direction, make length=1 via .normalized
        Vector2 dir = new Vector2(1, y).normalized;
        // Set Velocity with dir * speed
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }

    // Hit the right Racket?
    if (col.gameObject.name == "RacketRight") {
        // Calculate hit Factor
        float y = hitFactor(transform.position,
                            col.transform.position,
                            col.collider.bounds.size.y);

        // Calculate direction, make length=1 via .normalized
        Vector2 dir = new Vector2(-1, y).normalized;

        // Set Velocity with dir * speed
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }
    
}
 IEnumerator ExecuteAfterTime(string te)
    {
        mensagemsuser.gameObject.SetActive(true);
        mensagemsuser.text = te;
        yield return new WaitForSeconds(3.0f);
    }

void counter(int counterround){
    if(counterround==10){
        if(countp2>countp1){
            StartCoroutine (ExecuteAfterTime("Player2 win"));
            SceneManager.LoadScene("SampleScene");

        }
        else{
            StartCoroutine (ExecuteAfterTime("Player1 win"));
            SceneManager.LoadScene("SampleScene");

        }
    }

    else{
        string ronda = "Round: " + counterround.ToString();
        StartCoroutine (ExecuteAfterTime(ronda));

    }
}
}
