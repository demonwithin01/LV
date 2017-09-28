using UnityEngine;
using System.Collections;
using System;

public class BasicWave : Wave
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    public BasicWave()
    {
        base.WaveName = WaveNames.Initial;
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods
        
    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    public override void Initialise()
    {
        
    }

    public override void Update()
    {
        
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Protected Methods

    public override void ApplySettings( WaveSettings settings )
    {
        InitialWaveSettings waveSettings = settings as InitialWaveSettings;

    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* --------------------------------------------------------------------- */

}
