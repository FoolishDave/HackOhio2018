using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    private Rigidbody2D rigid;
    public float MoveSpeed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        Vector3 vel = rigid.velocity;
        vel.x = Input.GetAxis("Horizontal") * MoveSpeed;
        rigid.velocity = vel;
	}
}
