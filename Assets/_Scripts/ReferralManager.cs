using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ReferralManager : MonoBehaviour
{

    public GameObject LoadingPanel;
    public GameObject RefferalPanel;
    public InputField RefferalField;
    public GameObject SignUpPanel;
    public GameObject LogInPanel;
    public Text MessageText;
    public GameObject messageText;
    private string refferalKey;
    // Start is called before the first frame update
    void Start()
    {
       // GetAllReferrals();
    }

    public void MovetoLogIn()
    {
        RefferalPanel.SetActive(false);
        LogInPanel.SetActive(true);
    }

    public void GetAllReferrals()
    {
        LoadingPanel.SetActive(true);
        StartCoroutine(SendUnfreind());
    }
    public void DeleteRefferals()
    {
        StartCoroutine(DeleteReferralRequest(refferalKey));
    }
    
    IEnumerator DeleteReferralRequest(string code)
    {
        string request = "https://jego-44385-default-rtdb.firebaseio.com/Referral/"+code+".json";
        Debug.Log(code);


        using (UnityWebRequest webRequest = UnityWebRequest.Delete(request))
        {

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = request.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("Done ");
                    break;
            }
        }
    }
    IEnumerator SendUnfreind()
    {
        string request = "https://jego-44385-default-rtdb.firebaseio.com/Referral.json";



        using (UnityWebRequest webRequest = UnityWebRequest.Get(request))
        {

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = request.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(webRequest.downloadHandler.text);

                    Dictionary<string, string> referrals = JsonConvert.DeserializeObject<Dictionary<string, string>>(webRequest.downloadHandler.text);

                    foreach (KeyValuePair<string, string> pair in referrals)
                    {

                        Debug.Log("Refferal: " + pair.Key);
                        //Use pair.Key to get the key
                        //Use pair.Value for value
                        if (RefferalField.text == pair.Key)
                        {
                            RefferalPanel.SetActive(false);
                            SignUpPanel.SetActive(true);
                            LoadingPanel.SetActive(false);
                            refferalKey = pair.Key;
                            print("refferal:" + refferalKey);

                        }
                        else
                            
                        {
                            LoadingPanel.SetActive(false);
                            MessageText.text = "IN Valid Key!";
                            messageText.SetActive(true);
                            StartCoroutine(PopUpDisappear());
                        }
                        
                    }

                    break;
            }
        }
    }
    public IEnumerator PopUpDisappear()
    {
        yield return new WaitForSeconds(5);
        messageText.SetActive(false);
    }
}