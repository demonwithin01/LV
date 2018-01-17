using System;
using UnityEngine;

/// <summary>
/// Manages the players current score.
/// </summary>
public class ScoreSystem : MonoBehaviour
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Initialises a new score system instance.
    /// </summary>
    public ScoreSystem()
    {
        Current = this;

        this.Reset();
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Unity Methods

    /// <summary>
    /// Finds the scoreboard game object.
    /// </summary>
    private void Start()
    {

    }

    /// <summary>
    /// Updates the scoreboard.
    /// </summary>
    private void Update()
    {

    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Resets the score down to zero.
    /// </summary>
    public void Reset()
    {
        this.Score = 0;
    }

    /// <summary>
    /// Adds points to the current score.
    /// </summary>
    /// <param name="points">The number of points to add to the current score.</param>
    public void AddPoints( int points )
    {
        this.Score += (uint)( Math.Max( 0, points ) );

        Debug.Log( "Score: " + this.Score );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Protected Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets the current score.
    /// </summary>
    public uint Score { get; private set; }

    /// <summary>
    /// Gets the current instance of the score system.
    /// </summary>
    public static ScoreSystem Current { get; private set; }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Derived Properties

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}