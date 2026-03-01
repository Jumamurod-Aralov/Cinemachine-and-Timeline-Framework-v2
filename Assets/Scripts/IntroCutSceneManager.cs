using UnityEngine;

public class IntroCutSceneManager : MonoBehaviour
{
    public GameObject introShip;
    public GameObject mainShip;

    public void OnIntroFinished()
    {
        introShip.SetActive(false);
        mainShip.SetActive(true);
    }
}