using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//################################################# PLAYER DATA ###################################################
public class Data
{
    public int maxHp;
    public int hp;
    public int armor;
    public int velocity;
    public int attackSpeed;
    public Data()
    {
        maxHp = 100;
        hp = 100;
        armor = 100;
        velocity = 100;
        attackSpeed = 100;
    }
    public Data(int _maxHp,int _hp,int _armor,int _velocity,int _attackSpeed) 
    {
        maxHp = _maxHp;
        hp = _hp;
        armor = _armor;
        velocity = _velocity;
        attackSpeed = _attackSpeed;
    }
}
public class Databack
{
    public int BmaxHp;
    public int Bhp;
    public int Barmor;
    public int Bvelocity;
    public int BattackSpeed;
    public Databack(int _BmaxHp, int _Bhp,int _Barmor,int _Bvelocity,int _BattackSpeed)
    {
        BmaxHp = _BmaxHp;
        Bhp = _Bhp;
        Barmor = _Barmor;
        Bvelocity = _Bvelocity;
        BattackSpeed = _BattackSpeed;
    }
}

public class Hitten
{
    public int hp;
    public int armor;
    public int velocity;
    public int damage;
    public Hitten(int _hp, int _armor, int _velocity, int _damage)
    {
        hp = _hp;
        armor = _armor;
        velocity = _velocity;
        damage = _damage;
    }
}

public class Attack
{
    public int dmg;
    public Attack(int _dmg)
    {
        dmg = _dmg;
    }
}

public class DataDelete{
    public DataDelete(){}
}


//################################################# LEVEL 1 #######################################################
public class FistEventDone
{

    public FistEventDone(){
    }
}

public class DadAttackAction{
    public DadAttackAction()
    {

    }
}
public class MomAttackAction{
    public MomAttackAction()
    {

    }
}

//################################################# LEVEL 2 #######################################################
public class CakeEvent{
    public float CakeSpeed; 
    public int ColNum;
    public CakeEvent(float inputS, int inputNum){
        this.CakeSpeed = inputS;
        this.ColNum = inputNum;
    }
}

public class BossHurtEvent{
    public int hurtDamage;
    public BossHurtEvent(int _dmg)
    {
        this.hurtDamage = _dmg;
    }
}

public class ColChangeEvnet14{
    public int setter;
    public int setterCounter;
    public ColChangeEvnet14(int input, int inputcounter){
        this.setter = input;
        this.setterCounter = inputcounter;
    }
}

//################################################# LEVEL 4 ########################################################
public class LoseGirlHealthEvent{
    public int LoseAmount;
    public LoseGirlHealthEvent(int _LoseAmount){
        LoseAmount = _LoseAmount;
    }
}

public class Girl1_2Event{
    public Girl1_2Event(){
        
    }
}

public class P3o1Event{
    public int Pinknum;
    public P3o1Event(int _Pinknum){
        Pinknum = _Pinknum;
    }
}

public class ChangePhoneEvent{
    public bool ToBattle;
    public ChangePhoneEvent(bool input){
        ToBattle = input;
    }
}

public class P3o1Damage
{
    public int hp;
    public int armor;
    public int velocity;
    public int damage;
    public P3o1Damage(int _hp, int _armor, int _velocity, int _damage)
    {
        hp = _hp;
        armor = _armor;
        velocity = _velocity;
        damage = _damage;
    }
}


//################################################# LEVEL 5 ########################################################
public class MoveSwitch{
    public bool sw;
    public MoveSwitch(bool input){
        this.sw = input;
    }
}

public class ChangePhaseEvent{

    public ChangePhaseEvent(){
    }
}



public class ChangeFenceEvent{

    public ChangeFenceEvent(){
    }
}

//################################################# Audio ########################################################
public class AudioEvent
{
    public string EventName;
    public bool activate;
    public AudioEvent(string name,bool _activate)
    {
        this.EventName = name;
        this.activate = _activate;
    }
}
//拳头动画=Fist
//玩家走路减速=SlowWalk
//同事攻击=ColleageAttack
//吧唧嘴=EatCake
//老板讲话=BossTalk
//飞饼=FlyCake
//关进笼子=IntoCage
//妈妈哭了=MomCry
//笼子受击=CageHitten
//笼子炸开=CageDestroy
//压迫主角=PushPlayer
//发消息=SendMessage
//收消息=GetMessage
//女友叹气=GirlSign
//平静分手=BreakPeace
//生气分手=BreakAngry
//女友啧啧啧=GirlZe
//射中字=BoxShot
//Level5Phase1=L5P1
//Level5Phase2=L5P2
//Level4对话=L4Con
//Level4战斗=L4Battle

//发射子弹=BulletShoot
//主角挥剑=FighterAttack

//穿装备=GetItem
//脱装备=DropItem

//Textbox=Dialogue
//MenuButton=MenuButton
//死亡=Death
//打字机=Typewriter
//胜利=Victory
//开门=OpenDoor
//受到攻击=Hitten

//掉状态=NegaStatus


//################################################# General ########################################################
public class FadeInEvent{
    public bool FadeIn;
    public FadeInEvent(bool input){
        this.FadeIn = input;
    }
}
public class DiaEndEvent{

    public DiaEndEvent(){
    }
}

public class CombatEnd{
    public bool Fake;
    public CombatEnd(bool input){
        this.Fake = input;
    }
}

public class ClickEnd{

    public ClickEnd(){
    }
}

public class DropItemEvent
{
    public List<int> setNum;
    public Transform[] dropPos;
    public int dropSum;
    public DropItemEvent(List<int> input, int _dropSum, Transform[] _dropPos)
    {
        this.setNum = input;
        dropPos = _dropPos;
        dropSum = _dropSum;
    }
}

public class PlayerTrans
{
    public PlayerTrans()
    {

    }
}

public class InteractionFrozen
{
    public bool isFreeze;
    public InteractionFrozen(bool input)
    {
        isFreeze = input;
    }
}

public class ChangeCursor
{
    public bool isChange;
    public ChangeCursor(bool input)
    {
        isChange = input;
    }
}

//################################################# PlayerMove ########################################################
public class HoldStillEvent
{
    public bool isStop;
    public HoldStillEvent(bool _isStop)
    {
        this.isStop = _isStop;
    }
}

public class VerticalMoveEvent
{
    public bool onlyVertical;
    public VerticalMoveEvent(bool _onlyVertical)
    {
        this.onlyVertical = _onlyVertical;
    }
}

public class SlowMoveEvent
{
    public float speedMultiple;
    public SlowMoveEvent(float _speedMultiple)
    {
        this.speedMultiple = _speedMultiple;
    }
}

public class DisableAttack
{
    public DisableAttack() { }
}
