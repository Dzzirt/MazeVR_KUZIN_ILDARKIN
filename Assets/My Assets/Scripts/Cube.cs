using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour 
{
    //Create a reference to the CoinPoofPrefab
    public GameObject particles_collect;
    public Player player;

    public void OnGoldClicked()
    {
        GameObject.Destroy(gameObject);
        GameObject instance_particle = GameObject.Instantiate(particles_collect, transform.position, Quaternion.identity);
        GameObject.Destroy(instance_particle, 2);
        player.onCubeCollect();
    }

}
