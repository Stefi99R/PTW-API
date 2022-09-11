namespace Common.Results
{
    /// <summary>
    /// Determines the type of the result that object contains.
    /// </summary>
    public enum ResultType : short
    {
        /// <summary>
        /// Usually, used when internal server error has occurred on execution.
        /// </summary>
        InternalError = 0,

        /// <summary>
        /// Positive (successful) execution of the method. 
        /// </summary>
        Ok = 1,

        /// <summary>
        /// Usually, used when some resources cannot be found in the system.
        /// </summary>
        NotFound = 2,

        /// <summary>
        /// Usually, used when access to some part of the code is forbidden.
        /// </summary>
        Forbidden = 3,

        /// <summary>
        /// Usually, used when result of the execution provides duplicate outcome.
        /// </summary>
        Conflicted = 4,

        /// <summary>
        /// Usually, used on cases where validation of the data is not valid.
        /// </summary>
        Invalid = 5,

        /// <summary>
        /// Usually, used on places where access is not authorized for the execution party.
        /// </summary>
        Unauthorized = 6
    }
}
