using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : ControllerStats {
    
    private int curXP;
    private int maxXP;

    public PlayerStats(Controller controller) : base(controller, 10, 10, 10) {
        maxXP = 10;
    }

    private void levelUp() {
        setLevel(getLevel() + 1);
        curXP = 0;
        maxXP = getLevel() * getLevel() + 9;
    }

    /**
     * Adds XP and levels up if reached maxXP
     */
    public void addXP(int xp) {
        this.curXP += xp;
        if(curXP >= maxXP) {
            int overflow = curXP - maxXP;
            levelUp();
            addXP(overflow);
        }
    }

    public int getCurXP() {
        return curXP;
    }

    public int getMaxXP() {
        return maxXP;
    }

}
