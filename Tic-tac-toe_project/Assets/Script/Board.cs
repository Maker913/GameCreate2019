﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
}
[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class Board : MonoBehaviour
{
    public Text[] ButtonList;
    public GameObject Panel;
    public Text WinText;
    public GameObject restartButton;
    public GameObject TitleButton;
    public Player playerX;
    public Player player〇;
    public PlayerColor activePlayerColor;
    public PlayerColor inacivePlayerColor;
    private int NumberOfMoves;
    private string PlayerSide;
    private void Awake()
    {
        SetGameControllerReferenceOnButton();
        PlayerSide = "X";
        Panel.SetActive(false);
        NumberOfMoves = 0;
        restartButton.SetActive(false);
        TitleButton.SetActive(false);
        SetPlayerColors(playerX, player〇);
    }
    //
    private void SetGameControllerReferenceOnButton()
    {
        for (int i = 0; i < ButtonList.Length; i++)
        {
            ButtonList[i].GetComponentInParent<Buttons>()
                .SetGameControllerReference(this);
        }
    }
    //
    public string GetPlayerSide()
    {
        return PlayerSide;
    }
    //駒を置いた後3つ並んでいるか確認
    public void TurnEnd()
    {
        NumberOfMoves++;
        if(//横軸のチェック
            (ButtonList[0].text == PlayerSide && ButtonList[1].text == PlayerSide && ButtonList[2].text == PlayerSide)||
            (ButtonList[3].text == PlayerSide && ButtonList[4].text == PlayerSide && ButtonList[5].text == PlayerSide)||
            (ButtonList[6].text == PlayerSide && ButtonList[7].text == PlayerSide && ButtonList[8].text == PlayerSide)
            ){ GameOver(PlayerSide);return; }
        if(//縦軸のチェック
            (ButtonList[0].text == PlayerSide && ButtonList[3].text == PlayerSide && ButtonList[6].text == PlayerSide) ||
            (ButtonList[1].text == PlayerSide && ButtonList[4].text == PlayerSide && ButtonList[7].text == PlayerSide) ||
            (ButtonList[2].text == PlayerSide && ButtonList[5].text == PlayerSide && ButtonList[8].text == PlayerSide)
            ){ GameOver(PlayerSide); return; }
        if (//斜め軸のチェック
            (ButtonList[0].text == PlayerSide && ButtonList[4].text == PlayerSide && ButtonList[8].text == PlayerSide) ||
            (ButtonList[2].text == PlayerSide && ButtonList[4].text == PlayerSide && ButtonList[6].text == PlayerSide)
            ) { GameOver(PlayerSide); return; }
        //揃っていない
        if(NumberOfMoves == 9)//引き分け
        {
            GameOver(null);
        }
        else//ターンチェンジ
        {
            ChangeSides();
        }
    }
    //ターンの切り替え処理
    private void ChangeSides()
    {
        PlayerSide = (PlayerSide == "X") ? "〇" : "X";
        
        if(PlayerSide == "X")
        {
            SetPlayerColors(playerX, player〇);
        }
        else
        {
            SetPlayerColors(player〇, playerX);
        }
    }
    //ゲームオーバー時の勝敗判定
    private void GameOver(string winner)
    {
        SetBoardInteractable(false);
        if (winner == null) SetGameOverText("引き分け");
        else SetGameOverText(winner + "の勝ち");
        restartButton.SetActive(true);
        TitleButton.SetActive(true);
    }
    
    //ターンプレイヤーと非ターンプレイヤーでパネルの色を変える
    private void SetPlayerColors(Player newPlayer,Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inacivePlayerColor.panelColor;
        oldPlayer.text.color = inacivePlayerColor.textColor;
    }
    //ゲームオーバー時のテキスト
    private void SetGameOverText(string value)
    {
        Panel.SetActive(true);
        WinText.text = value;
    }

    //リセット後のボタンを押せるようにする
    private void SetBoardInteractable(bool toggle)
    {
        for(int i = 0; i < ButtonList.Length; i++)
        {
            ButtonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }
    //再試合準備
    public void Restart()
    {
        PlayerSide = "X";
        NumberOfMoves = 0;
        Panel.SetActive(false);
        TitleButton.SetActive(false);
        restartButton.SetActive(false);
        SetPlayerColors(playerX, player〇);
        SetBoardInteractable(true);
        for(int i = 0; i < ButtonList.Length; i++)
        {
            ButtonList[i].text = "";
        }
    }
    //タイトルに戻る
    public void Title()
    {
        //SceneManagement.Instance.Title();
        SceneManager.LoadScene("Title");
    }
}
