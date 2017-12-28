using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public GameObject Stone = null;
    public GameObject tmp = null;
    public GameObject parent = null;
    public Transform player;
    public Transform playerCam;
    public float throwForce = 10;
    bool hasPlayer = false;
    bool beingCarried = false;
    public AudioClip[] soundToPlay;
    private AudioSource audio;
    public int dmg;
    private bool touched = false;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2")/*GvrController.AppButtonDown*/)
        {
            
            tmp =
                Instantiate(Stone, parent.transform.position,
                    Quaternion.identity);
            tmp.GetComponent<Rigidbody>().isKinematic = true;
            tmp.transform.parent = parent.transform;    
//            tmp.transform.position = playerCam.transform.position + player.transform.forward * 1.4f;
        }
        if (Input.GetButtonUp("Fire2")/*GvrController.AppButtonUp*/)
        {
            tmp.GetComponent<Rigidbody>().isKinematic = false;
            tmp.transform.parent = null;
            tmp.GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
        }
        /*float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 2.5f)
        {
            hasPlayer = true;
        }
        else
        {
            hasPlayer = false;
        }
        if (hasPlayer && Input.GetButtonDown("Use"))
        {
            Stone.GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            beingCarried = true;
        }
        if (beingCarried)
        {
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }
            if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Rigidbody>().isKinematic = false;
                    transform.parent = null;
                    beingCarried = false;
                    GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);
                RandomAudio();
                }
                else if (Input.GetMouseButtonDown(1))
                {
                GetComponent<Rigidbody>().isKinematic = false;
                    transform.parent = null;
                beingCarried = false;
                }
            }*/
    }

    void RandomAudio()
    {
        /* if (audio.isPlaying){
             return;
                 }
         audio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
         audio.Play();*/
    }

    void OnTriggerEnter()
    {
        if (beingCarried)
        {
            touched = true;
        }
    }
}