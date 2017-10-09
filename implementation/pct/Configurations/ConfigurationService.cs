using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using FM.Top.TopIntModel;

namespace Configurations
{
    public class ConfigurationService
    {
        XmlReaderSettings setting = new XmlReaderSettings();
         public static T DeserializeAndValidate<T>(XmlElement xmlStream, XmlReaderSettings settings)
        {
            Exception exception = null;
            ValidationEventHandler validationHandler = (sender, args) =>
            {
                if (args.Severity == XmlSeverityType.Error)
                {
                    if (exception == null)
                    {
                        exception = args.Exception;
                    }
                }
            };
            settings.ValidationEventHandler += validationHandler;
            try
            {
                T topConfigurationObject;

                var serializer = new XmlSerializer(typeof(T));
                using (var reader = XmlReader.Create(new XmlNodeReader(xmlStream), settings))
                {
                    topConfigurationObject = (T)serializer.Deserialize(reader);
                }

                if (topConfigurationObject == null)
                {
                    //throw new ValidationException();
                    throw new System.ArgumentException("something went wrong", "original");
                }

                if (exception != null)
                {
                    //throw new ValidationException();
                    throw new System.ArgumentException("something went wrong", "original");
                }
                return topConfigurationObject;
            }
            finally
            {
                settings.ValidationEventHandler -= validationHandler;
            }
        }

         public void LoadShiftSettings(XmlElement xmlStream)
         {
             DeserializeAndValidate<TopShiftSettings>(xmlStream, setting);
         }
    }
}
