using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;

public class LeaderboardService
{
    //private string url = "http://localhost:5000/api/leaderboard";
    private string url = "https://b294-201-202-13-148.ngrok.io/api/leaderboard";
    
    //do a get request to the url and returns the response json
    public LeaderboardModelList GetLeaderboard()
    {
        //instantiate an httpclient object
        HttpClient httpClient = new HttpClient();
        //set its baseaddress to the url
        httpClient.BaseAddress = new Uri(url);
        //set the content type header to application/json
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //get the response into a httpresponse object
        HttpResponseMessage response = httpClient.GetAsync("").Result;
        //if the response is not successful, throw an exception
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        }
        //get the response content
        string responseContent = response.Content.ReadAsStringAsync().Result;
        //print the response content
        Debug.Log("Response Content");
        Debug.Log(responseContent);
        //convert the response content to a list of leaderboard models
        LeaderboardModelList leaderboardModels = JsonUtility.FromJson<LeaderboardModelList>("{\"data\":" + responseContent + "}");
        //return the list of leaderboard models
        return leaderboardModels;
    }

    public async void AddScore(int score, string partyName)
    {
        //instantiate an httpclient object
        HttpClient httpClient = new HttpClient();
        var values = new Dictionary<string, string>
        {
            { "team", partyName },
            { "score", score.ToString() }
        };

        var content = new FormUrlEncodedContent(values);

        var response = await httpClient.PostAsync(url, content);

        var responseString = await response.Content.ReadAsStringAsync();
        //print the response content
        Debug.Log("Response Content");
        Debug.Log(responseString);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public LeaderboardPosition GetTeamPosition(string team) {
         //instantiate an httpclient object
        HttpClient httpClient = new HttpClient();
        //set its baseaddress to the url
        httpClient.BaseAddress = new Uri(url  + "/" + team);
        //set the content type header to application/json
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //get the response into a httpresponse object
        HttpResponseMessage response = httpClient.GetAsync("").Result;
        //if the response is not successful, throw an exception
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        }
        string responseContent = response.Content.ReadAsStringAsync().Result;
        LeaderboardPosition leaderboardPosition = JsonUtility.FromJson<LeaderboardPosition>(responseContent);
        return leaderboardPosition;
    }
}


[Serializable]
public class LeaderboardModel
{
    public int _id;
    public string team;
    public int score;
}

[Serializable]
public class LeaderboardModelList
{
    public List<LeaderboardModel> data;
}

[Serializable]
public class LeaderboardPosition {
    public string team;
    public int position;
    public int score;
}






