using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerStats  {


    private Controller controller;

    private int str;
    private int def;
    private int vit;

    private int curHP;

    public ControllerStats(Controller controller) : this(controller, 10, 10, 10){

    }

    public ControllerStats(Controller controller, int str, int def, int vit) {
        this.controller = controller;
        this.str = str;
        this.def = def;
        this.vit = vit;

        this.curHP = vit;
    }

    public void setCurHP(int hp) {
        this.curHP = hp;
        if(curHP <= 0) {
            controller.onDeath();
        }
    }

    public int getCurHP() {
        return curHP;
    }

    public int getMaxHP() {
        return vit;
    }

    public int getStr() {
        return str;
    }

    public int getDef() {
        return def;
    }

    public int getVit() {
        return vit;  
    }

}
