using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LogINManager : MonoBehaviour
{
    public InputField emailText;
    public InputField passwordText;
    public GameObject LoadingPanel;
    public GameObject LogInPanel;
    public GameObject RefferalPanel;
    public Text MessageText;
    public GameObject messageText;
    private string AuthKey = "AIzaSyBD0Ovji8fklDHIw3YciG0IMxYdSKH0SUY";

public void SignIn()
    {
        LoadingPanel.SetActive(true);
        StartCoroutine(SignIn(emailText.text, passwordText.text));

    }
    public void MoveSignUp()
    {
        RefferalPanel.SetActive(true);
        LogInPanel.SetActive(false);
    }

    IEnumerator SignIn(string email, string password)
    {
        
        WWWForm form = new WWWForm();
        form.AddField("key", "AIzaSyBD0Ovji8fklDHIw3YciG0IMxYdSKH0SUY");
        form.AddField("email", email);
        form.AddField("password", password);


        using (UnityWebRequest www = UnityWebRequest.Post("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                LoadingPanel.SetActive(false);
                MessageText.text = "IN Valid Email or Password!";
                messageText.SetActive(true);
                StartCoroutine(PopUpDisappear());
            }
            else
            {
                Debug.Log("Form upload complete!");
                LoadingPanel.SetActive(false);
                SceneManager.LoadScene(1);
            }
        }
    }
    public IEnumerator PopUpDisappear()
    {
        yield return new WaitForSeconds(5);
        messageText.SetActive(false);
    }
}


