using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SignUpManager : MonoBehaviour
{
    public InputField Email;
    public InputField Password;

    public GameObject SignUpPanel;
    public GameObject LogInPanel;
    public GameObject LoadingPanel;
    public ReferralManager ReferralManager;
    public Text MessageText;
    public GameObject messageText;

    private string AuthKey = "AIzaSyBD0Ovji8fklDHIw3YciG0IMxYdSKH0SUY";


    public void SignUP()
    {
        LoadingPanel.SetActive(true);
       StartCoroutine(PostCreateUserRequest(Email.text, Password.text));

    }

    public void MoveSignIn()
    {
        SignUpPanel.SetActive(false);
        LogInPanel.SetActive(true);
    }
    IEnumerator PostCreateUserRequest( string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("key", "AIzaSyBD0Ovji8fklDHIw3YciG0IMxYdSKH0SUY");
        form.AddField("email", email);
        form.AddField("password", password);
        using (UnityWebRequest www = UnityWebRequest.Post("https://identitytoolkit.googleapis.com/v1/accounts:signUp", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                LoadingPanel.SetActive(false);
                MessageText.text = "NetWork Error!";
                messageText.SetActive(true);
                StartCoroutine(PopUpDisappear());
            }
            else
            {
                Debug.Log(www.disposeDownloadHandlerOnDispose.ToString());
                if (www.disposeDownloadHandlerOnDispose + "" == "Error")
                {
                    // ConsoleManager.instance.ShowMessage("Please try again");
                    Debug.Log("Something Went Wrong!");
                    LoadingPanel.SetActive(false);
                    MessageText.text = "Something Went Wrong!";
                    messageText.SetActive(true);
                    StartCoroutine(PopUpDisappear());
                }
                else if (www.downloadHandler.text == "Duplicate Email")
                {
                    //ConsoleManager.instance.ShowMessage("Email Already Registered");
                    Debug.Log("Email Already Registered!");
                    LoadingPanel.SetActive(false);
                    MessageText.text = "Email Already Registered!";
                    messageText.SetActive(true);
                    StartCoroutine(PopUpDisappear());
                }
                else
                {
                    Debug.Log("User Created");

                    SignUpPanel.SetActive(false);
                    LogInPanel.SetActive(true);
                    LoadingPanel.SetActive(false);
                    ReferralManager.DeleteRefferals();
                    
                }

            }
        }

    }
    public IEnumerator PopUpDisappear()
    {
        yield return new WaitForSeconds(5);
        messageText.SetActive(false);
    }
}
