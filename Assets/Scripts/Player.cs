using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public float moveSpeed = 0.2f;
    public float moveRotation = 20;

    [SyncVar(hook="setColor")] // Sincroniza do server para os clientes
    private Color color = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (isLocalPlayer) {
            transform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed);
            transform.Rotate(0, Input.GetAxis("Horizontal") * moveRotation, 0);
            if (Input.GetKeyDown(KeyCode.C)) {
                float r = Random.Range(0f, 1f);
                float g = Random.Range(0f, 1f);
                float b = Random.Range(0f, 1f);
                CmdChangeColor(r, g, b);
            }
        }
    }

    public void setColor (Color c) {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = c;
    }

    [Command] // Sincroniza dos clientes para o server
    public void CmdChangeColor (float r, float g, float b) {
        this.color = new Color(r, g, b);
    }
}
