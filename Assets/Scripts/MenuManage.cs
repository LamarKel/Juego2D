using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject panelControles;
    public GameObject menuPrincipalUI;

    public void Jugar()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }

    public void MostrarControles()
    {
        panelControles.SetActive(true);
        menuPrincipalUI.SetActive(false); // 🔴 oculta menú
    }

    public void OcultarControles()
    {
        panelControles.SetActive(false);
        menuPrincipalUI.SetActive(true); // 🟢 vuelve menú
    }
}