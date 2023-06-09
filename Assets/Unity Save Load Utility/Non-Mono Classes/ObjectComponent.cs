//The ObjectComponent class holds all data of a gameobject's component.
//The Dictionary holds the actual data of a component; A field's unitName as key and the corresponding value (object) as value. Confusing, right?
using System.Collections.Generic;

[System.Serializable]
public class ObjectComponent
{
	public bool isEnabled = true;
	public string componentName;
	public Dictionary<string, object> fields;
}