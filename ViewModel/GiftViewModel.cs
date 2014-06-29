using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Gifter.ViewModel
{
    public class GiftViewModel 
    {
        public int GiftId { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Podaj wartosc");
                }
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Podaj wartosc");
                }
            }
        }
        private string _imageurl;

        public string ImageUrl
        {
            get { return _imageurl; }
            set {
                _imageurl = value;
            }
        }
        
    }
}
