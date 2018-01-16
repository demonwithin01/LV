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

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// Holds the layer of the bullet.
    /// </summary>
    private readonly int _bulletLayer;

    /// <summary>
    /// Holds the total pool for all bullets.
    /// </summary>
    private List<Bullet> _bulletPool;

    /// <summary>
    /// Holds the queue for instantiating new bullets.
    /// </summary>
    private Dictionary<BulletType, Queue<Bullet>> _bulletQueues;

    /// <summary>
    /// Holds the prefabs for each bullet type.
    /// </summary>
    private Dictionary<BulletType, GameObject> _bulletPrefabs;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Instantiates nullable objects.
    /// </summary>
    /// <param name="bulletLayer">The collision layer to use for all bullets managed by this pool.</param>
    public BulletPool( int bulletLayer )
    {
        this._bulletLayer = bulletLayer;

        this._initialCount = 0;
        this._bulletPool = new List<Bullet>();
        this._bulletQueues = new Dictionary<BulletType, Queue<Bullet>>();
        this._bulletPrefabs = new Dictionary<BulletType, GameObject>();

        this._bulletQueues.Add( BulletType.Standard, new Queue<Bullet>() );
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

        LoadPrefab( BulletType.Standard );

        for ( int i = 0 ; i < this._initialCount ; i++ )
        {
            this.CreateBulletInstance( BulletType.Standard );
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
        bullet.gameObject.SetActive( false );

        this._bulletQueues[ bullet.BulletType ].Enqueue( bullet );
    }

    /// <summary>
    /// Gets the next bullet in line to be used.
    /// </summary>
    /// <param name="bulletType">The type of bullet requested.</param>
    /// <remarks>
    /// Automatically creates new instances when/if needed.
    /// </remarks>
    public Bullet Next( BulletType bulletType )
    {
        Queue<Bullet> queue = this._bulletQueues[ bulletType ];

        if ( queue.Count == 0 )
        {
            this.CreateBulletInstance( bulletType );
        }

        Bullet bullet = queue.Dequeue();

        bullet.gameObject.SetActive( true );

        return bullet;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    /// <summary>
    /// Loads a ship prefab from the resources folder.
    /// </summary>
    /// <param name="bulletType">The ship type to be loaded.</param>
    private void LoadPrefab( BulletType bulletType )
    {
        this._bulletPrefabs.Add( bulletType, (GameObject)Resources.Load( "Bullets/" + bulletType.ToString(), typeof( GameObject ) ) );
    }

    /// <summary>
    /// Creates an instance of the bullets and adds it to the appropriate pools/queues.
    /// </summary>
    /// <param name="bulletType">The type of ship to create an instance of.</param>
    private void CreateBulletInstance( BulletType bulletType )
    {
        GameObject bullet;
        Bullet script;

        switch ( bulletType )
        {
            default:
            case BulletType.Standard:
                bullet = Instantiate( this._bulletPrefabs[ bulletType ] );
                break;
        }

        bullet.gameObject.layer = this._bulletLayer;
        bullet.gameObject.SetActive( false );

        bullet.transform.parent = this.transform;

        script = bullet.GetComponent<Bullet>();

        script.Deactivated += BulletDeactivated;

        this._bulletPool.Add( script );
        this._bulletQueues[ bulletType ].Enqueue( script );
    }
    
    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}