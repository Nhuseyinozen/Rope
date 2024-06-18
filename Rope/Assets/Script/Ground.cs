using UnityEngine;

public class Ground : MonoBehaviour
{

    [SerializeField] private GameManager _GameManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            _GameManager.BlockDown();
        }
        else if (collision.CompareTag("Ball"))
        {
            
            _GameManager.BallDown();
        }
    }
}
