using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Rigidbody2D First_Point;

    [SerializeField] public Ball _Ball;

    [SerializeField] private int connectionsCount = 5;

    public GameObject[] ConnectionPool;

    public string JointName;

    void Start()
    {
        CreateRope();

    }
    void CreateRope()
    {

        // S�ral� ba�lant� i�in ilk bir �nceki kancay� veriyoruz.
        Rigidbody2D previousRope = First_Point;

        for (int i = 0; i < connectionsCount; i++)
        {
            ConnectionPool[i].gameObject.SetActive(true);

            //�lk zincir olu�turuldu.
            HingeJoint2D joint = ConnectionPool[i].GetComponent<HingeJoint2D>();

            // Ba�lant� yap�ld�.
            joint.connectedBody = previousRope;
            if (i < connectionsCount - 1)
            {
                // E�er bu blo�a girdiyse ba�lant� devam ediyor ve yeni ba�lant�n�n konumunu g�ncelle.
                previousRope = ConnectionPool[i].GetComponent<Rigidbody2D>();
            }
            else
            {
                // Son olarak topa ba�la.
                _Ball.LastConnection(ConnectionPool[i].GetComponent<Rigidbody2D>());
            }
        }

    }


}
