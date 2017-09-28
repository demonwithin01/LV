using UnityEngine;
using System.Collections;

public abstract class Wave
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    protected Path[] flightPaths;

    protected int flightPathIndex = 0;

    private WaveManager _waveManager;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    public void Configure( WaveManager waveManager )
    {

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

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    public string WaveName { get; protected set; }

    #endregion

    /* --------------------------------------------------------------------- */

}
