﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjMain : MonoBehaviour {
	public Text monthText; //第幾個月
	public Text AgeText; //年齡
	public Text PutMoneyText;//投入金額
	public Text bankText; //帳戶金額
	public Text Interest_Month_Text; //月配息
	public Text TotalSalaryText; //累積配息


	void Start () {
		
	}

	void Update () {
		
	}
	public void SetData	(int p_startAge, int p_month, float p_putMoney, float p_bankMoney, float p_Interest_Month, float p_TotalSalary){
		monthText.text = GetMonth(p_month);
		AgeText.text = GetAge (p_startAge*12 +p_month);
		PutMoneyText.text = ChangeMoney(p_putMoney);
		bankText.text = ChangeMoney(p_bankMoney);
		Interest_Month_Text.text = ChangeMoney(p_Interest_Month);
		TotalSalaryText.text = ChangeMoney(p_TotalSalary);
	}

	private string GetAge(int p_month){
		int _year = p_month/12;
		int _balance = p_month % 12;

		return ""+(_year)+"歲";
//		return ""+(_year)+"歲"+(_balance+1)+"個月";
	}

	private string GetMonth(int p_month){
		int _year = p_month/12;
		int _balance = p_month % 12;

//		return ""+(_year)+"歲"+(_balance+1)+"個月";
		if (_year == 0) {
			return (_balance)+"個月";
		}
		if (_balance == 0) {
			return (_year)+"年";
		}
		return (_year)+"年"+(_balance)+"個月";
	}

	public void SetGreenColor(){
		monthText.transform.parent.GetComponent<Image>().color = new Color32 (186, 255, 73, 255);
		PutMoneyText.transform.parent.GetComponent<Image>().color = new Color32 (186, 255, 73, 255);
		bankText.transform.parent.GetComponent<Image>().color = new Color32 (186, 255, 73, 255);
		Interest_Month_Text.transform.parent.GetComponent<Image>().color = new Color32 (186, 255, 73, 255);
		TotalSalaryText.transform.parent.GetComponent<Image>().color = new Color32 (186, 255, 73, 255);
		AgeText.transform.parent.GetComponent<Image>().color = new Color32 (186, 255, 73, 255);
	}

	private string ChangeMoney(float p_money){
		int _tenThousand = 0;
		int _balance = 0;
		int _money = (int)p_money;
		_tenThousand = _money / 10000;
		_balance = _money % 10000;
		if (_tenThousand == 0) {
			return _balance.ToString();
		}
		if (_balance == 0) {
			return _tenThousand + "萬";
		}
		return _tenThousand + "萬 " + _balance;
	}
}
