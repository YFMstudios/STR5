using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NextGame : MonoBehaviour
{
    private string opponentName; // Rakibin ismi
    private string currentPlayerName; // Mevcut oyuncunun ismi

    // Butona tıklandığında çalışacak olan fonksiyon
    public void goWarScene()
    {
        // opponentName'i RegionClickHandler'dan alıyoruz
        opponentName = RegionClickHandler.opponentName;
        Debug.Log("Opponent Name: " + opponentName);

        // Oyuncunun şu anki ismini alıyoruz
        currentPlayerName = PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("PlayerName")
            ? PhotonNetwork.LocalPlayer.CustomProperties["PlayerName"].ToString()
            : "Oyuncu Adı Yok";

        Debug.Log("Current Player Name: " + currentPlayerName);

        // Rakip ismini ve şu anki oyuncu ismini Photon'a kaydediyoruz
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { "OpponentName", opponentName }
        });

        // Odaya yeni oyuncu eklendiğinde veya grup oluşturulmadan önce, önceki grup bilgilerini temizliyoruz
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

        foreach (Photon.Realtime.Player player in players)
        {
            // Önceki grup bilgisini temizliyoruz
            player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Group", null } });

            // Eğer oyuncunun adı currentPlayerName veya opponentName ise, onları savaşa alıyoruz
            if (player.CustomProperties.ContainsKey("PlayerName"))
            {
                string playerName = player.CustomProperties["PlayerName"].ToString();

                if (playerName == currentPlayerName || playerName == opponentName)
                {
                    // Grup bilgilerini "WarGroup" olarak kaydediyoruz
                    player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { "Group", "WarGroup" } });
                    Debug.Log(playerName + " savaşa dahil edildi (Group = WarGroup).");
                }
            }
        }
    }

    // Her oyuncu tarafından sürekli olarak kontrol edilecek fonksiyon
    public void CheckWarGroupAndLoadScene()
    {
        // Mevcut oyuncunun ismini al
        string localPlayerName = PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("PlayerName")
            ? PhotonNetwork.LocalPlayer.CustomProperties["PlayerName"].ToString()
            : "Oyuncu Adı Yok";

        // Mevcut oyuncunun grup bilgisini al
        string currentGroup = PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Group")
            ? PhotonNetwork.LocalPlayer.CustomProperties["Group"].ToString()
            : "None";

        Debug.Log("Local Player: " + localPlayerName + " | Group: " + currentGroup);

        // Eğer oyuncu "WarGroup" içinde ise sahneye geçiş yap
        if (currentGroup == "WarGroup")
        {
            Debug.Log(localPlayerName + " savaşa dahil, sahneye geçiliyor...");
            PhotonNetwork.LoadLevel(7); // Savaş sahnesine geçiş
        }
    }

    void Update()
    {
        // Sürekli olarak grup kontrolü yapıyoruz
        CheckWarGroupAndLoadScene();
    }
}
