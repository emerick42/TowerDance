using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;


// Sauvegarde se trouve dans c:\Users\{Nom d’utilisateur}\Documents\SavedGames\{Nom de votre projet}\
namespace Data
{
    class SerializeData<T>
    {
        private StorageDevice storageDevice;
        private readonly string ConfigurationFileName;
 
        public SerializeData(string FileName_)
        {
            ConfigurationFileName = FileName_;

            IAsyncResult result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);

            storageDevice = StorageDevice.EndShowSelector(result);
        }

        public T LoadGameConfiguration(T res, string containerName)
        {
            StorageContainer container = GetContainer(containerName);

            // If File doesn't exist
            if (!container.FileExists(ConfigurationFileName))
                return res;

            // If File exist
            Stream stream = container.OpenFile(ConfigurationFileName, FileMode.Open);

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            res = (T)serializer.Deserialize(stream);

            stream.Close(); // Close File

            container.Dispose(); // Close "Container"


            return res;
        }

        public void SaveGameConfiguration(T gameConfig, string containerName)
        {
            StorageContainer container = GetContainer(containerName);

            if (container.FileExists(ConfigurationFileName))
                container.DeleteFile(ConfigurationFileName);

            Stream stream = container.CreateFile(ConfigurationFileName);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, gameConfig);
            stream.Close();

            container.Dispose();
        }

        private StorageContainer GetContainer(string name)
        {            
            IAsyncResult result = storageDevice.BeginOpenContainer(name, null, null);
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = storageDevice.EndOpenContainer(result);
            result.AsyncWaitHandle.Close();

            return container;
        }
    }
}
