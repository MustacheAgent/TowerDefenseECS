using Dictionaries;
using UnityEditor;

[CustomPropertyDrawer(typeof(EnemyDictionary)), CustomPropertyDrawer(typeof(TowerDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }