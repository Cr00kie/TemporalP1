//---------------------------------------------------------
// Breve descripción del contenido del archivo;
// Script del movimiento de cámara para que siga al jugador
// Responsable: SANTIAGO SALTO
// Kingless Dungeon
// Proyectos 1 - Curso 2024-25
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Componente responsable de la gestión de la cámara del juego
/// </summary>
public class CameraManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // Puesto que son atributos globales en la clase debes usar "_" + camelCase para su nombre.
  
    /// <summary>
    /// Posicion Jugador u objeto.
    /// </summary>
    [SerializeField] Transform _playerPosition;
    /// <summary>
    /// velocidad de la Cámara
    /// </summary>
    [SerializeField][Min(0)] float _velocityCamera;
    /// <summary>
    /// Margen de la Cámara respecto al objetivo
    /// </summary>
    [SerializeField] Vector3 _displacementCamera; //dista de la camara (posicion del jugador)
    

    #endregion
    
    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    
    #endregion

    // ---- PROPIEDADES ----
    #region Propiedades
    // Documentar cada propiedad que aparece aquí.
    // Escribir con PascalCase.
    #endregion 
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    
    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen   

    void FixedUpdate()
    {
        if (_playerPosition != null)
        {
            //Objetivo final de la camara (Jugador)
            Vector3 positionFinal = _playerPosition.position + _displacementCamera;

            //Hacer que el movimiento se vea gradual
            Vector3 smoothedMovement = Vector3.Lerp(transform.position, positionFinal, _velocityCamera);

            //Mover la Cámara
            transform.position = smoothedMovement;
        }
    }

 
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    #endregion
    
    // ---- MÉTODOS PRIVADOS O PROTEGIDOS ----
    #region Métodos Privados o Protegidos
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion   

} // class CameraManager 
// namespace
