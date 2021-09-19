using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    public AnimationCurve myCurve;

    public float speed;
    public float life;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, life);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        transform.position = new Vector3(transform.position.x, transform.position.y + myCurve.Evaluate(Time.time % myCurve.length), transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("enemy"))
        {
            Destroy(gameObject);
        }
    }
}
