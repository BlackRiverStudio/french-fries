using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Activate : MonoBehaviour
{
    private Vector2 button = new Vector2(250, 50);
    public GameObject disolve, hologram, forceField, portal, orb;
    private void Start()
    {
        disolve.SetActive(false);
        hologram.SetActive(false);
        forceField.SetActive(false);
        portal.SetActive(false);
        orb.SetActive(false);

    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(50, 50, button.x, button.y), "Disolve"))
        {
            disolve.SetActive(true);
            hologram.SetActive(false);
            forceField.SetActive(false);
            portal.SetActive(false);
            orb.SetActive(false);
        }
        if (GUI.Button(new Rect(50, 125, button.x, button.y), "Hologram"))
        {
            disolve.SetActive(false);
            hologram.SetActive(true);
            forceField.SetActive(false);
            portal.SetActive(false);
            orb.SetActive(false);

        }
        if (GUI.Button(new Rect(50, 200, button.x, button.y), "Force Field"))
        {
            disolve.SetActive(false);
            hologram.SetActive(false);
            forceField.SetActive(true);
            portal.SetActive(false);
            orb.SetActive(false);

        }
        if (GUI.Button(new Rect(50, 275, button.x, button.y), "Portal"))
        {
            disolve.SetActive(false);
            hologram.SetActive(false);
            forceField.SetActive(false);
            portal.SetActive(true);
            orb.SetActive(false);

        }
        if (GUI.Button(new Rect(50, 350, button.x, button.y), "Orb"))
        {
            disolve.SetActive(false);
            hologram.SetActive(false);
            forceField.SetActive(false);
            portal.SetActive(false);
            orb.SetActive(true);

        }
    }
}