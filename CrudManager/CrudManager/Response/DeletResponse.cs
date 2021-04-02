namespace FTeam.CrudManager.Response
{
    /// <summary>
    /// Delete Item Status
    /// </summary>
    public enum DeleteStatus
    {
        /// <summary>
        /// Success To Delete and Save
        /// </summary>
        Success,
        
        /// <summary>
        /// System Exceptions
        /// </summary>
        Exception,

        /// <summary>
        /// Null Refrence Object
        /// </summary>
        NullRefrence
    }
}
