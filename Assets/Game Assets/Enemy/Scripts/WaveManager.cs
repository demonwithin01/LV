using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    [SerializeField]
    private Wave[] _waves;
    
    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members
        
    private Dictionary<string, WaveSettings> _waveSettings;

    /// <summary>
    /// Holds a reference to the current wave.
    /// </summary>
    private Wave _currentWave;

    private EnemyShipPool _enemyShipPool;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    public WaveManager()
    {
        
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    // Use this for initialization
    void Start()
    {
        _waveSettings = new Dictionary<string, WaveSettings>();
        this._enemyShipPool = new EnemyShipPool( this.gameObject );

        WaveSettings[] waveSettings = GetComponents<WaveSettings>();

        if ( _waves == null )
        {
            _waves = new Wave[ 1 ];
            _waves[ 0 ] = new InitialWave();
        }

        foreach ( Wave wave in _waves )
        {
            wave.Configure( this );
        }
        
        foreach ( WaveSettings settings in waveSettings )
        {
            _waveSettings.Add( settings.WaveName, settings );
        }

        ActivateWave( this._waves[ 0 ] );
    }

    // Update is called once per frame
    void Update()
    {
        _currentWave.Update();
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Called whenever a wave has no enemies left.
    /// </summary>
    public void WaveFinished()
    {
        // Temp, re-use the current wave.
        ActivateWave( this._waves[ 0 ] );
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    private void ActivateWave( Wave wave )
    {
        //WaveSettings settings = _waveSettings[ wave.WaveName ];

        _currentWave = wave;
        _currentWave.Initialise();
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties
        
    public EnemyShipPool EnemyShipPool { get { return this._enemyShipPool; } }

    #endregion

    /* --------------------------------------------------------------------- */

}
