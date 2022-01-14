using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ReferralManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetAllReferrals();
        DeleteReferral();
    }

    public void GetAllReferrals()
    {
        //StartCoroutine(SendUnfreind("user_22"));
    }
    public void DeleteReferral()
    {
        StartCoroutine(DeleteReferralRequest("212121"));
    }
    IEnumerator DeleteReferralRequest(string code)
    {
        string request = "https://jego-44385-default-rtdb.firebaseio.com/Referral/"+code+".json";



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
    IEnumerator SendUnfreind(string status)
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
                    }

                    break;
            }
        }
    }

}