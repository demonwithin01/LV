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
            this.gameObject.SetActive( false );
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

}