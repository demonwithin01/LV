using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all instances of enemy ships so that they can be re-used.
/// </summary>
public class EnemyShipPool : MonoBehaviour
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// Holds the total pool for all enemy ships.
    /// </summary>
    private List<EnemyShip> _enemieShipsPool;

    /// <summary>
    /// Holds the queues for each enemy ship type.
    /// </summary>
    private Dictionary<EnemyShipType, Queue<EnemyShip>> _enemyShipQueues;

    /// <summary>
    /// Holds the prefabs for each ship type.
    /// </summary>
    private Dictionary<EnemyShipType, GameObject> _shipPrefabs;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Instantiates nullable objects.
    /// </summary>
    public EnemyShipPool()
    {
        this._enemieShipsPool = new List<EnemyShip>();
        this._enemyShipQueues = new Dictionary<EnemyShipType, Queue<EnemyShip>>();
        this._shipPrefabs = new Dictionary<EnemyShipType, GameObject>();

        this._enemyShipQueues.Add( EnemyShipType.Standard, new Queue<EnemyShip>() );
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    /// <summary>
    /// Creates the ship pool and queues.
    /// </summary>
    private void Start()
    {
        LoadPrefab( EnemyShipType.Standard );

        for ( int i = 0 ; i < 4 ; i++ )
        {
            this.CreateShipInstance( EnemyShipType.Standard );
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Get the next enemy ship in line to be used.
    /// </summary>
    /// <param name="shipType">The type of ship requested.</param>
    /// <remarks>
    /// Automatically creates new instances when/if needed.
    /// </remarks>
    public EnemyShip Next( EnemyShipType shipType )
    {
        Queue<EnemyShip> queue = this._enemyShipQueues[ shipType ];

        if ( queue.Count == 0 )
        {
            CreateShipInstance( shipType );
        }

        EnemyShip ship = queue.Dequeue();

        ship.gameObject.SetActive( true );

        return ship;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    /// <summary>
    /// Loads a ship prefab from the resources folder.
    /// </summary>
    /// <param name="shipType">The ship type to be loaded.</param>
    private void LoadPrefab( EnemyShipType shipType )
    {
        this._shipPrefabs.Add( shipType, (GameObject)Resources.Load( "EnemyShips/" + shipType.ToString(), typeof( GameObject ) ) );
    }

    /// <summary>
    /// Creates a new ship instance of the specified type.
    /// </summary>
    /// <param name="shipType">The type of ship to create an instance of.</param>
    private void CreateShipInstance( EnemyShipType shipType )
    {
        GameObject ship;
        EnemyShip script;

        switch ( shipType )
        {
            default:
            case EnemyShipType.Standard:
                ship = Instantiate( this._shipPrefabs[ shipType ] );
                break;
        }

        ship.SetActive( false );
        ship.transform.parent = this.transform;

        script = ship.GetComponent<EnemyShip>();

        script.Destroyed += EnemyShipDestroyed;

        this._enemieShipsPool.Add( script );
        this._enemyShipQueues[ shipType ].Enqueue( script );
    }

    /// <summary>
    /// Re-adds the destroyed ship to the queue to be re-used.
    /// </summary>
    /// <param name="enemy">The enemy ship that was destroyed.</param>
    private void EnemyShipDestroyed( EnemyShip enemy )
    {
        enemy.gameObject.SetActive( false );

        this._enemyShipQueues[ enemy.ShipType ].Enqueue( enemy );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Derived Properties

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
