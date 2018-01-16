using UnityEngine;
using System.Collections;
using System;

public class InitialWave : Wave
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    public override void Initialise()
    {
        base.flightPaths = new Path[ 1 ];

        Path path = new Path();

        path.Add( new PathPoint( 0, 0, 100f ) );
        path.Add( new PathPoint( 10f, 0f, 100f, 0.5f ) );
        path.Add( new PathPoint( 0, 0f, 100f ) );
        path.Add( new PathPoint( -10f, 0f, 100f, 0.5f ) );

        base.AddWaveDelay( 0f );
        base.AddWaveDelay( 3f );

        base.flightPaths[ 0 ] = path;

        base.enemies = new System.Collections.Generic.List<EnemyShip>();
        base.enemies.Add( base.WaveManager.EnemyShipPool.Next( EnemyShipType.Standard ) );
        base.enemies.Add( base.WaveManager.EnemyShipPool.Next( EnemyShipType.Standard ) );

        for ( int i = 0 ; i < base.enemies.Count ; i++ )
        {
            base.enemies[ i ].SetFlightPath( path, base.NextDelay );
        }
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
