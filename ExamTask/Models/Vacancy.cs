using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExamTask.Helpers;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace ExamTask.Models
{
    public class Vacancy : BaseVM
    {
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
        public int Found { get; set; }
    }

    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Published_at { get; set; }
        public Area Area { get; set; }
        public Salary Salary { get; set; }
        public Employer Employer { get; set; }
        public Snippet Snippet { get; set; }
        public Contact Contacts { get; set; }
        public string Description { get; set; }
    }

    public class Area
    {
        public string Name { get; set; }
    }

    public class Salary
    {
        public int? From { get; set; }
        public int? To { get; set; }
        public string StringValue { get; set; }
    }

    public class Employer
    {
        public string Name { get; set; }
        public Logo Logo_urls { get; set; }
    }

    public class Logo
    {
        [JsonProperty("90")]
        public string l90 { get; set; }
        [JsonProperty("240")]
        public string l240 { get; set; }
        public string Original { get; set; }
    }

    public class Snippet
    {
        public string Requirement { get; set; }
        public string Responsibility { get; set; }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Phone> Phones { get; set; }
    }

    public class Phone
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Number { get; set; }
        public string Comment { get; set; }
    }
}
