using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public bool isCrouched = false;
    public float speed = 10f;
    public static float distance;

    public bool isGrounded;
    private float jumpForce = -500f;
    private float dist;
    private float groundedTimer = 0;

    private Rigidbody rigidBody;
    private RaycastHit hit;
    private Vector3 dir;
    private Vector3 movement;

	public bool facing; //1 == right, 0 == left
	private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        isGrounded = true;
    }

    void Update()
    {
        var hitbox = GetComponent<BoxCollider>();

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			isCrouched = true;
			transform.localScale -= new Vector3(0, 0.5f, 0);
			jumpForce = -400f;
			hitbox.size = new Vector3(1, 0.5f, 1);
			if (isGrounded)
			{
				transform.localPosition = new Vector3(transform.position.x, -0.25f, 0);
			}
			
		}

		//else if (!isGrounded && !isCrouched)
		//{
		//	if (Input.GetKeyDown(KeyCode.DownArrow))
		//	{
		//		isCrouched = true;
		//		transform.localScale -= new Vector3(0, 0.5f, 0);
		//		jumpForce = -400f;
		//		hitbox.size = new Vector3(1, 0.5f, 1);
		//	}
		//}

		if (Input.GetKeyUp(KeyCode.DownArrow) && isCrouched)
		{
			isCrouched = false;
			transform.localScale += new Vector3(0, 0.5f, 0);
			jumpForce = -500f;
			hitbox.size = new Vector3(1, 1, 1);
		}

		dist = 0.5f;
        dir = new Vector3(0, -1, 0);

        Vector3 endpoint = transform.position + new Vector3(1, 0, 0);
        Vector3 startpoint = transform.position + new Vector3(-1, 0, 0);

        groundedTimer += Time.deltaTime;

        //Position
        if (!isGrounded && groundedTimer >= 0.2f)
        {
            if (Physics.Raycast(transform.position, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
                isGrounded = true;
                speed = 7.5f;
            }

            else
            {
                isGrounded = false;
            }

            //Endpoint
            if (Physics.Raycast(endpoint, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
                isGrounded = true;
                speed = 7.5f;
            }

            else
            {
                isGrounded = false;
            }

            //Startpoint
            if (Physics.Raycast(startpoint, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
                isGrounded = true;
                speed = 7.5f;
            }

            else
            {
                isGrounded = false;
            }
        }

        if (Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.AddForce(new Vector3(0, -1, 0) * jumpForce);
            speed = 3.5f;
            groundedTimer = 0;
            isGrounded = false;
        }

        distance = transform.localPosition.x;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        movement = new Vector3(moveHorizontal * (speed + 5), rigidBody.velocity.y, 0);
        rigidBody.velocity = movement;


		if(moveHorizontal > 0)
		{
			facing = true;
		}

		else if(moveHorizontal < 0)
		{
			facing = false;
		}
    }
}