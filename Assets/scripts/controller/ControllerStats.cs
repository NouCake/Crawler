using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerStats  {


    private Controller controller;

    private int level;
    private int str;
    private int def;
    private int vit;

    private int curHP;

    public ControllerStats(Controller controller) : this(controller, 10, 5, 5){

    }

    public ControllerStats(Controller controller, int vit, int str, int def) {
        this.controller = controller;
        this.vit = vit;
        this.str = str;
        this.def = def;
        this.level = 1;

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

    public int getLevel() {
        return level;
    }

    public void setLevel(int level) {
        this.level = level;
    }

    public Controller getController() {
        return controller;
    }

}
