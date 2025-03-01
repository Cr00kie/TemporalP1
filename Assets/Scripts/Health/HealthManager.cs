//---------------------------------------------------------
// Breve descripción del contenido del archivo
// He Deng
// Kingless Dungeon
// Proyectos 1 - Curso 2024-25
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class HealthManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // Puesto que son atributos globales en la clase debes usar "_" + camelCase para su nombre.

    /// <summary>
    /// La vida máxima que puede tener la entidad
    /// </summary>
    [SerializeField] int _maxHealth;

    /// <summary>
    /// La vida inicial que tiene la entidad
    /// </summary>
    [SerializeField] private int _initialHealth;

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
    /// <summary>
    /// La vida que tiene la entidad
    /// </summary>
    private int _health;








    // ---- PROPIEDADES ----
    #region Propiedades
    // Documentar cada propiedad que aparece aquí.
    // Escribir con PascalCase.
    #endregion
    /// <summary>
    /// Propiedad para la vida
    /// </summary>
    public int Health { get { return _health; } private set { _health = value; } }

    /// <summary>
    /// Evento para cuando la vida de la entidad es 0
    /// </summary>
    [HideInInspector]
    public UnityEvent _onDeath;

    /// <summary>
    /// Evento para cuando la entidad reciba daño
    /// </summary>
    [HideInInspector]
    public UnityEvent<int> _onDamaged;

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        //Dar una vida inicial a la entidad
        SetHealth(_initialHealth);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController
    
    /// <summary>
    /// Añadir vida a la entidad
    /// </summary>
    /// <param name="addedHealth"></param>
    public void AddHealth(int addedHealth)
    {
        if(_health + addedHealth > _maxHealth)
        {
            _health = _maxHealth;
        }
        else
        {
            _health = _health + addedHealth;
        }
    }

    /// <summary>
    /// Quitar vida a la entidad
    /// </summary>
    /// <param name="removedHealth"></param>
    public void RemoveHealth(int removedHealth)
    {
        if(_health - removedHealth <= 0)
        {
            _health = 0;
            _onDeath.Invoke();
        }
        else
        {
            _health = _health - removedHealth;
        }

        _onDamaged.Invoke(removedHealth);
    }

    /// <summary>
    /// Poner vida a la entidad, hacer la comprobación de no superar la vida máxima ni ser inferior que 0.
    /// </summary>
    /// <param name="setHealth"></param>
    public void SetHealth(int setHealth)
    {
        if(setHealth > _maxHealth)
        {
            _health = _maxHealth;
        }
        else if(setHealth < 0)
        {
            _health = 0;
        }
        else
        {
            _health = setHealth;
        }
    }

    #endregion

    // ---- MÉTODOS PRIVADOS O PROTEGIDOS ----
    #region Métodos Privados o Protegidos
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    /// <summary>
    /// Comprobar si la entidad esta muerto, si esta realiza todos los metodos subscriptos a el
    /// </summary>
    private void IsEntityDead()
    {
        if(_health <= 0 && gameObject.GetComponent<EnemyStateMachine>())
        {
            _onDeath.Invoke();
        }
    }
    #endregion

} // class Health 
// namespace
