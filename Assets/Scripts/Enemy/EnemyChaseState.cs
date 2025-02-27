//---------------------------------------------------------
// Estado de persecución del enemigo. Debe perseguir al jugador sin caerse de plataformas.
// Si el jugador sale del rango de detección se para, pero si entra en el rango de ataque continúa.
// Adrián Isasi
// Kingless Dungeon
// Proyectos 1 - Curso 2024-25
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class EnemyChaseState : BaseState
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    float _enemyWalkingSpeed;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    EnemyStateMachine _ctx;
    Rigidbody2D _rb;

    #endregion

    // ---- PROPIEDADES ----
    #region Propiedades
    // Documentar cada propiedad que aparece aquí.
    // Escribir con PascalCase.
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    private void Start()
    {
        _ctx = GetCTX<EnemyStateMachine>();   
        _rb = _ctx.Rigidbody;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Si el jugador sale del trigger pone el range a false.
        _ctx.IsPlayerInChaseRange = false;
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
    /// Metodo llamado cuando al transicionar a este estado.
    /// </summary>
    public override void EnterState()
    {
        
    }
    
    /// <summary>
    /// Metodo llamado antes de cambiar a otro estado.
    /// </summary>
    public override void ExitState()
    {
        _rb.velocity = Vector3.zero;
    }
    #endregion
    
    // ---- MÉTODOS PRIVADOS O PROTEGIDOS ----
    #region Métodos Privados o Protegidos
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    /// <summary>
    /// Metodo llamado cada frame cuando este es el estado activo de la maquina de estados.
    /// </summary>
    protected override void UpdateState()
    {
        _ctx.LookingDirection = (_ctx._playerTransform.position.x - _ctx.transform.position.x) > 0 ?
            EnemyStateMachine.EnemyLookingDirection.Right : EnemyStateMachine.EnemyLookingDirection.Left;

        _rb.velocity = new Vector2(_enemyWalkingSpeed * (short)_ctx.LookingDirection, 0);
    }

    /// <summary>
    /// Metodo llamado tras UpdateState para mirar si hay que cambiar a otro estado.
    /// Principalmente es para mantener la logica de cambio de estado separada de la logica del estado en si
    /// </summary>
    protected override void CheckSwitchState()
    {
        if (!_ctx.IsPlayerInChaseRange)
        {
            ChangeState(Ctx.GetStateByType<EnemyIdleState>());
        }
        /*else if((_ctx._playerTransform.position - _ctx.transform.position).magnitude < attackDistance)
        {
            Hace el ataque
        }*/ 
    }

    #endregion   

} // class EnemyChaseState 
// namespace
