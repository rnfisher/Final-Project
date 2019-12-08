using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitAnim : MonoBehaviour
{
    Animator anim;

    public static bool exit;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<PlayerScript>().exit == true)
        {
            anim.SetBool("Exit", true);
        }

        if (GameObject.Find("Player").GetComponent<PlayerScript>().exit == false)
        {
            anim.SetBool("Exit", false);
        }
}
}
