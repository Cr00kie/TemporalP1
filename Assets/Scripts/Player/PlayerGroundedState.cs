//---------------------------------------------------------
// Estado del jugador cuando esta en el suelo
// Chenlinjia Yi
// Kingless Dungeon
// Proyectos 1 - Curso 2024-25
//---------------------------------------------------------

using System;

//---------------------------------------------------------
// Estado del jugador cuando esta en el suelo
// Chenlinjia Yi
// Kingless Dungeon
// Proyectos 1 - Curso 2024-25
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PlayerGroundedState : BaseState
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // Puesto que son atributos globales en la clase debes usar "_" + camelCase para su nombre.
    [SerializeField][Min(0)] float _jumpBufferTime;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    Rigidbody2D _rigidbody;
    PlayerStateMachine _ctx;
    float _jumpBuffer;
    

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
        _ctx = GetCTX<PlayerStateMachine>();
        _rigidbody = _ctx.Rigidbody;
        _ctx.PlayerInput.Jump.started += (InputAction.CallbackContext context) => _jumpBuffer = _jumpBufferTime;
    }

    private void Update()
    {
        if ( _jumpBuffer > 0)
        {
            _jumpBuffer-=Time.deltaTime;
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


    /// <summary>
    /// Metodo llamado cuando al transicionar a este estado.
    /// </summary>
    public override void EnterState()
    {
        SetSubState(Ctx.GetStateByType<PlayerIdleState>());
    }
    
    /// <summary>
    /// Metodo llamado antes de cambiar a otro estado.
    /// </summary>
    public override void ExitState()
    {
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
        
    }

    /// <summary>
    /// Metodo llamado tras UpdateState para mirar si hay que cambiar a otro estado.
    /// Principalmente es para mantener la logica de cambio de estado separada de la logica del estado en si
    /// </summary>
    protected override void CheckSwitchState()
    {
        if (_jumpBuffer > 0)
        {
            ChangeState(Ctx.GetStateByType<PlayerJumpState>());
        }
        else if (_rigidbody.velocity.y < 0)
        {
            PlayerFallingState fallingState = Ctx.GetStateByType<PlayerFallingState>();
            ChangeState(fallingState);
            fallingState.ResetCoyoteTime();
        }
        else if (_ctx.PlayerInput.Dash.IsPressed())
        {
            PlayerDashState dashState = _ctx.GetStateByType<PlayerDashState>();
            if(Time.time > dashState.NextAvailableDashTime) ChangeState(dashState);
        }
        else if (_ctx.PlayerInput.Attack.IsPressed())
        {
            PlayerAttackState attackState = _ctx.GetStateByType<PlayerAttackState>();
            if (Time.time > attackState.NextAttackTime) ChangeState(attackState);
        }
    }
    #endregion   

} // class PlayerGroundedState 
// namespace
