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

        // Sýralý baðlantý için ilk bir önceki kancayý veriyoruz.
        Rigidbody2D previousRope = First_Point;

        for (int i = 0; i < connectionsCount; i++)
        {
            ConnectionPool[i].gameObject.SetActive(true);

            //Ýlk zincir oluþturuldu.
            HingeJoint2D joint = ConnectionPool[i].GetComponent<HingeJoint2D>();

            // Baðlantý yapýldý.
            joint.connectedBody = previousRope;
            if (i < connectionsCount - 1)
            {
                // Eðer bu bloða girdiyse baðlantý devam ediyor ve yeni baðlantýnýn konumunu güncelle.
                previousRope = ConnectionPool[i].GetComponent<Rigidbody2D>();
            }
            else
            {
                // Son olarak topa baðla.
                _Ball.LastConnection(ConnectionPool[i].GetComponent<Rigidbody2D>());
            }
        }

    }


}
