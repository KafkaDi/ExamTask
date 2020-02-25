using ExamTask.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExamTask.Helpers
{
    public static class Helper
    {
        public static ObservableCollection<Item> NormalizeData(ObservableCollection<Item> items)
        {
            foreach (var item in items)
            {
                NormalizeSalary(item.Salary);
                item.Published_at = NormalizeDateTime(item.Published_at);
            }
            return items;
        }

        private static void NormalizeSalary(Salary salary)
        {
            if (salary == null)
                return;

            if (salary.From != null && salary.To != null)
            {
                salary.StringValue = $"от {salary.From} до {salary.To}";
            }
            else if (salary.From != null && salary.To == null)
            {
                salary.StringValue = $"от {salary.From}";
            }
            else if (salary.From == null && salary.To != null)
            {
                salary.StringValue = $"до {salary.To}";
            }
        }

        private static string NormalizeDateTime(string published_at)
        {
            DateTime dateTime;
            DateTime.TryParse(published_at, out dateTime);
            return published_at = dateTime.ToString("g");
        }
    }
}
