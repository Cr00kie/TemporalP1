//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Kingless Dungeon
// Proyectos 1 - Curso 2024-25
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class EnemySummonerShootState : BaseState
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // Puesto que son atributos globales en la clase debes usar "_" + camelCase para su nombre.
    [Header("Shoot Properties")]
    /// <summary>
    /// El tiempo de espera entre dos ataques
    /// </summary>
    [SerializeField] float _attackSpeed;
    /// <summary>
    /// El daño del disparo.
    /// </summary>
    [SerializeField] int _damage;
    /// <summary>
    /// Proyectil del enemigo.
    /// </summary>
    [SerializeField] MagicBullet _magicBullet;

    /// <summary>
    /// Valor de tiempo para hacer disparo
    /// </summary>
    [SerializeField][Min(0)] float _waitTimeShoot;

    /// <summary>
    /// Punto de invocación de Bala
    /// </summary>
    [SerializeField] Transform _bulletPosition;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    /// <summary>
    /// El animator del enemigo
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// Referencia del tipo EnemyStatemachine del contexto.
    /// </summary>
    private EnemySummonerStateMachine _ctx;

    /// <summary>
    /// Tiempo de espera para disparar más tiempo del momento del juego
    /// </summary>
    private float _shootTime;

    /// <summary>
    /// Booleana para ver si ha terminado de atacar
    /// </summary>
    private bool _attackFinished;

    /// <summary>
    /// Posicion de la bala en el mundo cuando mira a la izquierda
    /// </summary>
    private Vector3 _bulletRightPos;

    /// <summary>
    /// Posicion de la bala en el mundo cuando mira a la derecha
    /// </summary>
    private Vector3 _bulletLeftPos;


    /// <summary>
    /// Posicion de la bala en el momento de disparar
    /// </summary>
    private Vector3 _bulletCurrentPos;


    #endregion

    // ---- PROPIEDADES ----
    #region Propiedades
    // Documentar cada propiedad que aparece aquí.
    // Escribir con PascalCase.
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
  
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
        //Coge una referencia de la máquina de estados para evitar hacer más upcasting
        _ctx = GetCTX<EnemySummonerStateMachine>();

        //Coger animator del contexto
        _animator = _ctx.GetComponent<Animator>();
   
        // activar animator
        _animator.SetBool("IsAttack", true);

        //determinar puntos de invocación de la bala
        _bulletLeftPos = _bulletPosition.transform.position;
        _bulletRightPos.y = _bulletLeftPos.y;
        _bulletRightPos.x = transform.position.x - (_bulletPosition.transform.position.x - transform.position.x);

        //dar valor al tiempo de carga del ataque
        _shootTime = Time.time + _waitTimeShoot;

        //indicar que el ataque no está acabado puesto que acaba de empezar
        _attackFinished = false;
    }

    /// <summary>
    /// Metodo que Instancia una bala cada vez que es llamado
    /// </summary>
    public void Shoot()
    {
        //determinar el punto actual de la posicion para la bala 
        if (_ctx.LookingDirection == EnemySummonerStateMachine.EnemyLookingDirection.Left)
        {
           _bulletCurrentPos = _bulletRightPos;
        }
        else
        {
            _bulletCurrentPos = _bulletLeftPos;
        }


        Instantiate(_magicBullet, _bulletCurrentPos, transform.rotation).Setup(_ctx.PlayerTransform.position);

        
    }
    
    /// <summary>
    /// Metodo llamado antes de cambiar a otro estado.
    /// </summary>
    public override void ExitState()
    {
        _animator.SetBool("IsAttack", false);
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
        //Actualizamos la dirección en la que mira el enemigo en función de la posición respecto al jugador
        _ctx.LookingDirection = (_ctx.PlayerTransform.position.x - _ctx.transform.position.x) > 0 ?
            EnemySummonerStateMachine.EnemyLookingDirection.Left : EnemySummonerStateMachine.EnemyLookingDirection.Right;

        _ctx.SpriteRenderer.flipX = _ctx.LookingDirection == EnemySummonerStateMachine.EnemyLookingDirection.Left;

        //Disparar después del tiempo de recarga
        if (Time.time > _shootTime && !_attackFinished)
        {

            Shoot();
            _ctx.ChangeState(_ctx.GetStateByType<EnemySummonerAttackState>());
            _animator.SetBool("IsAttack", false);
            _attackFinished = true;

        }
    }


    /// <summary>
    /// Metodo llamado tras UpdateState para mirar si hay que cambiar a otro estado.
    /// Principalmente es para mantener la logica de cambio de estado separada de la logica del estado en si
    /// </summary>
    protected override void CheckSwitchState()
    {
 
    }

    #endregion   

} // class EnemySummonerShootState 
// namespace
