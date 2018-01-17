using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Wave
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    protected Path[] flightPaths;

    /// <summary>
    /// Maintains a list of active enemies for this wave.
    /// </summary>
    private List<EnemyShip> enemies;
    
    /// <summary>
    /// Holds the wave delays that are to be applied to the enemies.
    /// </summary>
    private List<float> _waveDelays;

    /// <summary>
    /// Holds the index of the next wave delay value.
    /// </summary>
    private int _waveDelayIndex;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    /// <summary>
    /// Configures the wave with the WaveManager object.
    /// </summary>
    /// <param name="waveManager">The wave manager object.</param>
    public void Configure( WaveManager waveManager )
    {
        this.WaveManager = waveManager;
        
        this._waveDelays = new List<float>();
        this._waveDelayIndex = 0;
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Resets the enemies list.
    /// </summary>
    public virtual void Initialise()
    {
        this.enemies = new List<EnemyShip>();
    }

    public abstract void Update();

    #endregion

    /* --------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Protected Methods

    public abstract void ApplySettings( WaveSettings settings );
    
    /// <summary>
    /// Adds a delay for when the next enemy should start moving in.
    /// </summary>
    /// <param name="delay"></param>
    protected void AddWaveDelay( float delay )
    {
        this._waveDelays.Add( delay );
    }

    /// <summary>
    /// Adds an enemy of the provided type to the enemies list.
    /// </summary>
    /// <param name="shipType">The enemy type to add.</param>
    /// <param name="path">The path that this ship is to follow.</param>
    /// <param name="delay">The delay before this ship launches.</param>
    protected void AddEnemy( EnemyShipType shipType, Path path, float delay )
    {
        EnemyShip ship = this.WaveManager.EnemyShipPool.Next( shipType );
        
        ship.Destroyed += ShipDestroyed;

        ship.SetFlightPath( path, delay );

        this.enemies.Add( ship );
    }

    /// <summary>
    /// Handles when an enemy ship is destroyed.
    /// </summary>
    /// <param name="ship">The enemy ship that has been destroyed.</param>
    protected void ShipDestroyed( EnemyShip ship )
    {
        ship.Destroyed -= ShipDestroyed;

        this.enemies.Remove( ship );

        if ( this.enemies.Count == 0 )
        {
            this.WaveManager.WaveFinished();
        }
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets a reference to the WaveManager object.
    /// </summary>
    protected WaveManager WaveManager { get; private set; }

    /// <summary>
    /// Gets the name of the current wave.
    /// </summary>
    public string WaveName { get; protected set; }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Derived Properties

    /// <summary>
    /// Gets the next delay to use for an enemy.
    /// </summary>
    protected float NextDelay
    {
        get
        {
            float delay = 0f;

            if ( this._waveDelays != null && this._waveDelays.Count > 0 )
            {
                delay = this._waveDelays[ this._waveDelayIndex ];

                this._waveDelayIndex = ( this._waveDelayIndex + 1 ) % this._waveDelays.Count;
            }

            return delay;
        }
    }

    #endregion

    /* --------------------------------------------------------------------- */

}
