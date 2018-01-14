using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    [SerializeField]
    private Wave[] _waves;

    [SerializeField]
    private GameObject[] _enemyTypes;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    private Wave _initialWave;

    private Dictionary<string, WaveSettings> _waveSettings;

    private Wave _currentWave;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    // Use this for initialization
    void Start()
    {
        _waveSettings = new Dictionary<string, WaveSettings>();

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

        this._initialWave = this._waves[ 0 ];

        foreach ( WaveSettings settings in waveSettings )
        {
            _waveSettings.Add( settings.WaveName, settings );
        }

        ActivateWave( _initialWave );
    }

    // Update is called once per frame
    void Update()
    {
        _currentWave.Update();
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

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

    public GameObject[] EnemyTypes { get { return this._enemyTypes; } }

    #endregion

    /* --------------------------------------------------------------------- */

}
