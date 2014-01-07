
using UnityEngine;

public class Item {
	private string _name;
	private int _value;
	public Texture2D _icon;
	
//	void Awake() {
//		Init();
//	}
	
	public Item() {
		_name = "Need Name";
		_value = 0;

	}
	
	public Item(string name, int value, RarityTypes rare, int maxDur, int curDur) {
		_name = name;
		_value = value;
	}
	
	
	public string Name {
		get { return _name;  }
		set { _name = value; }
	}
	
	public int Value {
		get { return _value; }
		set { _value = value; }
	}
	
	
	public Texture2D Icon {
		get { return _icon; }
		set { _icon = value; }
	}
	
	public virtual string ToolTip() {
		return Name + "\n" +
				"Value " + Value;
	}
}