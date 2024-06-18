using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private AudioSource sound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            sound.Play();
        }
    }
    public void LastConnection(Rigidbody2D lastRope)
    {
        HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
        joint.autoConfigureConnectedAnchor = false;

        //Tope son zincire baðlýyoruz.
        joint.connectedBody = lastRope;
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = new Vector2(0f, -0.2f);
    }
}
