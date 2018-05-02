using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialize
{
    [Serializable]
   public class MyClass
   {
       public MyClass(string name)
       {
           this.name = name;
       }

       private string name;

       public void GetName()
       {
           Console.WriteLine(name);
       }
   }
}
