using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(TemporaryModifyStatEffect))]
public class TemporayModifyEffectPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect rect = new Rect(position);
        rect.width = EditorGUIUtility.labelWidth;

        // On dessine "effect" à rect (à gauche)
        EditorGUI.LabelField(rect, label);

        // On décale rect vers la droite
        rect.x += rect.width;

        // On coupe la place restane en 2
        float fieldWidht = (position.width - rect.width) / 2;
        rect.width = fieldWidht;

        //On réserve de la place (30 pixel) pour le texte
        EditorGUIUtility.labelWidth = 30;
        // On récupère la propriété typeValue de la variable affectée par le PropertyDrawer
        SerializedProperty subProperty = property.FindPropertyRelative("typeValue");
        // On dessine le champ avec comme label le nom de la propriété, et le IntField avec la valeur désirée
        // Et on dis que la valeur en code est égale à celle dans l'inspector

        //subProperty.enumValueIndex = EditorGUI.EnumFlagsField(rect, subProperty.        );


        // On répète l'opération pour la seconde propriété int
        rect.x += rect.width;
        EditorGUIUtility.labelWidth = 35;
        subProperty = property.FindPropertyRelative("value");
        subProperty.floatValue = EditorGUI.FloatField(rect, subProperty.displayName, subProperty.floatValue);
    }
}