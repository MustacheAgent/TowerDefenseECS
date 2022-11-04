using Dictionaries;
using UnityEditor;

[CustomPropertyDrawer(typeof(EnemyDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }