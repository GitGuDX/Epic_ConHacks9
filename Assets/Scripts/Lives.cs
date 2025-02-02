using UnityEngine;

public class Lives : MonoBehaviour
{
    public GamesObject[] livesUI;
    [SerializeField] 
    private int lives = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLivesUI();
    }

    public void UpdateLivesUI() {
        if (lives < 1) {
            Destroy(Lives[0].gameObject);
        }
        else if (lives < 2) {
            Destroy(Lives[1].gameObject);
        }
        else if (lives < 3) {
            Destroy(Lives[2].gameObject);
        }

    }
    


    public void Damage() {
        lives--;
        if (lives <= 0) {
            Destroy(gameObject);
        }
    }
}
