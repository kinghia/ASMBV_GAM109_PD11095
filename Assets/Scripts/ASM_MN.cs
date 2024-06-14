using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Threading;
using System;
using UnityEngine.SocialPlatforms.Impl;

[SerializeField]
public class Region
{
    public int ID;
    public string Name;
    public Region(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}

[SerializeField]
public class Players
{
    public int Id {get;set;}
    public string Name {get;set;}
    public int Score {get;set;}
    public Region PlayerRegion {get;set;}
    public string Rank { get; set; }
    ScoreKeeper sc;
    public Players(int id, string name, int score, Region region)
    {
        Id = id;
        Name = name;
        Score = score;
        PlayerRegion = region;
        Rank = calculate_rank(score);
    }
    private string calculate_rank(int score)
    {
        if (score < 100)
        {
            return "Hạng Đồng";
        }
        else if (score >= 100 && score < 500)
        {
            return "Bạc";
        }
        else if (score >= 500 && score < 1000)
        {
            return "Vàng";
        }
        else
        {
            return "Kim cương";
        }
    }
}

public class ASM_MN : Singleton<ASM_MN>
{
    public List<Region> listRegion = new List<Region>();
    public List<Players> listPlayer = new List<Players>();

    private void Start()
    {
        createRegion();
    }

    public void createRegion()
    {
        listRegion.Add(new Region(0, "VN"));
        listRegion.Add(new Region(1, "VN1"));
        listRegion.Add(new Region(2, "VN2"));
        listRegion.Add(new Region(3, "JS"));
        listRegion.Add(new Region(4, "VS"));
    }

    public void YC1()
    {

        String name = ScoreKeeper.Instance.GetUserName();
        int id = ScoreKeeper.Instance.GetID();
        int idR = ScoreKeeper.Instance.GetIDregion();

        int score = ScoreKeeper.Instance.GetScore();
        String regionName = ""; 

        if(idR == 0)
        {
            regionName = "VN";
        }
        else if (idR == 1)  
        {
            regionName = "VN1";
        }
        

        Players player1 = new Players(id, "Nam", 50, new Region(1, "VN1"));
        listPlayer.Add(player1);

        Players player2 = new Players(id, "Bin", 400, new Region(2, "VN2"));
        listPlayer.Add(player2);

        Players player3 = new Players(id, "Thor", 200, new Region(3, "JS"));
        listPlayer.Add(player3);

        Players player4 = new Players(id, "Hulk", 300, new Region(4, "VS"));
        listPlayer.Add(player4);
        /*thêm thông tin người chơi mới khi nhập từ text */
        Region playerRegion1 = new Region(idR, regionName);
        Players player0 = new Players(id, name, score, playerRegion1);
        listPlayer.Add(player0);
    }
    public void YC2()
    {
        foreach (Players player in listPlayer)
        {

            Debug.Log("Player Name: " + player.Name + " - Score: " + player.Score+" - Region: " + player.PlayerRegion.Name);    
        }
    }
    public void YC3()
    {
        int currentPlayerScore = ScoreKeeper.Instance.GetScore();

        foreach (Players player in listPlayer)
        {
            if (player.Score < currentPlayerScore)
            {
                Debug.Log("Player ID: " + player.Id + " - Name: " + player.Name + " - Score: " + player.Score + " - Region: " + player.PlayerRegion.Name + " - Rank: " + player.Rank);
            }
        }

    }
    public void YC4()
    {
        int currentPlayerId = ScoreKeeper.Instance.GetID();
        Players currentPlayer = listPlayer.FirstOrDefault(p => p.Id == currentPlayerId);

        if (currentPlayer != null)
        {
            Debug.Log("Thông tin người chơi hiện tại: ID: " + currentPlayer.Id + "| Name: " + currentPlayer.Name + "| Scores: " + currentPlayer.Score + "| Region: " + currentPlayer.PlayerRegion.Name + " | Rank: " + currentPlayer.Rank);
        }
        else
        {
            Debug.Log("Người chơi hiện tại không tồn tại!");
        }
    }
    public void YC5()
    {
        if (listPlayer.Count == 0)
        {
            Debug.Log("Danh sách không tồn tại người chơi! ");
            return;
        }
        var sortedPlayers = listPlayer.OrderByDescending(p => p.Score).ToList();
        Debug.Log("Danh sách người chơi theo thứ tự điểm số giảm dần:");
        foreach (Players player in sortedPlayers)
        {
            Debug.Log("ID: " + player.Id + "| Name: " + player.Name + "| Scores: " + player.Score + "| Region: " + player.PlayerRegion.Name + " | Rank: " + player.Rank);
        }
    }
    public void YC6()
    {
        if (listPlayer.Count == 0)
        {
            Debug.Log("Danh sách không tồn tại người chơi! ");
            return;
        }

        var sortedPlayers = listPlayer.OrderBy(p => p.Score).Take(5).ToList();
        Debug.Log("Top 5 người chơi có điểm số thấp nhất:");
        foreach (Players player in sortedPlayers)
        {
            Debug.Log("ID: " + player.Id + " | Name: " + player.Name + " | Scores: " + player.Score + " | Region: " + player.PlayerRegion.Name + " | Rank: " + player.Rank);
        }
    }
    public void YC7()
    {
        Thread rankingThread = new Thread(CalculateAndSaveAverageScoreByRegion);
        rankingThread.Name = "BXH";
        rankingThread.Start();
    }
    void CalculateAndSaveAverageScoreByRegion()
    {
        var regionAverageScores = listPlayer
        .GroupBy(player => player.PlayerRegion)
        .Select(group => new
        {
            Region = group.Key,
            AverageScore = group.Average(player => player.Score)
        })
        .ToList();

    // Write the results to a file
    string filePath = Path.Combine(Application.persistentDataPath, "bxhRegion.txt");
    using (StreamWriter writer = new StreamWriter(filePath))
    {
        foreach (var regionScore in regionAverageScores)
        {
            writer.WriteLine("Vùng: " + regionScore.Region.Name + " | Điểm trung bình: " + regionScore.AverageScore);
        }
    }

    Debug.Log("Điểm trung bình theo khu vực đã được lưu vào là: " + filePath);
    }

}