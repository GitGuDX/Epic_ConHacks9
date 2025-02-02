using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public GameObject[] livesUI;
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
            livesUI[0].SetActive(false);
        }
        else if (lives < 2) {
            livesUI[1].SetActive(false);
        }
        else if (lives < 3) {
            livesUI[2].SetActive(false);
        }

    }
    


    public void Damage() {
        lives--;
        if (lives <= 0) {
            Destroy(gameObject);
        }
    }
}
