using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace CorePlus.Common
{
      public class CloneCommonHelper
      {
            public static object Clone(object obj)
            {
                  MemoryStream memoryStream = new MemoryStream();
                  BinaryFormatter formatter = new BinaryFormatter();
                  formatter.Serialize(memoryStream, obj);
                  memoryStream.Position = 0;
                  return formatter.Deserialize(memoryStream);
            }
      }
}
