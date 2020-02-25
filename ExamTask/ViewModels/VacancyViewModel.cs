using ExamTask.Helpers;
using ExamTask.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ExamTask.ViewModels
{
    public class VacancyViewModel : BaseVM
    {
        public int CountVacancy { get; set; }

        private Item _selectedVacancy;
        public Item SelectedVacancy
        {
            get => _selectedVacancy;
            set
            {
                _selectedVacancy = value;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "");
                    var request = httpClient.GetAsync($"https://api.hh.ru/vacancies/{_selectedVacancy.Id}").Result;
                    var response = request.Content.ReadAsStringAsync().Result;
                    var description = JsonConvert.DeserializeObject<Item>(response).Description;
                    _selectedVacancy.Description = description.Replace("<p>","\n").Replace("</p>", "").Replace("<strong>", "").Replace("</strong>", "").Replace("<ul>", "").Replace("</ul>", "").Replace("<li>", "").Replace("</li>", "").Replace("<br>", "").Replace("<br />", "").Replace("&quot;", "\"");
                }
                OnPropertyChanged();
            }
        }

        public ICollectionView VacanciesView { get; set; }

        private string _filterVacancy;
        public string FilterVacancy
        {
            get => _filterVacancy;
            set
            {
                _filterVacancy = value;

                VacanciesView.Filter = (obj) =>
                {
                    if (obj is Item vacancyItem)
                    {
                        return vacancyItem.Name.ToLower().Contains(FilterVacancy.ToLower());
                    }

                    return false;
                };
                VacanciesView.Refresh();
            }
        }

        public VacancyViewModel()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "");
                var request = httpClient.GetAsync("https://api.hh.ru/vacancies?area=1859&per_page=50").Result;
                var response = request.Content.ReadAsStringAsync().Result;
                VacanciesView = CollectionViewSource.GetDefaultView(Helper.NormalizeData(JsonConvert.DeserializeObject<Vacancy>(response).Items));
                CountVacancy = JsonConvert.DeserializeObject<Vacancy>(response).Found;
            }
        }
    }
}
