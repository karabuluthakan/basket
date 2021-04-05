namespace Basket.Domain.Localized
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResponseLocalized
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        void AddResource(string path);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetCurrentCultureName();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetString(string key);
    }
}