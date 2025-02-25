//---------------------------------------------------------
// Máquina de estados de los enemigos. Contiene el contexto para todos los estados
// Alexandra Lenta
// Kingless Dungeon
// Proyectos 1 - Curso 2024-25
//---------------------------------------------------------

// IMPORTANTE: No uses los métodos del MonoBehaviour: Awake(), Start(), Update, etc. (NINGUNO)

using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
// Añadir aquí el resto de directivas using


/// <summary>
/// Máquina de estados del jugador donde se contiene el contexto de todos los estados.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))] // Obliga que el GameObject que contenga a este componente tenga un Rigibody2D
[SelectionBase] // Hace que cuando selecciones el objeto desde el editor se seleccione el que tenga este componente automáticamente
public class EnemyStateMachine : StateMachine
{
    /// <summary>
    /// <para>
    /// Codifica las dos formas en las que puede mirar el enemigo.
    /// </para>
    /// <remarks> Right = 1, Left = -1 </remarks>
    /// </summary>
    public enum EnemyLookingDirection : short
    {
        Right = 1,
        Left = -1,
    }   
    

    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // Puesto que son atributos globales en la clase debes usar "_" + camelCase para su nombre.
    [Header("Player Information")]
    /// <summary>
    /// El Transform del Player. 
    /// </summary>
    [Tooltip("The player's transform.")]
    public Transform _playerTransform;
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
    /// <summary>
    /// <para>Dirección en la que mira el enemigo.</para>
    /// <para>Right = 1, Left = -1.</para>
    /// Puedes hacer <c>(short)LookingDirection</c> para obtener el valor 1 o -1 directamente.
    /// </summary>
    public EnemyLookingDirection LookingDirection { get; set; } = EnemyLookingDirection.Right;

    /// <summary>
    /// Rigidbody2D del enemigo.
    /// </summary>
    public Rigidbody2D Rigidbody { get; private set; }

    /// <summary>
    /// SpriteRenderer del enemigo.
    /// </summary>
    public SpriteRenderer SpriteRenderer { get; private set; }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>

    protected override void OnAwake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    /// <summary>
    /// Método para notificar de la muerte del enemigo a la máquina de estados.
    /// </summary>
    public void Death() {
        Debug.Log("This enemy has died!");
    } 

    #endregion

    // ---- MÉTODOS PRIVADOS O PROTEGIDOS ----
    #region Métodos Privados o Protegidos
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class EnemyStateMachine 
// namespace
