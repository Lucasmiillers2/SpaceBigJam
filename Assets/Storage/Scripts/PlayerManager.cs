﻿using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public GameObject fire;
    public float angle;
    public float speed;
    private float timeClick;
    private float waitShoot;
    private Vector3 destinePos;
	public Camera camPrincipal;

    void Start()
    {
        speed = 25f;
    }

    void Move()
    {
        // anda em direção do mouse
        destinePos = new Vector3((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized.x, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized.y, 0);
        transform.position += destinePos * Time.deltaTime * speed;
    }

    void ShootFaster()
    {
        if (Time.fixedTime - waitShoot > 0.2f)
        {
            Instantiate(fire, this.transform.position, Quaternion.identity);
            waitShoot = Time.fixedTime;
        }

    }
    void MouseClick()
    {

		// Quando pressionar o botão direito do mouse
		if (Input.GetMouseButtonDown(0))
        {

			//intancia o tiro, determina o momento que foi clicado e vai ate a posiçao do mouse
			float step = 300 * Time.deltaTime;
			GameObject shoot;
			Vector3 posCamMouse = camPrincipal.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));

			shoot = (GameObject)Instantiate(fire, this.transform.position, Quaternion.identity);
			shoot.GetComponent<FireManager>().target = posCamMouse;

			timeClick = Time.fixedTime;
            waitShoot = Time.fixedTime;
        }
        // Caso solte o botão direito, reinicia o tempo
        if (Input.GetMouseButtonUp(0))
        {
            timeClick = 0;
        }
        // Se o tempo não for 0 e tiver segurado o botão por mais de 0.1 segundos
        if (timeClick != 0 && Time.fixedTime - timeClick > 0.1f)
        {
            Move();
           // ShootFaster();
        }
    }

	void FixedUpdate ()
    {
        MouseClick();
    }
}
