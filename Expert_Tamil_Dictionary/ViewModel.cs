using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expert_Tamil_Dictionary
{
    public class ViewModel
    {
        public string _word
        {
            get; set; }
        public string Words
        {
            get
            {
                return _word;
            }
            set
            {
                _word = value;
            }
        }
    }
}
