using CalendarXamForm.Model;

namespace CalendarXamForm.ViewModels.ItemsVM
{
    public class TaskItemVM : BaseVM
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTimeRenge DateTimeRenge { get; set; }
        
        public int RowStart
        {
            get
            {
                return DateTimeRenge.Start.Hour;
            }
        }

        public int RowEnd
        {
            get
            {
                return DateTimeRenge.DurationInHours();
            }
        }

        public TaskItemVM()
        {
            
        }

        public TaskItemVM(TaskItem taskItem)
        {
            Id = taskItem.Id;
            DateTimeRenge = taskItem.DateTimeRenge;
            Text = taskItem.Text + " = " + DateTimeRenge.DurationInHours() + "h";

            OnPropertyChanged("Id");
            OnPropertyChanged("Text");
            OnPropertyChanged("DateTimeRenge");
            OnPropertyChanged("RowStart");
            OnPropertyChanged("RowEnd");
        }
    }
}
