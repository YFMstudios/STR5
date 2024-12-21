using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;  // Photon ile ilgili sınıflara erişmek için gerekli
using Photon.Realtime;  // Photon Player sınıfı bu namespace altındadır.

public class NextGame : MonoBehaviour
{
    public void startLevel1() 
    {
        SceneManager.LoadScene(2);
    }

    public void goWarScene()
    {
        // opponentName'i yazdırıyoruz
        Debug.Log("Opponent Name: " + RegionClickHandler.opponentName);
        
        // Oyuncunun şu anki ismini CustomProperties aracılığıyla alıyoruz
        string currentPlayerName = PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("PlayerName") 
            ? PhotonNetwork.LocalPlayer.CustomProperties["PlayerName"].ToString() 
            : "Oyuncu Adı Yok";
        
        Debug.Log("Current Player Name: " + currentPlayerName);

        // Savaşacak olan iki oyuncuyu belirleyelim
        List<Photon.Realtime.Player> warPlayers = new List<Photon.Realtime.Player>();
        
        // Photon odasındaki oyuncular listesine erişiyoruz
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

        foreach (Photon.Realtime.Player player in players)
        {
            // İki oyuncuyu seçiyoruz: şu anki oyuncu ve opponent
            if (player.CustomProperties.ContainsKey("PlayerName"))
            {
                string playerName = player.CustomProperties["PlayerName"].ToString();
                
                // Eğer oyuncunun adı currentPlayerName veya opponentName ise, onları savaşa alıyoruz
                if (playerName == currentPlayerName || playerName == RegionClickHandler.opponentName)
                {
                    warPlayers.Add(player);  // Savaşacak oyuncuyu listeye ekliyoruz
                }
            }
        }

        // Eğer sadece bu iki oyuncu var ve onlar savaşa girecekse
        if (warPlayers.Count == 2)
        {
            // Savaş sahnesine geçiş yapıyoruz
            foreach (var player in PhotonNetwork.PlayerList)
            {
                // Eğer bu oyuncu, savaşacak oyunculardan biri ise sahneye geçmesine izin veriyoruz
                if (warPlayers.Contains(player))
                {
                    // Yalnızca savaşacak oyuncular için sahne geçişi
                    if (player == PhotonNetwork.LocalPlayer)
                    {
                        // Sadece yerel oyuncu savaşa geçiyor
                        PhotonNetwork.LoadLevel(7);
                    }
                }
                else
                {
                    // Diğer oyuncular sahneye geçmemeli
                    Debug.Log("Diğer oyuncu savaşa girmiyor: " + player.NickName);
                }
            }
        }
        else
        {
            Debug.LogError("Savaşacak oyuncular bulunamadı!");
        }
    }
}
