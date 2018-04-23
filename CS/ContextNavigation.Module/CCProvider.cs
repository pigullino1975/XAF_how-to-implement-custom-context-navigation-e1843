using System;
using System.Data;
using DevExpress.Xpo.DB;
namespace WinWebSolution.Module {
    #region For demo purposes only!!!
    public class CCProvider {
        private static readonly string fConnectionString;
        private static readonly DataSet fdataSet;
        public static string ConnectionString { get { return fConnectionString; } }
        static CCProvider() {
            string providerKey = Guid.NewGuid().ToString();
            fConnectionString = String.Format("XpoProvider={0}", providerKey);
            fdataSet = new DataSet();
            DataStoreBase.RegisterDataStoreProvider(providerKey, CreateProviderFromString);
        }
        public static IDataStore CreateProviderFromString(string connectionString, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect) {
            objectsToDisposeOnDisconnect = new IDisposable[] { };
            return new DataSetDataStore(fdataSet, autoCreateOption);
        }
    }
    #endregion
}