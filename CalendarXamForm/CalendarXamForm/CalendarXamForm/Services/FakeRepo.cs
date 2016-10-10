﻿using CalendarXamForm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarXamForm.Services
{
    public static class FakeRepo
    {
        private static List<TaskItem> _allTaskItems = new List<TaskItem>();

        public static void InitialRepo()
        {
            var r = new Random();

            _allTaskItems.Clear();
            for (int i = 1; i < 25; i++)
            {
                var t = new TaskItem();
                t.Id = i;
                t.Text = "Task " + i;
                t.DateTimeRenge = new DateTimeRenge(new DateTime(2016, 09, i, 9, 0, 0), new DateTime(2016, 09, i, r.Next(10, 15), 0, 0));
                
                _allTaskItems.Add(t);
            }
        }

        public static List<TaskItem> GetTaskForCurentDate(DateTime targetDate)
        {
            if(_allTaskItems.Count == 0)
                InitialRepo();

            return _allTaskItems.Where(t => t.DateTimeRenge.Start.Date == targetDate.Date).ToList();
        }

    }
}
