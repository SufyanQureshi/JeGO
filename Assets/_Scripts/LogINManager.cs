using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LogINManager : MonoBehaviour
{
    public InputField emailText;
    public InputField passwordText;

    public GameObject SignUpPanel;
    public GameObject LogInPanel;
    public GameObject RefferalPanel;
    private string AuthKey = "AIzaSyBD0Ovji8fklDHIw3YciG0IMxYdSKH0SUY";

public void SignIn()
    {
      
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
            }
            else
            {
                Debug.Log("Form upload complete!");
                SceneManager.LoadScene(1);
            }
        }
    } 
}


