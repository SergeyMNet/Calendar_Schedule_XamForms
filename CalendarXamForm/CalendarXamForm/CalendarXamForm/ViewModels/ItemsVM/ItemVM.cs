using System;
using Xamarin.Forms;

namespace CalendarXamForm.ViewModels.ItemsVM
{
    public class ItemVM : BaseVM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }


        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }


        public int IntCol { get; set; }
        public int IntRow { get; set; }


        private bool _isReal;
        public bool IsReal
        {
            get { return _isReal; }
            set
            {
                _isReal = value;
                OnPropertyChanged();
                OnPropertyChanged("Color");
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
                OnPropertyChanged("BackColor");
            }
        }

        public Color BackColor
        {
            get
            {
                if (IsSelected)
                {
                    return Color.FromHex("#CCFFBA");
                }
                else
                {
                    return Color.White;
                }
            }
        }
        public Color Color
        {
            get
            {
                if (IsReal)
                {
                    return Color.Black;
                }
                else
                {
                    return Color.FromHex("#E3E3E3");
                }
            }
        }

        public Command TapItem { get; set; }

        public ItemVM(string text, string title, int col, int row)
        {
            Text = text;
            Title = title;
            IntCol = col;
            IntRow = row;

            TapItem = new Command(TapItemCommand);
        }

        private void TapItemCommand(object obj)
        {
            if (IsReal)
            {
                //IsSelected = !IsSelected;
                DoDateSel();
            }

            //Debug.WriteLine("------click date = " + Date.ToString());
        }



        public event EventHandler DateSel;
        public void DoDateSel()
        {
            EventHandler eh = DateSel;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }
    }

    //public class ClickEventArgs : EventArgs
    //{
    //    public DateTime DateTime { get; set; }

    //    public ClickEventArgs( DateTime d)
    //    {
    //        DateTime = d;
    //    }
    //}
}
