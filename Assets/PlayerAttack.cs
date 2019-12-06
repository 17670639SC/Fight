using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public GameObject Player;
	public GameObject lightAttack;
	public GameObject heavyAttack;
	public GameObject rangedAttack;
	public bool activator;
	public bool facing;

	private void Update()
	{
		facing = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().facing;
		if(Input.GetButtonDown("Fire1"))
		{
			setLightActive();
			Invoke("setLightActive", 0.5f);
		}
		if(Input.GetButtonDown("Fire2"))
		{
			setHeavyActive();
			Invoke("setHeavyActive", 0.5f);
		}
		if(Input.GetButtonDown("Fire3"))
		{
			GameObject ranged = Instantiate(rangedAttack);
			ranged.transform.localPosition = gameObject.transform.localPosition;
			if (facing)
			{
				ranged.GetComponent<Rigidbody>().velocity = new Vector3(10, 0, 0);
			}
			if(facing == false)
			{
				ranged.GetComponent<Rigidbody>().velocity = new Vector3(-10, 0, 0);
			}
			Destroy(ranged, 1);
		}
	}

	void setLightActive()
	{
		activator = !activator;
		lightAttack.SetActive(activator);
	}

	void setHeavyActive()
	{
		activator = !activator;
		heavyAttack.SetActive(activator);
	}
}
