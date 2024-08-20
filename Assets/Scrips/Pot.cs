using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private Collider2D coll;
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash() {
        anim.SetTrigger("destroy");
        // Quitamos colisiones
        coll.enabled = false;
    }
}
