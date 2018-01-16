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
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    /// <summary>
    /// Finds the bullet pool instance.
    /// </summary>
    private void Start()
    {
        this._bulletPool = GameObject.FindObjectOfType<EnemyBulletPool>();
    }

    /// <summary>
    /// Updates the flight path.
    /// </summary>
    private void Update()
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
            collider.gameObject.SetActive( false );

            Destroy();
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

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Protected Methods

    /// <summary>
    /// Marks the ship as destroyed.
    /// </summary>
    protected void Destroy()
    {
        this.Destroyed( this );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Derived Properties

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
