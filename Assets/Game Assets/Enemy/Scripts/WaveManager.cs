using UnityEngine;
using System.Collections;
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

    private InitialWave _initialWave;

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
        _initialWave = new InitialWave();
        _waveSettings = new Dictionary<string, WaveSettings>();

        WaveSettings[] waveSettings = GetComponents<WaveSettings>();

        if ( _waves == null ) _waves = new Wave[0];

        foreach ( Wave wave in _waves )
        {
            wave.Configure( this );
        }

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

    #endregion

    /* --------------------------------------------------------------------- */

}
