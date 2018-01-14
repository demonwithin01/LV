using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The bullet pool object responsible for creating new instances of bullets.
/// </summary>
public class BulletPool : MonoBehaviour
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    /// <summary>
    /// Holds the initial number of bullets to instantiate.
    /// </summary>
    [SerializeField]
    private int _initialCount;

    /// <summary>
    /// Holds the prefab for instantiating the bullet game objects.
    /// </summary>
    [SerializeField]
    private GameObject _prefab;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members
        
    /// <summary>
    /// Holds the total pool for all bullets.
    /// </summary>
    private List<Bullet> _bulletPool;

    /// <summary>
    /// Holds the queue for instantiating new bullets.
    /// </summary>
    private Queue<Bullet> _bulletQueue;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Instantiates nullable objects.
    /// </summary>
    public BulletPool()
    {
        this._initialCount = 0;
        this._prefab = null;
        this._bulletPool = new List<Bullet>();
        this._bulletQueue = new Queue<Bullet>();
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    /// <summary>
    /// Creates the bullet pool and queue.
    /// </summary>
    private void Start()
    {
        if ( this._initialCount < 1 )
        {
            throw new Exception( "The initial number of bullets must be greater than zero." );
        }

        for ( int i = 0 ; i < this._initialCount ; i++ )
        {
            this.CreateBulletInstance();
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Deactivates a bullet and re-adds it to the queue.
    /// </summary>
    /// <param name="bullet">The bullet that is to be deactivated.</param>
    public void BulletDeactivated( Bullet bullet )
    {
        this._bulletQueue.Enqueue( bullet );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    /// <summary>
    /// Creates an instance of the bullets and adds it to the appropriate pools/queues.
    /// </summary>
    private void CreateBulletInstance()
    {
        GameObject bullet = Instantiate( this._prefab );
        bullet.transform.parent = this.transform;

        Bullet script = bullet.GetComponent<Bullet>();

        script.Deactivated += BulletDeactivated;

        this._bulletPool.Add( script );
        this._bulletQueue.Enqueue( script );
    }
    
    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets the next bullet in line to be used.
    /// </summary>
    /// <remarks>
    /// Automatically creates new instances when/if needed.
    /// </remarks>
    public Bullet Next
    {
        get
        {
            if ( this._bulletQueue.Count == 0 )
            {
                this.CreateBulletInstance();
            }

            return this._bulletQueue.Dequeue();
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}