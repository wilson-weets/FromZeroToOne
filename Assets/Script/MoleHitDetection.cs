using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoleHitDetection : MonoBehaviour
{
    private int points = 0;
    public Text txt;
    public Text txtTimer;
    public Text GameOver;
    public Button butRes;
    public GameObject hitPoint;
    private float timer = 0f;

    private bool finished;

    // Use this for initialization
    void Start()
    {
        butRes.transform.gameObject.SetActive(false);
        finished = false;
        Screen.autorotateToPortrait = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        txtTimer.text = timer.ToString();
        if (timer <= 15 && Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray touchRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(touchRay, out hit, Camera.main.farClipPlane))
            {
                if (hit.transform.gameObject.tag == "Mole" || hit.transform.gameObject.tag == "Fake")
                {
                    GameObject tmp = Instantiate(hitPoint, hit.point, new Quaternion());
                    StartCoroutine(WaitTilUp(tmp));
                    tmp.transform.parent = hit.transform.gameObject.transform;
                    hit.transform.gameObject.GetComponent<MoleController>().Wacked();
                    points++;
                }
                if (hit.transform.gameObject.tag == "Fake")
                {
                    points -= 6;
                }
                txt.text = "Score : " + points.ToString();
            }
        }
        if (timer > 15)
        {
            if (!finished)
            {
                finished = true;
                GameOver.text = "Time's up !\nYour score is " + points.ToString();
                GameOver.enabled = true;
                GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<SaveLoad>().Save(new ScoreData()
                {
                    id = "0",
                    score = points.ToString()
                });
                StartCoroutine(WaitTilEnd());
            }
        }
    }

    public void OnButtonClick()
    {
        GameOver.enabled = false;
        timer = 0f;
        points = 0;
        txt.text = "Score : 0";
        butRes.transform.gameObject.SetActive(false);
    }

    private IEnumerator WaitTilUp(GameObject h)
    {

        yield return new WaitForSeconds(1);
        Destroy(h);
    }

    private IEnumerator WaitTilEnd()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
