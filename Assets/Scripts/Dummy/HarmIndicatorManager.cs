//---------------------------------------------------------
// Este es otro de los scripts que se encuentra en el prefab de dummy, y se
// encarga de mostrar el daño que ha recibido el dummy mediante el texto que
// se encuentra encima suya.
// Creador del Script: Alejandro Menéndez Fierro
// Kingless Dungeon
// Proyectos 1 - Curso 2024-25
//---------------------------------------------------------

using TMPro;
using UnityEngine;
public class HarmIndicatorManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    // Aquí se encuentra el atributo "DamageText"a
    // 
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    private TMP_Text DamageText; // Aquí se asigna el texto encima del dummy

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----


    // ---- PROPIEDADES ----
    #region Propiedades
    // Documentar cada propiedad que aparece aquí.
    // Escribir con PascalCase.
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    // No se necesitan métodos aquí, ya se hace todo el trabajo en el método público "UpdateDamageText";
    #region Métodos de MonoBehaviour

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    public void UpdateDamageText(int damage)
    ///<summary>
    /// Este es el método principal del script. Gracias a él, el texto de daño
    /// causado encima del dummy cambiará cuando se le llame desde el script
    /// "HarmManager"
    ///</summary>
    {
        DamageText.text = $"{damage}";
        ///<summary>
        /// Cambia el texto al nuevo valor de daño actual
        /// después de la suma en el método "DamageDummy"
        ///</summary>
    }

    // ---- MÉTODOS PRIVADOS O PROTEGIDOS ----
    // No hay métodos privados en el script.
    #region Métodos Privados o Protegidos


    #endregion

} // class HarmIndicatorManager 
// namespace
