using UnityEngine;
using System.Collections;

public class Torchelight : MonoBehaviour {
	
	public GameObject TorchLight;
	public GameObject MainFlame;
	public GameObject BaseFlame;
	public GameObject Etincelles;
	public GameObject Fumee;
	public float MaxLightIntensity;
	public float IntensityLight;
	

	void Start () {
		float intensity = TorchLight.GetComponent<Light>().intensity * 10f ;
		MainFlame.GetComponent<ParticleSystem>().emissionRate=intensity;
		BaseFlame.GetComponent<ParticleSystem>().emissionRate=intensity;	
		Etincelles.GetComponent<ParticleSystem>().emissionRate=intensity;
		Fumee.GetComponent<ParticleSystem>().emissionRate=intensity;
	}
	

	void Update () {
		

	}
}
