//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Chenlinjia Yi
// Kingless Dungeon
// Proyectos 1 - Curso 2024-25
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PlayerFallingState : BaseState
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // Puesto que son atributos globales en la clase debes usar "_" + camelCase para su nombre.
    [SerializeField][Min(0)] float _maxCoyoteTime;  //tiempo en el que el jugador puede saltar aunque este en el aire despues de caer de una plataforma
    [SerializeField] RaycastHit2D _hitLeft; //raycast izquierdo del jugador para detectar si esta en el suelo
    [SerializeField] RaycastHit2D _hitRight;//raycast derecho del jugador para detectar si esta en el suelo
    [SerializeField] float _hitDistance;//longitud de ambos ray
    [SerializeField] bool _debugRayCast; //determina si pintar los ray en el editor
    [SerializeField] float _maxSpeed; //velocidad maxima del jugador para caer
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    Rigidbody2D _rigidbody;//El rigidbody del jugador
    PlayerStateMachine _ctx;//el contexto para acceder a parametros globales del playerstatemachine
    float _coyoteTime; //parametro para saber si el jugador ha caido de una plataforma y si puede seguir saltando
    float _moveDir;//para detectar si el jugador esta en movimiento

    #endregion

    // ---- PROPIEDADES ----
    #region Propiedades
    // Documentar cada propiedad que aparece aquí.
    // Escribir con PascalCase.
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Metodo llamado al instanciar el script
    /// </summary>
    private void Start()
    {
        // Asigna la referencia a _ctx y _rigidbody
        _ctx = GetCTX<PlayerStateMachine>();
        _rigidbody = _ctx.Rigidbody;
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
    /// Determina si el subestado es Move o Idle dependiendo de si esta en movimiento el jugador
    /// </summary>
    public override void EnterState()
    {
        if (_moveDir != 0) //si movimiento no es nulo
        {
            SetSubState(Ctx.GetStateByType<PlayerMoveState>());
        }
        else
        {
            SetSubState(Ctx.GetStateByType<PlayerIdleState>());
        }

        _ctx.Animator.SetBool("IsFalling", true);

    }
    public void ResetCoyoteTime()
    {
        _coyoteTime = _maxCoyoteTime; //resetea el coyote time al maximo fijado desde el editor de unity
    }
    /// <summary>
    /// Metodo llamado antes de cambiar a otro estado.
    /// </summary>
    public override void ExitState()
    {
        _coyoteTime = 0; // cuando sale del estado falling, pone el coyoteTime a 0
        _ctx.Animator.SetBool("IsFalling",false);
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
        _moveDir = GetCTX<PlayerStateMachine>().PlayerInput.Move.ReadValue<float>();//_moveDir será 0 si no esta moviendo el jugador

        if (_coyoteTime > 0) //va restando el coyoteTime si no es 0
        {
            _coyoteTime -= Time.deltaTime;
        }
        else if (_coyoteTime < 0) _coyoteTime = 0;

        //Definimos los dos Raycast
        _hitLeft = Physics2D.Raycast(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), Vector2.down, _hitDistance, LayerMask.GetMask("Ground"));
        _hitRight = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y), Vector2.down, _hitDistance, LayerMask.GetMask("Ground"));


        if (_debugRayCast) //pinta el raycast si debugRayCast es true
        {
            Debug.DrawRay(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), Vector2.down * _hitDistance, Color.red);
            Debug.DrawRay(new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y), Vector2.down * _hitDistance, Color.red);
        }
        
    }
    protected override void FixedUpdateState()
    {
        if (_rigidbody.velocity.y < _maxSpeed) //limita la velocidad de caida en maxSpeed
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _maxSpeed);
        }
    }
    /// <summary>
    /// Metodo llamado tras UpdateState para mirar si hay que cambiar a otro estado.
    /// Principalmente es para mantener la logica de cambio de estado separada de la logica del estado en si
    /// </summary>
    protected override void CheckSwitchState()
    {
        if (_hitLeft.collider != null || _hitRight.collider != null) //detecta si esta colisionando con el suelo para pasar al estado Grounded
        {
            Ctx.ChangeState(_ctx.GetStateByType<PlayerGroundedState>());
        }
        else if (_coyoteTime > 0 && _ctx.PlayerInput.Jump.IsPressed()) // detecta si el jugador a dado a saltar o si el coyotetime es mayor que 0 para pasar la estado Jump
        {
            Ctx.ChangeState(_ctx.GetStateByType<PlayerJumpState>());
        }
        else if (_ctx.PlayerInput.Dash.IsPressed()) // detecta si el jugador ha presionado al dash
        {
            PlayerDashState dashState = _ctx.GetStateByType<PlayerDashState>();
            if (Time.time > dashState.NextAvailableDashTime)
            {
                Ctx.ChangeState(dashState);
            }
        }
    }

    #endregion   

} // class PlayerFallingState 
// namespace
