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
    public GameObject PopUP;
    public InputField RefferalField;



    private string AuthKey = "AIzaSyBD0Ovji8fklDHIw3YciG0IMxYdSKH0SUY";


    public void SignUP()
    {
        PopUP.SetActive(true);
    }
    public void OnSubmit()
    {
        if (RefferalField.text == "1234")
        {
            StartCoroutine(PostCreateUserRequest(Email.text, Password.text));
            PopUP.SetActive(false);
        }

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
            }
            else
            {
                Debug.Log(www.disposeDownloadHandlerOnDispose.ToString());
                if (www.disposeDownloadHandlerOnDispose + "" == "Error")
                {
                    // ConsoleManager.instance.ShowMessage("Please try again");
                    Debug.Log("Something Went Wrong!");

                }
                else if (www.downloadHandler.text == "Duplicate Email")
                {
                    //ConsoleManager.instance.ShowMessage("Email Already Registered");
                    Debug.Log("Email Already Registered!");

                }
                else
                {
                    Debug.Log("User Created");

                    SignUpPanel.SetActive(false);
                    LogInPanel.SetActive(true);
                }

            }
        }

    }
}
