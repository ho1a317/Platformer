using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlers
{
    public static Controlers controlers = new Controlers();

    public Controls inpyts;

    public Controlers()
    {
        inpyts = new Controls();
        inpyts.Enable();
    }

}
