using System;
using UnityEngine;

/// <summary>
/// The bullet base that is responsible for managing the lifespan of a single bullet.
/// </summary>
public class Bullet : MonoBehaviour
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// The maximum distance that this bullet is allowed to travel.
    /// Exists for performance purposes only.
    /// </summary>
    private const float MAX_DISTANCE = 150f;

    /// <summary>
    /// Holds the location that the bullet was initially fired from.
    /// </summary>
    private Vector3 _fireFrom;

    /// <summary>
    /// Holds the direction that the bullet was fired.
    /// </summary>
    private Vector3 _direction;

    /// <summary>
    /// Holds the bullet type.
    /// </summary>
    private BulletType _bulletType;

    /// <summary>
    /// Holds the damage that this bullet does.
    /// </summary>
    protected readonly int damage;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Events

    /// <summary>
    /// The event which is raised whenever a bullet is deactivated.
    /// </summary>
    public event Action<Bullet> Deactivated;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Creates a new bullet using the provided bullet type.
    /// </summary>
    /// <param name="bulletType">The type of this bullet.</param>
    public Bullet( BulletType bulletType )
    {
        this._bulletType = bulletType;

        this.damage = 10;
    }

    /// <summary>
    /// Activates the bullet game object.
    /// </summary>
    public void Initialise()
    {
        this.gameObject.SetActive( false );
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods
        
    /// <summary>
    /// Updates the position of the bullet.
    /// </summary>
    private void Update()
    {
        if ( Vector3.Distance( this._fireFrom, base.transform.position ) > MAX_DISTANCE )
        {
            this.Deactivated( this );
        }
        else
        {
            base.transform.position += this._direction;
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Deactivates the current bullet to prevent new collisions.
    /// </summary>
    public void Deactivate()
    {
        this.Deactivated( this );
    }

    /// <summary>
    /// Fires this bullet from one location in the specified direction.
    /// </summary>
    /// <param name="fireFrom">The world space location to spawn the bullet.</param>
    /// <param name="direction">The world space direction to fire the bullet.</param>
    public void Fire( Vector3 fireFrom, Vector3 direction )
    {
        this._fireFrom = fireFrom;
        this._direction = direction;

        base.transform.position = fireFrom;

        base.gameObject.SetActive( true );
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
    /// Gets the bullet type.
    /// </summary>
    public BulletType BulletType
    {
        get
        {
            return this._bulletType;
        }
    }

    /// <summary>
    /// Gets the damage that this bullet is capable of.
    /// </summary>
    public int Damage
    {
        get
        {
            return this.damage;
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}