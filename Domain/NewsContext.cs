
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class NewsApiDBEntities
    {

        private static NewsApiDBEntities _context;
        public static NewsApiDBEntities CreateInstanceSingleton()
        {
            if (_context == null)
                _context = new NewsApiDBEntities();
            return _context;
        }
    }
}
