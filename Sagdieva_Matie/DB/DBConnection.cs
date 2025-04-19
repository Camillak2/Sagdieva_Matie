using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sagdieva_Matie.DB
{
    internal class DBConnection
    {
        public static SagdievaKamilla_MatieExamEntities matie = new SagdievaKamilla_MatieExamEntities();

        public static User logginedUser;
        public static Worker logginedWorker;
    }
}
