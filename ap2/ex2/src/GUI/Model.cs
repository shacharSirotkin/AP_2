using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class Model:PropertyChangeNotifier
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private int rows;
        public int Rows
        {
            get { return rows; }
            set
            {
                rows = value;
                NotifyPropertyChanged("Rows");
            }
        }

        private int cols;
        public int Cols
        {
            get { return cols; }
            set
            {
                cols = value;
                NotifyPropertyChanged("Cols");
            }
        }
    }
}
