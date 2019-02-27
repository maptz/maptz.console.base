namespace Maptz
{
        ///<summary>A progress monitor</summary>
        public interface IProgressMonitor
    {
        /// <summary>
        /// Update the progress. 
        /// </summary>
        /// <param name="progress"></param>
        void Update(int progress);
    }
}