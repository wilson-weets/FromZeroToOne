using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour {
    public float dist;
    private Vector3 posFin;
    public bool down = false;
    public bool touched = false;
    public float speed;
    Vector3 posInit;

    [SerializeField]
    private GameObject faceHolder;
    [SerializeField]
    private Sprite[] faces;
    private int chosenFace;

	void Start () {
        posInit = transform.position;
        posFin = posInit + Vector3.up * dist;
    }

    private void Awake()
    {
        if (gameObject.tag != "Fake")
        {
            chosenFace = Random.Range(0, faces.Length);
            faceHolder.transform.Find("Face").GetComponent<SpriteRenderer>().sprite = faces[chosenFace];

        }
        StartCoroutine(WaitTilUp());
    }
    
    void Update ()
    {
		
	}

    private void FixedUpdate()
    {
        if(!down)
        {
            if (transform.position.y < posFin.y)
            {
                transform.position += Vector3.up * speed;
            }
            if (transform.position.y > posInit.y + 1)
            {
                GetComponent<BoxCollider>().enabled = true;
            }
        }
        else
        {
            if (transform.position.y > posInit.y)
            {
                if(touched)
                    transform.position += Vector3.up * -speed * 1.5f;
                else
                    transform.position += Vector3.up * -speed * 2f;
            }
            else
            {
                Destroy(transform.gameObject);
            }
        }
            
    }

    public void Wacked()
    {
        //When touched
        down = true;
        touched = true;
        GetComponent<BoxCollider>().enabled = false;
        transform.Find("Head").GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 5000));
    }

    private IEnumerator WaitTilUp()
    {
        
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        down = true;
    }
}
