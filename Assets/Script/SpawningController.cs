using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningController : MonoBehaviour {
    public List<SpawerController> Lspawn = new List<SpawerController>();
    public float speed;
    private float time = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        int rand = Random.Range(0, 5);
        if (time >= speed && Lspawn[rand].transform.childCount == 0 )
        {
            time = 0;
            Lspawn[rand].Spawn();
            
        }
	}
}
