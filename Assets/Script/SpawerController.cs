using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawerController : MonoBehaviour {
    public GameObject mole;
    public GameObject fake;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn()
    {
        int rand = Random.Range(0,101);
        if(rand > 15F)
        {
            var tmp = Instantiate(mole, transform);
            tmp.transform.parent = gameObject.transform;
        }
        else
        {

            var tmp = Instantiate(fake, transform);
            tmp.transform.parent = gameObject.transform;
        }
       
    }
}
