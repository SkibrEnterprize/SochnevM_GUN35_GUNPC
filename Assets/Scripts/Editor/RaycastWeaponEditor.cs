using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RaycastWeapon))]
public class RaycastWeaponEditor : Editor
{
    //// Кэшируем SerializedProperty, чтобы избежать лишних вызовов FindProperty
    //private SerializedProperty fireRate;   

    //private void OnEnable()
    //{
    //    fireRate = serializedObject.FindProperty("fireRate");
    //}

    //public override void OnInspectorGUI()
    //{
    //    // Обновляем сериализованные данные
    //    serializedObject.Update();

    //    // 1. Чек‑бокс (первый элемент)
    //    EditorGUILayout.PropertyField(_showAdvancedProp, new GUIContent("Show Advanced Settings"));

    //    // 2. Если чек‑бокса отмечен – выводим остальные поля
    //    if (_showAdvancedProp.boolValue)
    //    {
    //        EditorGUI.indentLevel++;                     // отступ для визуального разделения
    //        EditorGUILayout.PropertyField(_speedProp);
    //        EditorGUILayout.PropertyField(_healthProp);
    //        EditorGUI.indentLevel--;
    //    }

    //    // 3. Прочие элементы (например, публичные свойства)
    //    //    Можно добавить кнопку или текстовый блок
    //    EditorGUILayout.Space();
    //    EditorGUILayout.LabelField("Status", ((ToggleableFields)target).Status);

    //    // Сохраняем изменения
    //    serializedObject.ApplyModifiedProperties();
}

