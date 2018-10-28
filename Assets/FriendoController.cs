using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendoController : MonoBehaviour {

    public Transform playerTarget;
    public float followDistance;
    public float moveSpeed;

    private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(playerTarget.position, transform.position) > followDistance)
        {
            rigid.MovePosition(transform.position + (playerTarget.position - transform.position).normalized * moveSpeed);
        }
	}
}
