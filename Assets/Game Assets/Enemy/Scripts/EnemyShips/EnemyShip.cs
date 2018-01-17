using System;
using UnityEngine;

/// <summary>
/// Manages the base aspects of an enemy ship.
/// </summary>
public abstract class EnemyShip : MonoBehaviour
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    /// <summary>
    /// Holds the movement speed of the ship.
    /// </summary>
    [SerializeField]
    private float _movementSpeed = 1f;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// Holds the ship type.
    /// </summary>
    private EnemyShipType _shipType;

    /// <summary>
    /// Holds the type of bullets that this ship uses.
    /// </summary>
    private BulletType _bulletType;

    /// <summary>
    /// Holds a reference to the bullet pool.
    /// </summary>
    private EnemyBulletPool _bulletPool;
    
    /// <summary>
    /// Holds the flight path of the ship.
    /// </summary>
    private Path _flightPath;

    /// <summary>
    /// Holds the start location of the ship.
    /// </summary>
    private Vector3 _startLocation;
    
    /// <summary>
    /// Holds the health of the ship.
    /// </summary>
    protected readonly int startingHealth;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Events

    /// <summary>
    /// The event which is raised whenever a bullet is deactivated.
    /// </summary>
    public event Action<EnemyShip> Destroyed;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Sets the type of ship and assigns nullable values.
    /// </summary>
    /// <param name="shipType">The ship type.</param>
    public EnemyShip( EnemyShipType shipType, BulletType bulletType )
    {
        this._shipType = shipType;
        this._bulletType = BulletType.Standard;

        this.startingHealth = 20;

        this.Points = 10;
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    /// <summary>
    /// Finds the bullet pool instance.
    /// </summary>
    protected virtual void Start()
    {
        this._bulletPool = GameObject.FindObjectOfType<EnemyBulletPool>();
        this._startLocation = new Vector3( 0f, 0f, -150f );

        this.transform.position = this._startLocation;
    }

    /// <summary>
    /// Updates the flight path.
    /// </summary>
    protected virtual void Update()
    {

        this._flightPath.Update();
    }

    /// <summary>
    /// Handlers when a collision is made. Likely this will destroy the ship.
    /// </summary>
    private void OnTriggerEnter( Collider collider )
    {
        if ( collider.gameObject.layer == Layer.PlayerBullet )
        {
            Bullet bullet = collider.gameObject.GetComponent<Bullet>();

            PlayerBulletHit( bullet, collider );

            bullet.Deactivate();
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Tells the ship to fire it's weapon.
    /// </summary>
    public virtual void Fire()
    {
        this._bulletPool.Next( this._bulletType ).Fire( base.transform.position, Vector3.back );
    }

    /// <summary>
    /// Sets the flight path that this enemy is to take.
    /// </summary>
    /// <param name="path">The flight path to take.</param>
    /// <param name="delay">The delay before starting the path.</param>
    public void SetFlightPath( Path path, float delay )
    {
        this._flightPath = path.Clone();

        this._flightPath.SetInitialDelay( delay );

        this._flightPath.Initialise( this );
    }

    /// <summary>
    /// Resets the ship object to it's starting values.
    /// </summary>
    public void Reset()
    {
        this.Health = this.startingHealth;

        this.transform.position = this._startLocation;

        this.gameObject.SetActive( true );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Protected Methods

    /// <summary>
    /// Executes the health check that determines whether or not this ship should be destroyed.
    /// </summary>
    /// <returns>True if the ship has been destroyed.</returns>
    protected bool RunHealthCheck()
    {
        if ( this.Health < 1 )
        {
            Destroy();

            return true;
        }

        return false;
    }

    /// <summary>
    /// Called whenever a player bullet connects with the ship.
    /// </summary>
    /// <param name="bullet">The bullet that connected with the ship.</param>
    /// <param name="collider">The collider that triggered the collision.</param>
    protected virtual void PlayerBulletHit( Bullet bullet, Collider collider )
    {
        if ( this.Shield > 0 )
        {
            this.ReduceShieldBy( bullet.Damage );
            
            return;
        }

        this.Health -= bullet.Damage;

        if ( this.RunHealthCheck() )
        {
            ScoreSystem.Current.AddPoints( this.Points );
        }
    }

    /// <summary>
    /// Reduces the shield by the specified amount.
    /// </summary>
    /// <param name="reduceBy">The amount to reduce the shield by.</param>
    protected void ReduceShieldBy( int reduceBy )
    {
        this.Shield -= reduceBy;

        if ( this.Shield < 1 )
        {
            ShieldDepleted();
        }
    }

    /// <summary>
    /// Called whenever the ship loses its shield.
    /// </summary>
    protected virtual void ShieldDepleted()
    {
        // Do something...
        // Why aren't you doing it?
        // What do you mean I have to tell you what to do?
        // Fine, I'll tell you what to do later.
    }

    /// <summary>
    /// Marks the ship as destroyed.
    /// </summary>
    protected virtual void Destroy()
    {
        this.gameObject.SetActive( false );

        this.Destroyed( this );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets the current health of the ship.
    /// </summary>
    public int Health { get; protected set; }

    /// <summary>
    /// Gets the shield strength of the ship.
    /// </summary>
    public int Shield { get; protected set; }

    /// <summary>
    /// Gets the number of points this ship is worth when destroyed by the player.
    /// </summary>
    public int Points { get; protected set; }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Derived Properties

    /// <summary>
    /// Gets the reference to the enemy bullet pool.
    /// </summary>
    protected BulletPool BulletPool
    {
        get
        {
            return this._bulletPool;
        }
    }

    /// <summary>
    /// Gets the type of the current enemy ship.
    /// </summary>
    public EnemyShipType ShipType
    {
        get
        {
            return this._shipType;
        }
    }

    /// <summary>
    /// Gets the start location of the ship.
    /// </summary>
    public Vector3 StartLocation
    {
        get
        {
            return this._startLocation;
        }
    }

    /// <summary>
    /// Gets the movement speed of the ship.
    /// </summary>
    public float MovementSpeed
    {
        get
        {
            return this._movementSpeed;
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
