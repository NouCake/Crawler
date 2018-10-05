using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoController : MonoBehaviour {

    private Text lbLevel;
    private Text lbVit;
    private Text lbStr;
    private Text lbDef;
    private Text lbXp;
    private Text lbGold;

	// Use this for initialization
	void Start () {
        Transform t = transform.Find("Info");
        lbLevel = t.Find("LevelLb").GetComponent<Text>();
        lbVit = t.Find("VitLb").GetComponent<Text>();
        lbStr = t.Find("StrLb").GetComponent<Text>();
        lbDef = t.Find("DefLb").GetComponent<Text>();
        lbXp = t.Find("XPLb").GetComponent<Text>();
        lbGold = t.Find("GoldLb").GetComponent<Text>();
    }
	
    public void updateStats() {
        PlayerStats stats = PlayerController.player.getStats();
        lbLevel.text = stats.getLevel() + "";
        lbVit.text = stats.getVit() + "";
        lbStr.text = stats.getStr() + "";
        lbDef.text = stats.getDef() + "";
        lbXp.text = stats.getCurXP() + "/" + stats.getMaxXP();

        lbGold.text = PlayerController.player.getGold() + "";
    }

}
