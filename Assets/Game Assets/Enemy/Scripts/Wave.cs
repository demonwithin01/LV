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

    protected List<EnemyShip> enemies;

    protected int flightPathIndex = 0;

    private WaveManager _waveManager;

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

    public abstract void Initialise();

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
